using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This controls the players character and allows them to teleport to the underground store.//

public class PlayerBunny : MonoBehaviour
{
    //These stats are for jump power, gravity, speed, camera/player rotation, 
    //and sets the players position to zero
    private float hopPower = 10;
    private float theGravity = 20.0f;
    private float bunnySpeed = 6;
    private float turnSpeed = 2;
    private Vector3 moveBunny = Vector3.zero;

    public static int playerHealth;

    //These are used for teleporting the player 
    public GameObject playerB;
    public Transform telePoint1, telePoint2;

    //Player fist
    public GameObject bFist;
    public Transform fistSpawn;

    AudioSource punch;

    public Transform espawn;
    public GameObject ebunny;

    CharacterController controller;

    void Start ()
    {
        playerHealth = 10;

        punch = GetComponent<AudioSource>();
 
	}

    void Update()
    {
        if (MainLevelStuff.moveCam == true)
        {
            //Gets the camera/player to rotate left or right
            transform.Rotate(0, Input.GetAxis("Mouse X") * turnSpeed, 0);

            //Gets the character controller attached to the player
            controller = GetComponent<CharacterController>();

            //If the player is grounded then they can move or jump
            if (controller.isGrounded)
            {
                moveBunny = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveBunny = transform.TransformDirection(moveBunny);
                moveBunny *= bunnySpeed;

                if (Input.GetButton("Jump"))
                {
                    moveBunny.y = hopPower * 1.2f;
                }
            }
            //These allow the player to move with Time.deltaTime, and jump being effected by gravity
            controller.Move(moveBunny * Time.deltaTime);
            moveBunny.y -= theGravity * Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            punch.Play();
            GameObject fistInstance;
            fistInstance = Instantiate(bFist, fistSpawn.position, fistSpawn.rotation);
            fistInstance.GetComponent<Rigidbody>().AddForce(transform.forward * 300);
        }

        if(playerHealth <= 0)
        {
            MainLevelStuff.playerDied = true;
            CarrotScore.carrotScore -= 50;
            Destroy(gameObject);
        }

       /*if(Input.GetKeyDown(KeyCode.F))
        {
            CarrotScore.carrotScore += 100;
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(ebunny, espawn.position, espawn.rotation);
        }*/
        
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Enemy")
        {
            float pushPower = 0.1f;
            Vector3 pushDir = transform.position - hit.transform.position;
            controller.Move(pushDir * pushPower);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //These allow the player to teleport when they touch the trigger
        if (other.tag == "PortPad")
        {
            playerB.transform.position = new Vector3(11, -92, 4);
            //playerB.transform.position = telePoint1.transform.position;
        }
        else if (other.tag == "PortPad2")
        {
            playerB.transform.position = new Vector3(13, 1, -7);

            //playerB.transform.position = telePoint2.transform.position;
        }
    }

}

