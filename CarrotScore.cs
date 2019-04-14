using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarrotScore : MonoBehaviour
{
    //Sets the carrot score and gets the text attached
    public static int carrotScore;
    public static int maxCarrots;
    Text score;

	void Start ()
    {
        //Gets the text component and sets max carrot score
        score = GetComponent<Text>();
        carrotScore = 5;
        maxCarrots = 10;
	}
	
	void Update ()
    {
        //Sets the text to a certain string in "" and adds the carrotScore 
        score.text = "Carrots: " + carrotScore + "/" + maxCarrots;

        if(carrotScore < 0)
        {
            carrotScore = 0;
        }
        if (carrotScore > maxCarrots)
        {
            carrotScore = maxCarrots;
        }
	}


    //This function changes the text for the max amount of carrots a player can have
    //By this void being a public static, the maxCarrots int must also be a public Static
    public static void UpdateCarrotScore()
    {
        switch (CarrotStorage.lvlUnlockC)
        {
            case 1:
                maxCarrots = 250;
                break;
            case 2:
                maxCarrots = 500;
                break;
            case 3:
                maxCarrots = 750;
                break;
            case 4:
                maxCarrots = 1000;
                break;
            default:
                break;
        }
    }
}
