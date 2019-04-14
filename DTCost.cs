using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DTCost : MonoBehaviour
{
    public static int dtCost;
    Text theCost;

    void Start()
    {
        theCost = GetComponent<Text>();
        dtCost = 50;
    }

    void Update()
    {
        theCost.text = dtCost + " Carrots";
    }

    public static void UpdateDTCost()
    {
        switch (DTowerStorage.lvlUnlockD)
        {
            case 1:
                dtCost = 100;
                break;
            case 2:
                dtCost = 150;
                break;
            case 3:
                dtCost = 200;
                break;
            default:
                break;
        }
    }
}
