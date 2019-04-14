using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABStorage : MonoBehaviour
{
    private bool pBuyingAB, abBought;
    public Transform[] cSpawnAB, ABSpawn;
    public GameObject cPrefabAB, ABunny;
    public static int lvlUnlockAB, abCount;
    private int minCarNeededAB;
    public Transform costText;

    public static bool canRespawn;
    private int abRespawn;

    AudioSource purchaseSound;

    void Start()
    {
        pBuyingAB = false;
        lvlUnlockAB = 0;
        minCarNeededAB = 100;
        canRespawn = false;
        purchaseSound = GetComponent<AudioSource>();


        InvokeRepeating("HowManyBunnies", 0f, 0.5f);
    }

    void HowManyBunnies()
    {
        if (abCount < lvlUnlockAB)
        {
            StartCoroutine("RespawnAlly");
            Debug.Log("less than 4");
        }
    }

    void Update()
    {
        if (pBuyingAB && Input.GetKeyDown(KeyCode.E))
        {

            //This allows the player to unlock higher upgrade levels when a minimum amount of
            //carrots has been met
            if (CarrotScore.carrotScore >= minCarNeededAB && lvlUnlockAB <= 3)
            {
                //Spawns the ally model and ally bunny prefab in a position equal to the lvlUnlock 
                //and increases the lvlUnlock
                Instantiate(cPrefabAB, cSpawnAB[lvlUnlockAB].position, Quaternion.identity);
                Instantiate(ABunny, ABSpawn[lvlUnlockAB].position, ABSpawn[lvlUnlockAB].rotation);
                lvlUnlockAB += 1;
                abCount = lvlUnlockAB;
                purchaseSound.Play();

                //Changes the minimum amount of carrots needed to unlock the next upgrade
                CarrotScore.carrotScore -= minCarNeededAB;
                switch (lvlUnlockAB)
                {
                    case 1:
                        minCarNeededAB = 150;
                        break;
                    case 2:
                        minCarNeededAB = 200;
                        break;
                    case 3:
                        minCarNeededAB = 250;
                        break;
                    default:
                        break;
                }

                //The initiates the void function in the CarrotScore script
                CarrotScore.UpdateCarrotScore();
                ABCost.UpdateABCost();

                if(lvlUnlockAB >= 4)
                {
                    abBought = true;
                }
            }

        }

        if(abBought == true)
        {
            costText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pBuyingAB = true;

            if (costText.gameObject.activeInHierarchy == false && !abBought)
            {
                costText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            pBuyingAB = false;

            costText.gameObject.SetActive(false);
        }
    }
    
    IEnumerator RespawnAlly()
    {
        //canRespawn = false;
        abCount += 1;
        yield return new WaitForSeconds(10);
        abRespawn = Random.Range(0, 3);
        Instantiate(ABunny, ABSpawn[abRespawn].position, ABSpawn[abRespawn].rotation);
        //StopCoroutine("RespawnAlly");
    }
}
