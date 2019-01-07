 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMeunUI : MonoBehaviour {

    [SerializeField]
    private GameObject GameMenuPanel;

	// Use this for initialization
	void Start () {
        HideAllPanel();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowGameMenuPanel();
        }
	}

    private void HideAllPanel()
    {
        GameMenuPanel.SetActive(false);
    }

    public void ShowGameMenuPanel()
    {
        GameMenuPanel.SetActive(true);
    }

    public void OnBackBtnDown()
    {
        Application.LoadLevel("Start");
    }

    public void OnReStartBtnDown()
    {
        Application.LoadLevel("Game");
    }

    
}
