using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicScript : MonoBehaviour
{
    private bool canMove;

    // Use this for initialization
    void Start ()
    {
        canMove = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //This pauses the game and makes sure the mouse is visible
        if (Input.GetKeyDown(KeyCode.V))
        {
            if(canMove == true)
            {
                Time.timeScale = 0;

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                canMove = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.V) && !canMove)
        {
            if(canMove == false)
            {
                Time.timeScale = 1;

                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

                canMove = false;
            }

        }
    }
}
