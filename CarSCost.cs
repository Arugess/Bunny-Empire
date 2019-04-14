using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This changes how much the next upgrade will cost to purchase.//

public class CarSCost : MonoBehaviour
{
    public static int csCost;
    Text theCost;

	void Start ()
    {
        theCost = GetComponent<Text>();
        csCost = 10;
	}
	
	void Update ()
    {
        theCost.text = csCost + " Carrots";
	}

    public static void UpdateCSCost()
    {
        switch(CarrotStorage.lvlUnlockC)
        {
            case 1:
                csCost = 100;
                break;
            case 2:
                csCost = 200;
                break;
            case 3:
                csCost = 400;
                break;
            default:
                break;


        }
    }
}
