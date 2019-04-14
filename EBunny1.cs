using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//This controls the behavior of the enenmy bunnies.//
//They will target carrots that have sprouted and move to destroy them.//
//It also controls the damage they take from arrows.//

public class EBunny1 : MonoBehaviour
{

    private float dRange = 60f;
    public Transform target, enemyRot;
    private int turnSpeed = 8;
    private string carrotTag = "Carrot";
    public GameObject deadBunny;

    private int ebHealth;

    private NavMeshAgent navMesh;

	void Start ()
    {
        ebHealth = 10;

        navMesh = this.GetComponent<NavMeshAgent>();

        InvokeRepeating("UpdateCarrot", 0f, 0.5f);
	}


    void UpdateCarrot()
    {
        //This finds the carrot 
        GameObject[] carrots = GameObject.FindGameObjectsWithTag(carrotTag);
        float sDist = Mathf.Infinity;
        GameObject nCarrot = null;

        foreach(GameObject carrot in carrots)
        {
            //This finds the the carrot in relation to the enemy 
            float disToCarrot = Vector3.Distance(transform.position, carrot.transform.position);

            if(disToCarrot < sDist)
            {
                sDist = disToCarrot;
                nCarrot = carrot;
            }
        }

        //If the new carrot is detected and the shortest distance is within the detection range
        //the the enemy will target the new carrot
        if(nCarrot != null && sDist <= dRange)
        {
            target = nCarrot.transform;

        }
        else
        {
            target = null;
        }
    }	

	void Update ()
    {

        if (ebHealth <= 0)
        {
            Instantiate(deadBunny, enemyRot.position, enemyRot.rotation);
            target = null;
            Destroy(gameObject);
        }

        //This makes sure nothing will happen unless a target is found
        if (target == null)
        {
            return;
        }

        //Sets the direction the enemy is looking at to the target's position
        Vector3 theDirection = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(theDirection);

        //Makes turret rotate smoothly with euler angles
        Vector3 rotation = Quaternion.Lerp(enemyRot.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;

        //This allows the enemy to rotate toward a carrot. If you don't want the whole body
        //to rotate around (ex. 2D games, etc.) then just set it to rotate on the y axis
        enemyRot.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        //This allows the enemy to move using the navmesh
        if(target != null)
        {
            Vector3 targetVector = target.transform.position;
            navMesh.SetDestination(targetVector);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Carrot")
        {
            target = null;
        }

        if(other.gameObject.tag == "BunnyFist" || other.gameObject.tag == "Arrow")
        {

            ebHealth -= 1;
        }

    }
}
