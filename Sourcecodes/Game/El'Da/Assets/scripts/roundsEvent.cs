using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class roundsEvent : MonoBehaviour {
    public int rounds;
    public Text roundText;
    private int curRnd = 1;

    public int Rounds
    {
        get
        {
            return rounds;
        }
    }

    // Use this for initialization
    void Start () {
        if (roundText == null && GetComponent<Text>() != null)
        {
            roundText = GetComponent<Text>();
        }
        roundText.text = "Runde 1 von " + Rounds;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void roundUpdate(int round)
    {
        if (round > curRnd)
        {
            roundText.text = "Runde "+round+" von " + Rounds;
            curRnd = round;
        }
    }
}
