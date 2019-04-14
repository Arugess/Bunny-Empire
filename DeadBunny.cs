using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This makes the body of an enemy fall over then destroy itself after 2 seconds.//

public class DeadBunny : MonoBehaviour
{
    Rigidbody theBody;

	void Start ()
    {
        theBody = GetComponent<Rigidbody>();

        theBody.centerOfMass = new Vector3(0f, 0f, 1f);
        Destroy(gameObject, 2f);
	}

	void Update ()
    {
		
	}
}
