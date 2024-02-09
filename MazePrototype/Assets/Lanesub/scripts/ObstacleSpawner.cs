using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] Transform[] spawnPoints;
    GameObject[] currentObstacles;
    [SerializeField] int obstaclesToBeSpawned;
    [SerializeField] int delay;
    bool hasBeenCalled;

    private void Start()
    {
        currentObstacles = new GameObject[obstaclesToBeSpawned];
    }

    void Update()
    {
         if (!hasBeenCalled)
         {
             StartCoroutine(SpawnObstacle());
         }            
    }

    /*void SpawnObstacle(int i)
    {
        int spawnIndex = Random.Range(1, spawnPoints.Length);
        Transform spawnPoint = transform.GetChild(spawnIndex).transform;

        currentObstacles[i] = Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity);
    }*/

    IEnumerator SpawnObstacle()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = transform.GetChild(spawnIndex).transform;

        Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity);
        hasBeenCalled = true;
        yield return new WaitForSeconds(delay);
        hasBeenCalled = false;
    }
}
