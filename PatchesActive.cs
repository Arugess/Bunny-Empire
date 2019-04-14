using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PatchesActive : MonoBehaviour
{
    private int patchesActive;
    Text pCount;

	void Start ()
    {
		pCount = GetComponent<Text>();

	}
	
	void Update ()
    {
        patchesActive = CastleStorage.gPatches;

        pCount.text = patchesActive + "/80";
	}
}
