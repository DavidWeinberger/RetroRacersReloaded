using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointE : MonoBehaviour
{

    public CheckPointControllerSingle checkpointController;

    // Use this for initialization
    void Start () {
        checkpointController = GameObject.Find("CheckpointController").GetComponent<CheckPointControllerSingle>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            checkpointController.UpdateCheckpoints(this.gameObject);
        }
    }
}
