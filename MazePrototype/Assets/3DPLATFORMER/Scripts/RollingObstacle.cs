using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingObstacle : MonoBehaviour
{
    
    public GameObject cactusPrefab;
    public GameObject spawnPoint;

    public float radius;
    public float angle;
    public float ImpulseStrength;

    public GameObject playerRef;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Transform target = playerRef.transform;

        //Finds the direction to the target
        Vector3 directionToTarget = (target.position - transform.position).normalized;

        //Checks if the angle of the targets location is within the agents range
        if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
        {
            //finds the distance to the target
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            //Checks if target is within range and if there are any obstructions between the agent and the target
            if (GetComponent<Rigidbody>().velocity == Vector3.zero &&distanceToTarget < radius && Physics.Raycast(transform.position, directionToTarget, distanceToTarget))
            {
                gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * ImpulseStrength);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.health - 1 or something
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            //collision.destroy or wahtever

            Instantiate(cactusPrefab, spawnPoint.transform.position, cactusPrefab.transform.rotation);
        }
    }

}

