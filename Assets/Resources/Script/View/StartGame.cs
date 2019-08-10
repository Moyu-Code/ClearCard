using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Button Btn;
    public GameObject[] PlaneManger;
    // Start is called before the first frame update
    void Start()
    {
        Btn.onClick.AddListener (EnterGame);
    }

    private void EnterGame()
    {
        PlaneManger[0].SetActive(false);
        PlaneManger[1].SetActive(true);

    }

    public void ExitGame()
    {
        PlaneManger[0].SetActive(true);
        PlaneManger[1].SetActive(false);
        PlaneManger[0].transform.GetChild(0).GetComponent<Text>().text = "游戏结束了";
        PlaneManger[0].transform.GetChild(1).gameObject.SetActive(false);
        Btn.onClick.RemoveListener(EnterGame);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
