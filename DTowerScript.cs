using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTowerScript : MonoBehaviour
{
    //The target of the turret
    public Transform target;


    //Stats that can be upgraded or changed
    [Header("Attributes")]

    //Sets the range of the turret, fire rate and the
    //countdown will allow it to begin immediatly
    public float range = 15f;
    public static float fireRate = 1f;
    private float fireCountdown = 0f;

    //Things required by unity in order for this script
    //to function and not to be changed by the player
    [Header("Locked Fields")]


    //Tag attached to the enemy bunnies
    private string enemyTag = "Enemy";

    //Sets which object will turn and its turning speed
    public Transform partToRotate;
    private int turnSpeed = 8;


    public GameObject arrowPrefab;
    public Transform arrowSpawn;


	void Start ()
    {
        //Makes UpdateTarget run twice every second
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
	

    void UpdateTarget()
    {
        //Finds enemy with the enemy tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        //Will store the shortest distance, makes it so shortest distance doesn't get within the range
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;


        foreach(GameObject enemy in enemies)
        {
            //Gets the distance to an enemy
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            //Finds enemy closer than any that was found previously
            if(distanceToEnemy < shortestDistance)
            {
                //Sets shortset distance to distanceToEnemy and that whatever enemy it is is now the closest
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        } 

        //When enemy is found set that enemy to a target otherwise no target
        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }

        else
        {
            target = null;
        }
    }

	void Update ()
    {
        //This makes sure nothing will happen unless a target is found
        if (target == null)
        {
            return;
        }

        //Sets the direction the turret is looking at to the target's position
        Vector3 theDirection = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(theDirection);

        //Makes turret rotate smoothly with euler angles
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;

        //This allows the turret to rotate toward an enemy. If you don't want the whole body
        //to rotate around (ex. 2D games, etc.) then just set it to rotate on the y axis
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        //This makes the tower fire when it reaches 0, sets the rate of fire (currently set 1 every 3 seconds)
        if(fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = fireRate;
        }

        //Reduces the fireCountdown by 1 every second
        fireCountdown -= Time.deltaTime;

    }

    void Shoot()
    {
        GameObject arrowGo = (GameObject) Instantiate(arrowPrefab, arrowSpawn.position, Quaternion.identity);

        //Calls on the Arrow script
        Arrow arrow = arrowGo.GetComponent<Arrow>();

        if(arrow != null)
        {
            arrow.Seek(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        //Creates a wire sphere which is blue to show range in editor
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
