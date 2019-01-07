using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class startscene : MonoBehaviour {

    //两个功能按钮
    public Button btnStart;
    public Button btnExit;

	// Use this for initialization
	void Start () {
        //按钮点击处理事件
        btnStart.onClick.AddListener(PlayGame);
        btnExit.onClick.AddListener(ExitGame);
    }
	
	// Update is called once per frame
	void Update () {
       
    }


    //退出游戏
    public void ExitGame()
    {
        Application.Quit();
    }

    //开始游戏
    public void PlayGame() {
        // Debug.Log("play game");
        //Application.LoadLevel("game");
        SceneManager.LoadScene("game");
       
    }

}
