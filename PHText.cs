using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PHText : MonoBehaviour
{
    public static int pHealth;
    Text health;

    void Start()
    {
        health = GetComponent<Text>();

    }

    void Update()
    {
        pHealth = PlayerBunny.playerHealth;

        health.text = "HP: " + pHealth;
    }
}
