using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This allows the camera to rotate up and down and from sidee to side.//

public class CamScript : MonoBehaviour
{

    public enum RotationAxes { MouseY = 1 }
    public RotationAxes axes = RotationAxes.MouseY;


    //These lines determine the sensitivty of of the camera.
    public float sensY = 2.0F;

    //These lines determine how far the player can look up and down. By setting it on the Main Camera then only the camera will tilt up and down.
    private float minY = -20F;
    private float maxY = 10F;

    //This line allows the camera to rotate on the "Y" axis.
    float rotationY = 0F;

    void Start()
    {

    }

    void Update()
    {
        if(MainLevelStuff.moveCam == true)
        {
            //These lines call on the "X" and "Y" axis attached to the mouse.
            if (axes == RotationAxes.MouseY)
            {

                //This line calls on the "Y" axis to rotate the camera by the speed of the sensitivity. 
                rotationY += Input.GetAxis("Mouse Y") * sensY;

                //This line sets the minimum and and maximum movement for the "Y" axis of the camera.
                rotationY = Mathf.Clamp(rotationY, minY, maxY);

                //This line allows the camera to rotate the player while it moves. 
                transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
            }
        }



    }
}
