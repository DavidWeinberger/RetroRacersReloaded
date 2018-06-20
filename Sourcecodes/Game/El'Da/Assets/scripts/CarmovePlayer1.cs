using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Timers;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class CarmovePlayer1 : MonoBehaviour {
    private Stopwatch stopwatch = new Stopwatch();
    private Stopwatch stopwatchClosed = new Stopwatch();
    private bool isOpen = false;
    private Rect windowRect = new Rect(0, 0, 0, 0);
    public float Geschwindigkeit;
	public float Beschleunigung;
    public float Drehgeschwindigkeit;
    static float rot = 0;
    public Rigidbody2D rb;
    private bool collided;
    private float aktuelleGeschwindigkeit = 0;
    private bool isBreacking = false;
    private string path;
    //public CheckPointControllerSingle test;

    void Start()
	{
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody2D>();
        path = Directory.GetCurrentDirectory();
        string[] pathParts = path.Split('\\');
        //File.WriteAllText(@"E:\Users\david\Desktop\log2.txt", path);
        path = "";
        for (int x = 0; x < pathParts.Length - 2; x++)
        {
            path += pathParts[x] + "\\";
        }

        
    }

	void Update()
	{
        
        if (EingabeÜberprüfen())
		{
			if (aktuelleGeschwindigkeit < Geschwindigkeit)
			{
				aktuelleGeschwindigkeit += Beschleunigung * Time.deltaTime;
			}
            
            rb.rotation = rot;
            this.rb.MoveRotation(rb.rotation);
            Vector3 tempVec = transform.up;
            float x = tempVec.x;
            float y = tempVec.y;
            this.rb.MovePosition(rb.position + new Vector2(x,y)* aktuelleGeschwindigkeit* Time.deltaTime);
            
		}
		else
		{
            if (aktuelleGeschwindigkeit > 0)
            {
                if (isBreacking)
                {
                    aktuelleGeschwindigkeit -= Beschleunigung * Time.deltaTime * 1f;
                }
                {
                    aktuelleGeschwindigkeit -= Beschleunigung * Time.deltaTime * 0.5f;
                }
                
            }

            rb.rotation = rot;
            this.rb.MoveRotation(rb.rotation);
            Vector3 tempVec = transform.up;
            float x = tempVec.x;
            float y = tempVec.y;
            
            this.rb.MovePosition(rb.position + new Vector2(x, y) * aktuelleGeschwindigkeit * Time.deltaTime);

        }
    }
    private void OnGUI()
    {
        if (isOpen == true)
        {
            GUI.Box(new Rect(Screen.width / 2 - 125, Screen.height / 2 - 200, 250, 50), "Verlassen?");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            if (GUI.Button(new Rect(Screen.width / 2 - 125, Screen.height / 2 - 200, 50, 50), "Ja"))
            {
                //test.enterList();
                Application.Quit();
            }
            if (GUI.Button(new Rect(Screen.width / 2 + 125-50, Screen.height / 2 - 200, 50, 50), "Nein"))
            {
                isOpen = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                stopwatch.Stop();
                stopwatch.Reset();
                stopwatchClosed.Start();
            }
        }
        else
        {
            Time.timeScale = 1;
        }

    }

    private bool EingabeÜberprüfen()
	{
		bool eingabe = false;
        float localRot = rot;

        if (Input.GetKey(KeyCode.Escape) && isOpen == false && (!stopwatchClosed.IsRunning || stopwatchClosed.Elapsed.Milliseconds > 300))
        {

            isOpen = true;
            stopwatch.Start();
            stopwatchClosed.Stop();
            stopwatchClosed.Reset();
            
            

        }
        else if (Input.GetKey(KeyCode.Escape) && isOpen == true && stopwatch.Elapsed.Milliseconds > 300)
        {
            //test.enterList();
            isOpen = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            stopwatch.Stop();
            stopwatch.Reset();
            stopwatchClosed.Start();
            //Application.Quit();

        }
        if (Input.GetKey(KeyCode.RightArrow))
		{
            localRot = localRot - Drehgeschwindigkeit;
            localRot = localRot % 360;
            
            eingabe = false;
            
		}
        if (Input.GetKey(KeyCode.LeftArrow))
		{
            localRot = localRot + Drehgeschwindigkeit;
            if ((int) localRot < 0)
            {
                localRot += 360;
            }            
            localRot = localRot % 360;
			eingabe = false;
      		}
        if (Input.GetKey(KeyCode.UpArrow))
		{
            
			eingabe = true;
		}
        if (Input.GetKey(KeyCode.DownArrow))
		{
            isBreacking = true;

            eingabe = false;
        }
       
        rot = localRot;
       
        return eingabe;
	}
}
