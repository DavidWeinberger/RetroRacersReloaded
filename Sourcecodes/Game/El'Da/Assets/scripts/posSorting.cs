using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class posSorting : MonoBehaviour {
    public Text pos;
    public int playerAmount;
    private List<int> points = new List<int>();
    private List<string> ranks = new List<string>();
    private string[] colors = { "Blau", "Rot", "Grün", "Gelb" };



    // Use this for initialization
    void Start () {
        if (pos == null && GetComponent<Text>() != null)
        {
            pos = GetComponent<Text>();
        }
        for (int i = 0; i < playerAmount; i++)
        {
            points.Add(0);
            ranks.Add("Spieler " + (i + 1) + "("+colors[i]+")");
        }
        string ranking = "";
        for (int x = 0; x < ranks.Count; x++)
        {
            ranking += ranks[x] + "\n";
        }
        pos.text = ranking;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void updatePoints(int pointsIn, string player)
    {
        if (ranks[0].Contains(player))
        {
            points[0] += pointsIn;
        }
        else 
        {
            for (int i = 1; i < ranks.Count; i++)
            {
                if (ranks[i].Contains(player))
                {
                    points[i] += pointsIn;
                    if (points[i] > points[i-1])
                    {
                        int tmpPoints = points[i];
                        string tmpName = ranks[i];
                        points[i] = points[i - 1];
                        ranks[i] = ranks[i - 1];
                        points[i - 1] = tmpPoints;
                        ranks[i - 1] = tmpName;
                        if (i-2 >= 0 && points[i-1] > points[i - 2])
                        {
                            tmpPoints = points[i-1];
                            tmpName = ranks[i-1];
                            points[i-1] = points[i - 2];
                            ranks[i-1] = ranks[i - 2];
                            points[i - 2] = tmpPoints;
                            ranks[i - 2] = tmpName;
                            if (i - 3 >= 0 && points[i-2] > points[i - 3])
                            {
                                tmpPoints = points[i - 2];
                                tmpName = ranks[i - 2];
                                points[i - 2] = points[i - 3];
                                ranks[i - 2] = ranks[i - 3];
                                points[i - 3] = tmpPoints;
                                ranks[i - 3] = tmpName;
                            }
                        }
                    }
                }
            }
        }
        string ranking = "";
        for (int i = 0; i < ranks.Count; i++)
        {
            ranking += ranks[i] + "\n";
        }
        pos.text = ranking;
    }

    public void printBestList()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var dirPath = System.IO.Path.Combine(appDataPath, @"\Roaming\RetroRacerReloaded\");
        if (!Directory.Exists(dirPath))
            Directory.CreateDirectory(dirPath);
        File.WriteAllText(dirPath + "\\Place.txt", pos.text);
    }
}
