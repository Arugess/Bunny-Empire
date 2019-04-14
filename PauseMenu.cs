using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//This allows the player to click on buttons while they are pausing the game and locks the camera.//

public class PauseMenu : MonoBehaviour
{
    public Button resume, startMenu;
    public Transform theCanvas;

    // Use this for initialization
    void Start ()
    {
        Button btn1 = resume.GetComponent<Button>();
        btn1.onClick.AddListener(Resume);

        Button btn2 = startMenu.GetComponent<Button>();
        btn2.onClick.AddListener(StartMenu);

	}
	
    //This unpauses the game, sets the mouse invisible and locks it to the screen
    void Resume()
    {
        theCanvas.gameObject.SetActive(false);

        Time.timeScale = 1;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        MainLevelStuff.moveCam = true;
    }

    void StartMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

	// Update is called once per frame
	void Update ()
    {
		
	}
}
