using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Timers;
using System.Threading;
using System.Diagnostics;

public class CheckpointControllerM : MonoBehaviour
{
    private GameObject curCh;
    private int curRou;
    public GameObject ch1;
    public GameObject ch2;
    public GameObject ch3;
    public GameObject ch4;
    public Text roundText;
    private static int maxRou;
    private static bool fir = false;
    private static bool sec = false;
    private static bool thr = false;
    private int points;
    private CheckpointControllerListM ccL;

    public int CurRou
    {
        get
        {
            return curRou;
        }
    }

    public GameObject CurCh
    {
        get
        {
            return curCh;
        }
    }

    // Use this for initialization
    void Start()
    {
        ccL = GameObject.Find("CheckpointControllerList").GetComponent<CheckpointControllerListM>();
        ch1 = GameObject.FindGameObjectWithTag("ch1");
        ch2 = GameObject.FindGameObjectWithTag("ch2");
        ch3 = GameObject.FindGameObjectWithTag("ch3");
        ch4 = GameObject.FindGameObjectWithTag("ch4");
        if (roundText == null && GetComponent<Text>() != null)
        {
            roundText = GetComponent<Text>();
        }
        if ((this.name == "CheckpointController_3" && ccL.Player >= 3) || (this.name == "CheckpointController_4" && ccL.Player == 4)|| this.name == "CheckpointController_1" || this.name == "CheckpointController_2" )
        { 
            curCh = ch1;
        }
        else
        {
            curCh = null;
        }
        curRou = 0;
        maxRou = 0;
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateCheckpoints(GameObject curCheck)
    {
        if(this.Points() < this.maxPoints())
        {
            if (curCheck == curCh)
            {
                if (curCh == ch1)
                {
                    curRou++;
                    if (curRou > maxRou)
                    {
                        maxRou = curRou;
                        roundText.text = "Runde " + maxRou + " von " + ccL.Rounds;
                    }                   
                }
                if (curCh == ch1)
                {
                    curCh = ch2;
                }
                else if (curCh == ch2)
                {
                    curCh = ch3;
                }
                else if (curCh == ch3)
                {
                    curCh = ch4;
                }
                else if (curCh == ch4)
                {
                    curCh = ch1;
                }
            }
        }
    }

    public int Points()
    {
        int count = ccL.Player;
        if (count < 4)
        {
            thr = true;
        }
        if(count < 3)
        {
            sec = true;
        }
        if(curRou == ccL.Rounds)
        {
            if (points <= int.MaxValue - 5)
            {
                if (fir == false)
                {
                    fir = true;
                    points = int.MaxValue;
                }
                else if (sec == false)
                {
                    sec = true;
                    points = int.MaxValue - 1;
                }
                else if (thr == false)
                {
                    thr = true;
                    points = int.MaxValue - 2;
                }
                else
                {
                    print("Quiting");					
                    Application.Quit();
                }
            }
        }
        else
        {
            points = (curRou) * 4;
            if (curCh == ch2)
            {
                points += 1;
            }
            else if (curCh == ch3)
            {
                points += 2;
            }
            else if (curCh == ch4)
            {
                points += 3;
            }            
        }
        return points;
    }

    public int maxPoints()
    {
        int temp = ccL.Rounds * 4 + 1;
        return temp;
    }
}
