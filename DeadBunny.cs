using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
