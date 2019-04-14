using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotStorage : MonoBehaviour
{
    private bool pBuyingC, csBought;
    public Transform[] cSpawnC;
    public GameObject cPrefabC;
    public static int lvlUnlockC;
    public static int minCarNeededC;
    public Transform costText;

    AudioSource purchaseSound;

	void Start ()
    {
        pBuyingC = false;
        lvlUnlockC = 0;
        minCarNeededC = 10;

        purchaseSound = GetComponent<AudioSource>();
	}
	
	void Update ()
    {
		if(pBuyingC && Input.GetKeyDown(KeyCode.E))
        {

            //This allows the player to unlock higher upgrade levels when a minimum amount of
            //carrots has been met
            if(CarrotScore.carrotScore >= minCarNeededC && lvlUnlockC <= 3)
            {
                //Spawns the carrot prefab in a position equal to the lvlUnlock and increases
                //the lvlUnlock
                Instantiate(cPrefabC, cSpawnC[lvlUnlockC].position, Quaternion.identity);
                lvlUnlockC += 1;
                purchaseSound.Play();

                //Changes the minimum amount of carrots needed to unlock the next upgrade
                CarrotScore.carrotScore -= minCarNeededC;
                switch(lvlUnlockC)
                {
                    case 1:
                        minCarNeededC = 100;
                        break;
                    case 2:
                        minCarNeededC = 200;
                        break;
                    case 3:
                        minCarNeededC = 400;
                        break;
                    default:
                        break;
                }

                //The initiates the void function in the CarrotScore script
                CarrotScore.UpdateCarrotScore();
                CarSCost.UpdateCSCost();
                if (lvlUnlockC >= 4)
                {
                    csBought = true;
                }
            }

        }

        if(csBought == true)
        {
            costText.gameObject.SetActive(false);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            pBuyingC = true;

            if (costText.gameObject.activeInHierarchy == false && !csBought)
            {
                costText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag =="Player")
        {
            pBuyingC = false;
            costText.gameObject.SetActive(false);

        }
    }
}
