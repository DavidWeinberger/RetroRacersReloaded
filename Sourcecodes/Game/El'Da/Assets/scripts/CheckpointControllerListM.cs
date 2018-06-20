using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Timers;
using System.Threading;
using System.Diagnostics;

public class CheckpointControllerListM : MonoBehaviour
{
    public int rounds;
    public string mapName;
    public CheckpointControllerM cc1;
    public CheckpointControllerM cc2;
    public CheckpointControllerM cc3;
    public CheckpointControllerM cc4;
    public int player;

    public int Player
    {
        get
        {
            return player;
        }
    }

    public int Rounds
    {
        get
        {
            return rounds;
        }
    }

    void Start()
    {        
        cc1 = GameObject.Find("CheckpointController_1").GetComponent<CheckpointControllerM>();
        cc2 = GameObject.Find("CheckpointController_2").GetComponent<CheckpointControllerM>();
        cc3 = GameObject.Find("CheckpointController_3").GetComponent<CheckpointControllerM>();
        cc4 = GameObject.Find("CheckpointController_4").GetComponent<CheckpointControllerM>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateCheckpoints(GameObject curCheck,String tag)
    {
        if (tag == "p1")
        {
            cc1.UpdateCheckpoints(curCheck);
        }
        else if(tag == "p2")
        {
            cc2.UpdateCheckpoints(curCheck);
        }
        else if(tag == "p3")
        {
            cc3.UpdateCheckpoints(curCheck);
        }
        else if(tag == "p4")
        {
            cc4.UpdateCheckpoints(curCheck);
        }
    }

    public String Points()
    {
        int[] p = new int[player];
        p[0] = cc1.Points();
        p[1] = cc2.Points();

        if (player >= 3)
        {
            p[2] = cc3.Points();
        }
        if (player == 4)
        {
            p[3] = cc4.Points();
        }

        Array.Sort(p);
        String temp = "";
        bool one = false;
        bool two = false;
        bool three = false;
        bool four = false;
        for (int i = player-1; i >= 0; i--)
        {
            if (i == player-1)
            {
                temp += "Erster: ";
            }
            else if (i == player - 2)
            {
                temp += "Zweiter: ";
            }
            else if (i == player - 3)
            {
                temp += "Dritter: ";
            }
            else if (i == 0 || i == player -4)
            {
                temp += "Letzter: ";
            }
            if (p[i] == cc1.Points() && one == false)
            {
                temp += "Spieler1(Blau)";
                one = true;
            }
            else if (p[i] == cc2.Points() && two == false)
            {
                temp += "Spieler2(Rot)";
                two = true;
            }
            else if (p[i] == cc3.Points() && player >= 3 && three == false)
            {
                temp += "Spieler3(Gruen)";
                three = true;
            }
            else if (p[i] == cc4.Points() && player == 4 && four == false)
            {
                temp += "Spieler4(Gelb)";
                four = true;
            }
            temp += "\n";
			
        }
		var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var dirPath = System.IO.Path.Combine(appDataPath, @"\Roaming\RetroRacerReloaded\");
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            File.WriteAllText(dirPath + "\\Place.txt", temp);
        return temp;
    }
}