using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingObstacle : MonoBehaviour
{
    
    public GameObject cactusPrefab;
    public GameObject spawnPoint;

    private bool hasRolled = false;
    Rigidbody rb;

    public float radius;
    public float angle;
    public float ImpulseStrength;
    public float spawnDelay;
    public float breakThreshold;

    public GameObject playerRef;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        //DElay
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
            if (hasRolled == false && rb.velocity == Vector3.zero &&distanceToTarget < radius && Physics.Raycast(transform.position, directionToTarget, distanceToTarget))
            {
                rb.AddForce(transform.forward * ImpulseStrength);
                hasRolled = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.health - 1 or something
            if (rb.velocity.magnitude > breakThreshold)
            {
                Debug.Log("SHOULD BREAK");
                //2 Second delay or something
                Instantiate(cactusPrefab, spawnPoint.transform.position, cactusPrefab.transform.rotation);
                Destroy(gameObject);
            }
            

            //collision.destroy or wahtever
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (rb.velocity.magnitude > breakThreshold)
            {
                //Same Delay
                Instantiate(cactusPrefab, spawnPoint.transform.position, cactusPrefab.transform.rotation);
                Destroy(gameObject);
            }
            //collision.destroy or wahtever
        }
    }

}

