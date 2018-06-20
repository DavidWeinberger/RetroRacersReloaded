using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using UnityEngine.UI;
using System.Timers;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;


public class CheckPointControllerMultiplayer : MonoBehaviour
{
    public posSorting pos;
    public string name = "";
    private int rounds;
    public roundsEvent roundsEv;
    private GameObject curCh;
    private int curRou;
    private String[] roundTimes;
    public GameObject ch1;
    public GameObject ch2;
    public GameObject ch3;
    public GameObject ch4;
    private Stopwatch stopWatch;
    
    private TimeSpan bestTime;
    private int bestLapTime;
    private string bestLapTimeString;
    private string path;
    private string dirPath;
    private int points = 0;

    // Use this for initialization
    void Start()
    {
        ch1 = GameObject.FindGameObjectWithTag("ch1");
        ch2 = GameObject.FindGameObjectWithTag("ch2");
        ch3 = GameObject.FindGameObjectWithTag("ch3");
        ch4 = GameObject.FindGameObjectWithTag("ch4");
        rounds = roundsEv.Rounds;
        curCh = ch1;
        curRou = 0;



    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateCheckpoints(GameObject curCheck)
    {
        
        
        if (curCheck == curCh)
        {
            if (curCh == ch1)
            {
                curRou++;
                roundsEv.roundUpdate(curRou);

                if (curRou - 1 >= rounds)
                {
                    pos.printBestList();

                    Application.Quit();
                }
            }
            if (curCh == ch1)
            {
                updateRanking();
                points++;
                curCh = ch2;
            }
            else if (curCh == ch2)
            {
                updateRanking();
                points++;
                curCh = ch3;
            }
            else if (curCh == ch3)
            {
                updateRanking();
                points++;
                curCh = ch4;
            }
            else if (curCh == ch4)
            {
                updateRanking();
                points++;
                curCh = ch1;
            }
        }
    }

    public void updateRanking()
    {
        pos.updatePoints(1, name);
    }
}
