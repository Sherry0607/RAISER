using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFeather : MonoBehaviour
{
    public float Speed = 1.0f;
    private Vector3 m_OriginalPos;

	// Use this for initialization
	void Start ()
    {
        m_OriginalPos = gameObject.transform.localPosition;
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.position += gameObject.transform.up * Speed * Time.deltaTime;
        if (Vector3.Distance(gameObject.transform.localPosition, m_OriginalPos) >= 40)
        {
            gameObject.SetActive(false);
        }
    }

    public void Shoot()
    {
        gameObject.transform.localPosition = m_OriginalPos;
        gameObject.SetActive(true);
    }
}
