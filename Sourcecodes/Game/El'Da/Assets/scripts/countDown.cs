using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;


public class countDown : MonoBehaviour {
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;
    private float timeStart = 3;
    private float timeRemaining = 3;
    private bool active = true;
    private Stopwatch watch = new Stopwatch();
    private Stopwatch watchSec = new Stopwatch();
    private GUIStyle style = new GUIStyle();
	// Use this for initialization
	void Start () {
        Time.timeScale = 0;
        watchSec.Start();
        style.fontSize = 50;
        style.normal.textColor = Color.red;
        if (p1 != null)
        {
            p1.GetComponent<CarmovePlayer2>().enabled = false;
        }
        if (p2 != null)
        {
            p2.GetComponent<CarmovePlayer1>().enabled = false;
        }
        if (p3 != null)
        {
            p3.GetComponent<CarmovePlayer3>().enabled = false;
        }
        if (p4 != null)
        {
            p4.GetComponent<CarmovePlayer4>().enabled = false;
        }

    }
	
	// Update is called once per frame
	void Update () {
        timeRemaining = timeStart - watchSec.Elapsed.Seconds;
    }

    private void OnGUI()
    {
        if (timeRemaining >= 1)
        {
            GUI.Label(new Rect(Screen.width/2, Screen.height/2, 200, 100), ((int)timeRemaining).ToString(), style);
            Time.timeScale = 0;
        }
        else if(active)
        {
            if (!watch.IsRunning)
            {
                watchSec.Stop();
                watch.Start();
            }
            if (watch.Elapsed.Seconds == 1)
            {
                active = false;
                watch.Stop();
            }

            Time.timeScale = 1;
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200, 100), "Los!!!", style);
            if (p1 != null)
            {
                p1.GetComponent<CarmovePlayer2>().enabled = true;
            }
            if (p2 != null)
            {
                p2.GetComponent<CarmovePlayer1>().enabled = true;
            }
            if (p3 != null)
            {
                p3.GetComponent<CarmovePlayer3>().enabled = true;
            }
            if (p4 != null)
            {
                p4.GetComponent<CarmovePlayer4>().enabled = true;
            }
        }
    }
}
