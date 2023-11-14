using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] Transform[] spawnPoints;
    GameObject[] currentObstacles;
    [SerializeField] int obstaclesToBeSpawned;

    private void Start()
    {
        currentObstacles = new GameObject[obstaclesToBeSpawned];
    }

    void Update()
    {
        for (int i = 0; i < currentObstacles.Length; i++)
        {
            if (currentObstacles[i] == null)
            {
                SpawnObstacle(i);
            }
            else if(currentObstacles[i] != null)
            {
                currentObstacles[i].GetComponent<Obstacle>().Move();
            }
        }
        
    }

    void SpawnObstacle(int i)
    {
        int spawnIndex = Random.Range(1, spawnPoints.Length);
        Transform spawnPoint = transform.GetChild(spawnIndex).transform;

        currentObstacles[i] = Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity);
    }
}
