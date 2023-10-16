using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTape : MonoBehaviour
{
    public GameObject prefab;
    public Transform playerObj;
    public Transform spawnPoint;

    bool playerInRange;
    public float detectionRange;

    void Update()
    {
        DetectPlayer();

        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            Instantiate(prefab, transform.position, transform.rotation);
        }
    }

    void DetectPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerObj.position);
        if (distanceToPlayer <= detectionRange)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
    }
}
