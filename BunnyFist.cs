using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
