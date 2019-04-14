using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleCost : MonoBehaviour
{
    private int castleCost;
    Text theCost;

	void Start ()
    {
        theCost = GetComponent<Text>();
        castleCost = CastleStorage.minCarNeededD;
	}
	
	void Update ()
    {
        theCost.text = castleCost + " Carrots";
	}
}
