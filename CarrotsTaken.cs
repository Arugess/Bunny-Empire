using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This shows the number of carrots that have been taken by the enemy through text on the screen.//

public class CarrotsTaken : MonoBehaviour
{
    public static int carrotsTaken;
    Text cTaken;

	// Use this for initialization
	void Start ()
    {
        cTaken = GetComponent<Text>();
        carrotsTaken = 0;	
	}
	
	// Update is called once per frame
	void Update ()
    {
        cTaken.text = "Carrots Taken: " + carrotsTaken + "/1000";
	}
}
