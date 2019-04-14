using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllyBunny : MonoBehaviour
{

    private float abRange = 120f;
    public Transform target, allyRot, abFistSpawn;
    private int turnSpeed = 9;
    private string enemyTag = "Enemy";

    private int abHealth;
    public GameObject abFist;
    private bool canPunch;

    private NavMeshAgent navMesh;

    void Start()
    {
        abHealth = 15;

        navMesh = this.GetComponent<NavMeshAgent>();

        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        InvokeRepeating("AttackEnemy", 0f, 1f);
    }

    void UpdateTarget()
    {
        //This finds the carrot 
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float sDist = Mathf.Infinity;
        GameObject nEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            //This finds the the carrot in relation to the enemy 
            float disToCarrot = Vector3.Distance(transform.position, enemy.transform.position);

            if (disToCarrot < sDist)
            {
                sDist = disToCarrot;
                nEnemy = enemy;
            }
        }

        //If the new carrot is detected and the shortest distance is within the detection range
        //the the enemy will target the new carrot
        if (nEnemy != null && sDist <= abRange)
        {
            target = nEnemy.transform;

        }
        else
        {
            target = null;
            canPunch = false;

        }

    }

    void AttackEnemy()
    {
        if(canPunch == true)
        {
            GameObject abFistInst;
            abFistInst = Instantiate(abFist, abFistSpawn.position, abFistSpawn.rotation);
            abFistInst.GetComponent<Rigidbody>().AddForce(transform.forward * 300);
        }

    }

    void Update()
    {


        if (abHealth <= 0)
        {
            ABStorage.abCount -= 1;
            target = null;
            Destroy(gameObject);
        }

        //This makes sure nothing will happen unless a target is found
        if (target == null)
        {
            return;           
        }

        //Sets the direction the turret is looking at to the target's position
        Vector3 theDirection = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(theDirection);

        //Makes turret rotate smoothly with euler angles
        Vector3 rotation = Quaternion.Lerp(allyRot.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;

        //This allows the turret to rotate toward an enemy. If you don't want the whole body
        //to rotate around (ex. 2D games, etc.) then just set it to rotate on the y axis
        allyRot.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        //This allows the ally to move using the navmesh
        if (target != null)
        {
            Vector3 targetVector = target.transform.position;
            navMesh.SetDestination(targetVector);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            target = null;

            canPunch = true;
            //StartCoroutine("PunchEnemy");

        }

        if (other.gameObject.tag == "EnemyFist")
        {
            abHealth -= 1;
        }
    }

    /*IEnumerator PunchEnemy()
    {

            yield return new WaitForSeconds(1f);
            GameObject abFistInst;
            abFistInst = Instantiate(abFist, abFistSpawn.position, abFistSpawn.rotation);
            abFistInst.GetComponent<Rigidbody>().AddForce(transform.forward * 300);
            StartCoroutine("PunchEnemy");

    }*/
}
