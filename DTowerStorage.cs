using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This allows the player to purchase defense towers (DT) that spawn at certain points in the level.//

public class DTowerStorage : MonoBehaviour
{
    private bool pBuyingD, dtBought;
    public Transform[] cSpawnD, DTSpawn;
    public GameObject cPrefabD, DTower;
    public static int lvlUnlockD;
    private int minCarNeededD;
    public Transform costText;

    AudioSource purchaseSound;

    void Start()
    {
        pBuyingD = false;
        lvlUnlockD = 0;
        minCarNeededD = 50;
        purchaseSound = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (pBuyingD && Input.GetKeyDown(KeyCode.E))
        {

            //This allows the player to unlock higher upgrade levels when a minimum amount of
            //carrots has been met
            if (CarrotScore.carrotScore >= minCarNeededD && lvlUnlockD <= 3)
            {
                //Spawns the tower model and tower prefab in a position equal to the lvlUnlock
                //and incraeses the lvlUnlock
                Instantiate(cPrefabD, cSpawnD[lvlUnlockD].position, Quaternion.identity);
                Instantiate(DTower, DTSpawn[lvlUnlockD].position, Quaternion.identity);
                lvlUnlockD += 1;
                purchaseSound.Play();

                //Changes the minimum amount of carrots needed to unlock the next upgrade
                CarrotScore.carrotScore -= minCarNeededD;
                switch (lvlUnlockD)
                {
                    case 1:
                        minCarNeededD = 100;
                        break;
                    case 2:
                        minCarNeededD = 150;
                        break;
                    case 3:
                        minCarNeededD = 200;
                        break;
                    default:
                        break;
                }

                //The initiates the void function in the CarrotScore script
                CarrotScore.UpdateCarrotScore();
                DTCost.UpdateDTCost();
                if(lvlUnlockD >= 4)
                {
                    dtBought = true;
                }
            }
        }
        if (dtBought == true)
        {
            costText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pBuyingD = true;

            if (costText.gameObject.activeInHierarchy == false && !dtBought)
            {
                costText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            pBuyingD = false;
            costText.gameObject.SetActive(false);

        }
    }
}
