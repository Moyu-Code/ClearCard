using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LineinLine : MonoBehaviour

{

    private int[,] MathArry = new int[9, 15];

    private int[] NormalizeArry = new int[90];

    private int[] ClassArry = new int[135];

    private string FristCardName = "";

    private string SecondCardName = "";

    private bool IsFristCard = true;

    private int CardKind = 0;

    private int SecondCardPosX = 0;
    private int SecondCardPosY = 0;

    private int FirstCardPosX = 0;
    private int FirstCardPosY = 0;

    private int POSTemp = 1;

    private int UP = 0;
    private int RIGHT = 0;
    private int LEFT = 0;
    private int DOWN = 0;

    private Vector3 RootPos = new Vector3(-882, 960, 0);

    private int FristIndex;

    // Use this for initialization


    private void OnEnable()
    {
        InitGameCard();
        
        ClearCard();
    }



    /// <summary>
    /// 初始化
    /// </summary>
    public void InitGameCard()
    {
        var count = 0;
        var Temp = 0;

        //逻辑数据的构造
        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                NormalizeArry[Temp] = count;
                Temp++;
            }
            count++;
        }


        System.Random rand = new System.Random();

        //打乱数组
        for (int i = NormalizeArry.Length - 1; i > 0; --i)
        {
            int p = rand.Next(i + 1);
            int temp = NormalizeArry[p];
            NormalizeArry[p] = NormalizeArry[i];
            NormalizeArry[i] = temp;
        }

        //扩展数组
        for (int i = 0; i < 135; i++)
        {
            ClassArry[i] = -1;
        }
        Temp = 0;
        count = 16;
        while (count < 103)
        {
            for (int i = count; i < count + 13; i++)
            {
                ClassArry[i] = NormalizeArry[Temp];
                Temp++;
            }
            count += 15;
        }

        for (int i = 106; i < 118; i++)
        {
            ClassArry[i] = NormalizeArry[Temp];
            Temp++;
        }


        Temp = 0;
        //物理赋值
        for (int i = 0; i < ClassArry.Length; i++)
        {
            if (i == 67)
            {
                ClassArry[118] = ClassArry[67];
                ClassArry[67] = -1;//中间空出来的位置
            }

        }



        /// <summary>
        /// 将一维数组转成二维数组
        /// </summary>
        for (int i = 0; i < ClassArry.Length; i++)
        {
            MathArry[i / 15, i % 15] = ClassArry[i];
        }
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                string  PicCard ;
                this.gameObject.transform.GetChild(1).GetChild(Temp).GetChild(0).GetComponent<Text>().text =PicCard= MathArry[i, j].ToString();
                this.gameObject.transform.GetChild(1).GetChild(Temp).GetChild(0).GetComponent<Text>().fontSize = 0;
                this.gameObject.transform.GetChild(1).GetChild(Temp).name = Temp.ToString();               
                this.gameObject.transform.GetChild(1).GetChild(Temp).GetComponent<Image>().sprite = Resources.Load("Image/" + int.Parse(PicCard), typeof(Sprite)) as Sprite;
                this.gameObject.transform.GetChild(1).GetChild(Temp).GetComponent<Image>().color = Color.white;
                this.gameObject.transform.GetChild(1).GetChild(Temp).gameObject.AddComponent<EventTriggerListener>().onClick = FindSelf;
                Temp++;
            }
        }
    }

    /// <summary>
    /// 处理卡牌显示的公共类
    /// </summary>
    private void ClearCard()
    {
        int EndFlag = 0;
        for (int i = 0; i < ClassArry.Length; i++)
        {
            if (int.Parse(this.gameObject.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<Text>().text) == -1)
            {               
                this.gameObject.transform.GetChild(1).GetChild(i).gameObject.SetActive(false);
                EndFlag++;
            }
            if(EndFlag==135)
            {
                GetComponent<StartGame>().ExitGame();
            }
        }
    }

    /// <summary>
    /// 
    ///卡牌逻辑
    ///
    /// </summary>
    /// <param name="go"></param>
    private void FindSelf(GameObject go)
    {
        if (IsFristCard)
        {
            FristCardName = go.transform.GetChild(0).GetComponent<Text>().text;
            FristIndex = int.Parse(go.name);
            IsFristCard = false;
        }
        else
        {
            SecondCardName = go.transform.GetChild(0).GetComponent<Text>().text;
            IsFristCard = true;
            int SelfPos = int.Parse(go.name);
            if (FristCardName == SecondCardName& SelfPos != FristIndex)
            {               
                SecondCardPosX = (SelfPos / 15);
                SecondCardPosY = (SelfPos % 15);

                FirstCardPosX = (FristIndex / 15);
                FirstCardPosY = (FristIndex % 15);

                FindRoad();
            }
        }
    }

    private void FindRoad()
    {
        this.gameObject.transform.GetChild(1).GetChild((SecondCardPosX) * 15 + SecondCardPosY).GetChild(0).GetComponent<Text>().text = (-1).ToString();

        this.gameObject.transform.GetChild(1).GetChild((FirstCardPosX) * 15 + FirstCardPosY).GetChild(0).GetComponent<Text>().text = (-1).ToString();

        ClearCard();
    }



}
