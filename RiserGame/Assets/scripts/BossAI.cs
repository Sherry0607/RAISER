using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public Boss Boss1;
    public Boss Boss2;

    public UnityEngine.UI.Image LifeImage;
    private int m_Life;

    public float Duration1;
    public Transform Boss1DiveStartPoint;
    public Transform Boss1DiveEndPoint;
    public Transform Boss2DiveStartPoint;
    public Transform Boss2DiveEndPoint;

    public float Duration2;
    public Transform Boss1CollideStartPoint;
    public Transform Boss1CollideEndPoint;
    public Transform Boss2CollideStartPoint;
    public Transform Boss2CollideEndPoint;

    public float Duration3;
    public Transform Boss1BezierPoint1;
    public Transform Boss1BezierPoint2;
    public Transform Boss1BezierPoint3;
    public Transform Boss2BezierPoint1;
    public Transform Boss2BezierPoint2;
    public Transform Boss2BezierPoint3;

    private int m_CurrentIndex;
    private float m_CurrentDuration;

	// Use this for initialization
	void Start ()
    {
        m_Life = 10;
        m_CurrentIndex = 0;
        LifeImage.transform.parent.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_CurrentDuration += Time.deltaTime;
        switch (m_CurrentIndex)
        {
            case 0:     //检测玩家是否进入boss区域
                break;
            case 1:     //第一阶段
                break;
        }
    }

    //碰撞检测
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {//开始第一阶段
            EnterFirstStage();
        }
    }

    void EnterFirstStage()
    {
       
        StartCoroutine(DelayToInvoke.DelayToInvokeDo(() =>
        {
            m_CurrentIndex = 1;
            Boss1.MoveLoop(m_CurrentIndex, Boss1DiveStartPoint.position, Boss1DiveEndPoint.position, 10, Duration1, EnterSecondStage);
            Boss2.MoveLoop(m_CurrentIndex, Boss2DiveStartPoint.position, Boss2DiveEndPoint.position, 10, Duration1, null);
        }, 3f));
       

        LifeImage.transform.parent.gameObject.SetActive(true);
    }

    void EnterSecondStage()
    {
        m_CurrentIndex = 2;
        Boss1.MoveLoop(m_CurrentIndex, Boss1CollideStartPoint.position, Boss1CollideEndPoint.position, 10, Duration2, EnterThirdStage);
        Boss2.MoveLoop(m_CurrentIndex, Boss2CollideStartPoint.position, Boss2CollideEndPoint.position, 10, Duration2, null);
    }

    void EnterThirdStage()
    {
        m_CurrentIndex = 3;
        Boss1.MoveBezier(Boss1BezierPoint1.position, Boss1BezierPoint2.position, Boss1BezierPoint3.position, 5, Duration3, null);
        Boss2.MoveBezier(Boss2BezierPoint1.position, Boss2BezierPoint2.position, Boss2BezierPoint3.position, 5, Duration3, null);
    }

    public void LifeChange()
    {
        --m_Life;
        LifeImage.fillAmount = m_Life / 10.0f;
        if (m_Life == 0)
        {//boss死亡

        }
    }
}
