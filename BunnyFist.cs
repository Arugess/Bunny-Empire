using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This allows the player to spawn a fist to hit enemies and destroy itself on contact.//

public class BunnyFist : MonoBehaviour
{
    //Player fist
    public GameObject fistImpact;
    

    // Use this for initialization
    void Start ()
    {
        Destroy(gameObject, .05f);
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            GameObject fEffect = Instantiate(fistImpact, transform.position, transform.rotation);
            Destroy(fEffect, 1.0f);
            Destroy(gameObject);
        }
    }
}
