using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CastleStorage : MonoBehaviour
{
    private bool pBuyingCas, casBought;
    public Transform CasSpawn;
    public GameObject CastlePrefab;
    public static int minCarNeededD, gPatches;
    public Transform costText, pActive;

    AudioSource purchaseSound;


    void Start()
    {
        casBought = false;
        pBuyingCas = false;
        minCarNeededD = 1000;
        purchaseSound = GetComponent<AudioSource>();
        

    }

    void Update()
    {
        if (pBuyingCas && Input.GetKeyDown(KeyCode.E))
        {

            //This allows the player to unlock higher upgrade levels when a minimum amount of
            //carrots has been met
            if (CarrotScore.carrotScore >= minCarNeededD && gPatches == 80)
            {
                purchaseSound.Play();

                //Spawns the tower model and tower prefab in a position equal to the lvlUnlock
                //and incraeses the lvlUnlock
                Instantiate(CastlePrefab, CasSpawn.position, Quaternion.identity);
                
                //Changes the minimum amount of carrots needed to unlock the next upgrade
                CarrotScore.carrotScore -= minCarNeededD;
                                
                casBought = true;

                //The initiates the void function in the CarrotScore script
                CarrotScore.UpdateCarrotScore();

                SceneManager.LoadScene("WinScreen");
            }

        }

        if(casBought == true)
        {
            costText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pBuyingCas = true;

            if(costText.gameObject.activeInHierarchy == false && !casBought)
            {
                costText.gameObject.SetActive(true);
            }

            if (pActive.gameObject.activeInHierarchy == false && !casBought)
            {
                pActive.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            pBuyingCas = false;

            costText.gameObject.SetActive(false);
            pActive.gameObject.SetActive(false);

        }
    }
}
