using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject RealBoss;
    public BossFeather[] Features;

    private bool m_Running;
    private int m_Index;
    private float m_Duration;
    private float m_MaxDuration;
    private float m_FeatureDuration;
    private int m_Times;
    private int m_MaxTimes;
    private float m_Sign;
    private List<Vector3> m_Points;
    private float m_Speed;
    private System.Action m_Callback;

	// Use this for initialization
	void Start ()
    {
        m_Running = false;

        foreach (var feature in Features)
        {
            feature.gameObject.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!m_Running)
        {
            return;
        }

        if (m_Index == 1 || m_Index == 2)
        {
            var target = m_Points[0];
            transform.position = Vector3.MoveTowards(transform.position, target, m_Speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, target) <= 0.00001f)
            {
                m_Points.RemoveAt(0);
                if (m_Points.Count == 0)
                {
                    m_Running = false;
                    if (m_Callback != null)
                    {
                        m_Callback.Invoke();
                    }
                }
                else
                {
                    gameObject.transform.LookAt(m_Points[0]);
                }
            }
        }
        else
        {
            m_Duration += Time.deltaTime * m_Sign;
            if (m_Duration <= 0)
            {
                m_Sign = 1;
                m_Duration = 0;

                if (++m_Times >= m_MaxTimes)
                {
                    m_Running = false;
                    if (m_Callback != null)
                    {
                        m_Callback.Invoke();
                    }
                }
            }
            else if (m_Duration >= m_MaxDuration)
            {
                m_Sign = -1;
                m_Duration = m_MaxDuration;
            }
            float t = m_Duration / m_MaxDuration;
            transform.position = (1 - t) * (1 - t) * m_Points[0] + 2 * t * (1 - t) * m_Points[1] + t * t * m_Points[2];
            transform.right = Vector3.Normalize((m_Sign > 0 ? m_Points[0] : m_Points[2]) - transform.position);

            //发射羽毛
            m_FeatureDuration += Time.deltaTime;
            if (m_FeatureDuration >= 2)
            {
                m_FeatureDuration = 0;
                Features[0].transform.parent.position = transform.position;
                foreach (var feature in Features)
                {
                    feature.Shoot();
                }
            }
        }
    }

    public void MoveLoop(int index, Vector3 point1, Vector3 point2, int times, float duration, System.Action callback)
    {
        m_Running = true;
        m_Index = index;
        m_Points = new List<Vector3>();
        for (int i = 0; i < times; ++i)
        {
            m_Points.Add(point1);
            m_Points.Add(point2);
        }
        m_Callback = callback;

        gameObject.transform.position = m_Points[0];
        gameObject.transform.LookAt(m_Points[1]);
        m_Points.RemoveAt(0);

        m_Speed = Vector3.Distance(point1, point2) * 2 / (duration / times);

        switch (index)
        {
            case 1:
                RealBoss.transform.localEulerAngles = new Vector3(90, 0, 0);
                break;
            case 2:
                RealBoss.transform.localEulerAngles = new Vector3(0, -90, -90);
                break;
        }
    }

    public void MoveBezier(Vector3 point1, Vector3 point2, Vector3 point3, int times, float duration, System.Action callback)
    {
        m_Running = true;
        m_Index = 3;
        m_Duration = 0;
        m_MaxDuration = duration / (times * 2);
        m_Times = 0;
        m_MaxTimes = times;
        m_Sign = 1;

        m_Points = new List<Vector3>
        {
            point1,
            point2,
            point3
        };

        RealBoss.transform.localEulerAngles = new Vector3(0, 0, 90);
    }
}
