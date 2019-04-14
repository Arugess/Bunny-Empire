using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This allows the player to plant carrots, and makes the carrots grow.//
//When the player touches the carrots they gain more carrots to spend.//

public class GroundPatch : MonoBehaviour
{

    public GameObject carrot;
    private GameObject newCarrot;
    public Transform parentTrans;

    private bool trigEntered;
    private bool carrotPicked;

    //These variables are only activated once when the player steps on the ground patch 
    public Material newMaterial;
    private bool pActivated;

    Vector3 carrotPos;

    AudioSource carPickSound;

    void Start ()
    {
        //Starts these variables as false
        pActivated = false;
        carrotPicked = false;

        carPickSound = GetComponent<AudioSource>();

	}


	void Update ()
    {
        //This initiates the coroutine by the carrot being picked, the patch being activated, 
        //and the player outside the patch trigger 
		if(carrotPicked == true && pActivated == true && trigEntered == false)
        {

            StartCoroutine("SpawnCarrot");
        }

        //This will initiate the coroutine to begin the carrot spawning when the player
        //enters the and presses "E", and the 
        if(trigEntered == true && pActivated == false && CarrotScore.carrotScore >= 1 && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine("ActivatePatch");

        }


    }
    private void OnTriggerEnter(Collider other)
    {

        //When the player enters the patches trigger they gain a carrot
        if (other.tag == "Player")
        {
            trigEntered = true;

            if (pActivated == true)
            {
                //This only allows carrots to be picked if the max amount of carrot has not been reached
                if (carrotPicked == false && CarrotScore.carrotScore < CarrotScore.maxCarrots)
                {

                    carrotPicked = true;
                    Destroy(newCarrot.gameObject);

                    MainLevelStuff.carrotCount += 1;
                    CarrotScore.carrotScore += 2;
                    
                    carPickSound.Play();
                }
            }           
        }
        if ( other.tag == "Ally")
        {

            if (pActivated == true)
            {
                //This only allows carrots to be picked if the max amount of carrot has not been reached
                if (carrotPicked == false && CarrotScore.carrotScore < CarrotScore.maxCarrots)
                {

                    carrotPicked = true;
                    Destroy(newCarrot.gameObject);

                    CarrotScore.carrotScore += 2;

                    carPickSound.Play();
                }
            }
        }



        //When the enemy enters the trigger they take a carrot
        if (other.tag == "Enemy")
        {
            if (pActivated == true)
            {

                if (carrotPicked == false)
                {
                    carPickSound.Play();
                    carrotPicked = true;
                    CarrotsTaken.carrotsTaken += 1;
                    Destroy(newCarrot.gameObject);
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            trigEntered = false;
        }
    }

    IEnumerator SpawnCarrot()
    {

        yield return new WaitForSeconds(5.0f);

        //This sets the carrot's position to the ground patch parent
        Vector3 carrotPos = new Vector3(this.transform.position.x,
                                        this.transform.position.y,
                                        this.transform.position.z);        
        //This spawns the carrot
        newCarrot = (GameObject)Instantiate(carrot, carrotPos, Quaternion.identity);
        //This sets the carrot's transform to the parent ground patch
        newCarrot.transform.parent = parentTrans;

        //These stop this coroutine from continuosly going
        carrotPicked = false;
        StopCoroutine("SpawnCarrot");
    }

    //This changes the material of the ground patch, subtracts a carrot from your score,
    //and the sets the patch to start instantiating carrots
    IEnumerator ActivatePatch()
    {
        yield return
        
        GetComponent<Renderer>().material = newMaterial;

        CarrotScore.carrotScore -= 1;

        CastleStorage.gPatches += 1;

        this.pActivated = true;
        this.carrotPicked = true;

        StopCoroutine("ActivatePatch");
    }
}
