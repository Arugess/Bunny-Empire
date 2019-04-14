using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This makes a 5 second delay before an ally can respawn.//

public class AllyRespawn : MonoBehaviour
{
    //set a bool to activate when the case for ally spawning 
    //equates to the spawn point
    public bool isActive;
    public bool thisAllyDead;

    public Transform thisSpawn;
    public GameObject thisAlly;
    
	void Start ()
    {
        isActive = false;
	}
	
	void Update ()
    {
		if (isActive && thisAllyDead)
        {
            StartCoroutine("RespawnAlly");
        }
	}

    IEnumerator RespawnAlly()
    {
        yield return new WaitForSeconds(5);
        Instantiate(thisAlly, thisSpawn.position, thisSpawn.rotation);
    }
}
