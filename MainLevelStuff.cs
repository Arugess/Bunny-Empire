using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//This starts the main level.//
//This script will also keep track of how many carrots a player has picked.//
//It will generate enemies when a certain amount hve been obtained.//
//the scritp will also cause the player to lose when too many carrots have been stolen.//

public class MainLevelStuff : MonoBehaviour
{
    public static bool moveCam, playerDied;

    public GameObject thePlayer;
    public Transform enemySpawn, theRespawn;

    public Transform canvas, canvas2;

    public Transform[] eSpawn;
    private int whereToSpawn;
    private int enemyCount, e2Count, e3Count;
    public static int carrotCount;
    public GameObject eBunny1;

    AudioSource bMusic;


    void Start()
    {

        playerDied = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        moveCam = true;

        carrotCount = 0;

        InvokeRepeating("RandomizeSpawn", 0f, 1f);
        InvokeRepeating("CheckPlayer", 0f, 0.5f);

    }

    void CheckPlayer()
    {
        if (playerDied)
        {
            StartCoroutine("RespawnPlayer");
        }
    }

    void RandomizeSpawn()
    {
        whereToSpawn = Random.Range(0, 15);
        if(carrotCount >= 50)
        {
            Instantiate(eBunny1, eSpawn[whereToSpawn].position, eSpawn[whereToSpawn].rotation);

            enemyCount += 1;           

            if (enemyCount >= 5)
            {
                e2Count += 1;
                carrotCount = 0;
                enemyCount = 0;
            }
        }

        if(e2Count >= 5)
        {
            Instantiate(eBunny1, eSpawn[whereToSpawn].position, eSpawn[whereToSpawn].rotation);

            enemyCount += 1;

            if (enemyCount >= 10)
            {
                e3Count += 1;
                e2Count = 0;
                enemyCount = 0;
            }
        }

        if (e3Count >= 3)
        {
            Instantiate(eBunny1, eSpawn[whereToSpawn].position, eSpawn[whereToSpawn].rotation);

            enemyCount += 1;

            if (enemyCount >= 15)
            {
                e3Count = 0;
                enemyCount = 0;
            }
        }

    }

    void Update ()
    {

        //This pauses the game and makes sure the mouse is visible
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (canvas.gameObject.activeInHierarchy == false)
            {

                canvas.gameObject.SetActive(true);

                Time.timeScale = 0;

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                moveCam = false;
            }                   
        }

        /*if (Input.GetKeyDown(KeyCode.V) && canMove)
        {
            if (canvas2.gameObject.activeInHierarchy == false)
            {

                canvas2.gameObject.SetActive(true);

                Time.timeScale = 0;

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                canMove = false;
                moveCam = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.V) && !canMove)
        {
            if (canvas2.gameObject.activeInHierarchy == true)
            {

                canvas2.gameObject.SetActive(false);

                Time.timeScale = 1;

                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

                canMove = true;
                moveCam = true;
            }
        }*/

        if (CarrotsTaken.carrotsTaken >= 1000)
        {
            SceneManager.LoadScene("LoseScreen");
        }

    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(5f);
        playerDied = false;
        PlayerBunny.playerHealth = 10;
        Instantiate(thePlayer, theRespawn.position, theRespawn.rotation);
        StopCoroutine("RespawnPlayer");
    }

}
