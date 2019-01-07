using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

    public float waitingTime; //电梯上的等待时间
    public float risingForce; //上升力,emmm按上升速度来理解也是可的

    private GameObject player;
    public float staySecond; //玩家在电梯上停留的时间
    public bool moveUp; //电梯可否向上移动
    public bool atTop; //电梯是否在顶端

    // Use this for initialization
    void Start () {
        moveUp = true;
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (staySecond >= waitingTime && moveUp == true && atTop == false) //停留一定时间后电梯开始向上运动
        {
            Debug.Log("aaa");
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * risingForce);
        }

        if (staySecond >= waitingTime && moveUp == false  && atTop == true) //向下
        {
            Debug.Log("bbb");
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * (-risingForce+10));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("2222");
            staySecond += Time.deltaTime; //计算玩家停留在电梯上的时间
            if (moveUp == false && atTop == true)
            {
                player.transform.parent = gameObject.transform;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "end") //电梯到达终点位置
        {
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            Invoke("StopMovement", 0.1f);
            moveUp = false;
        }

        if (collision.name == "start") //电梯到达起始位置
        {
            transform.DetachChildren();
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            moveUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            staySecond = 0;
            if (moveUp == false)
            {
                atTop = true;
            }
            else
            {
                atTop = false;
            }
        }
    }

    void StopMovement() //抵消上升惯性
    {
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

}
