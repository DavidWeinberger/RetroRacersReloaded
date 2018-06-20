using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Timers;
using System.Threading;
using System.Diagnostics;

public class CheckpointM : MonoBehaviour
{

    public CheckPointControllerMultiplayer p1;
    public CheckPointControllerMultiplayer p2;
    public CheckPointControllerMultiplayer p3;
    public CheckPointControllerMultiplayer p4;
    public Text positionen;

    // Use this for initialization
    void Start () {
        
        if (positionen == null && GetComponent<Text>() != null)
        {
            positionen = GetComponent<Text>();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "p1")
        {
            p1.UpdateCheckpoints(this.gameObject);
        }
        else if(other.tag == "p2")
        {
            p2.UpdateCheckpoints(this.gameObject);
        }
        else if (other.tag == "p3" && p3 != null)
        {
            p3.UpdateCheckpoints(this.gameObject);
        }
        else if (other.tag == "p4" && p4 != null)
        {
            p4.UpdateCheckpoints(this.gameObject);
        }
    }
}
