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


public class CheckPointControllerSingle : MonoBehaviour
{
    public string mapName;
    public int rounds;
    private GameObject curCh;
    private int curRou;
    private String[] roundTimes;
    public GameObject ch1;
    public GameObject ch2;
    public GameObject ch3;
    public GameObject ch4;
    private Stopwatch stopWatch;
    public Text bestRound;
    public Text lastRound;
    public Text roundText;
    private TimeSpan bestTime;
    private int bestLapTime;
    private string bestLapTimeString;
    private string path;
    private string dirPath;


    // Use this for initialization
    void Start()
    {
        
        path = Directory.GetCurrentDirectory();
        
        string[] pathParts = path.Split('\\');
        path = "";
        for (int x = 0; x < pathParts.Length-2; x++)
        {
            path += pathParts[x] + "\\";
        }
        dirPath = path;
        path += "Ranking";
        bestLapTime = 0;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        path += "\\" + mapName + ".txt";
        //File.WriteAllText(@"E:\Users\david\Desktop\filestest.txt", path);
        if (!File.Exists(path))
        {
            //File.WriteAllText(@"E:\Users\david\Desktop\filestest.txt", path);
            File.Create(path);
        }
        
        
        
        stopWatch = new Stopwatch();
        roundTimes = new String[rounds];
        ch1 = GameObject.FindGameObjectWithTag("ch1");
        ch2 = GameObject.FindGameObjectWithTag("ch2");
        ch3 = GameObject.FindGameObjectWithTag("ch3");
        ch4 = GameObject.FindGameObjectWithTag("ch4");
        if (bestRound == null && GetComponent<Text>() != null)
        {
            bestRound = GetComponent<Text>();
        }
        if (lastRound == null && GetComponent<Text>() != null)
        {
            lastRound = GetComponent<Text>();
        }
        if (roundText == null && GetComponent<Text>() != null)
        {
            roundText = GetComponent<Text>();
        }
        curCh = ch1;
        curRou = 0;
        bestTime = TimeSpan.MaxValue;

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void enterList()
    {
        //File.WriteAllText(@"E:\Users\david\Desktop\teste.txt", bestLapTimeString + "    " + bestLapTime);
        if (bestLapTime != 0 && bestLapTimeString != null)
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var dirPath = System.IO.Path.Combine(appDataPath, @"\Roaming\RetroRacerReloaded\time.txt");
            File.WriteAllText(dirPath, mapName+";"+ bestLapTimeString + ";" + bestLapTime);
            
        }
        
    }

    public void UpdateCheckpoints(GameObject curCheck)
    {
        
        
        if (curCheck == curCh)
        {
            if (curCh == ch1)
            {
                curRou++;
                if (curRou == 1)
                {
                    roundText.text = "Runde 1 von " + rounds;
                    stopWatch.Start();
                }
                else
                {
                    roundText.text = "Runde " + curRou + " von " + rounds;
                    TimeSpan ts = stopWatch.Elapsed;
                    string elapsedTime = String.Format("{0:00}:{1:00}.{2:00}",
                        ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);
                    string testString = String.Format("{0:00}{1:00}{2:00}",
                        ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);
                    if (bestLapTime == 0 || Convert.ToInt32(testString) < bestLapTime)
                    {
                        bestLapTime = Convert.ToInt32(testString);
                        bestLapTimeString = elapsedTime;
                        bestRound.text = "Beste Runde: " + elapsedTime;
                        bestTime = ts;
                    }
                    roundTimes[curRou - 2] = elapsedTime;
                    
                    lastRound.text = "Letzte Runde: " + elapsedTime;
                    
                    stopWatch.Reset();
                    stopWatch.Start();
                }
                if (curRou - 1 >= rounds)
                {
                    enterList();

                    Application.Quit();
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
