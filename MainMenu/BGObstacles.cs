using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGObstacles : MonoBehaviour
{
    public Vector2[] spawnPos;
    int randomPos;

    public GameObject[] bgObstacles;
    int randomObstacle;

    public float targetTime;
    float currentTime;

    int lastObstacleIndex = -1;
    int lastSpawnPointIndex = -1;

    private void Awake()
    {
        currentTime = 0;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= targetTime)
        {
            SpawnRandomObstacle();
            currentTime = 0;
        }
    }

    void SpawnRandomObstacle()
    {
        do
        {
            randomObstacle = Random.Range(0, bgObstacles.Length);
        }
        while (randomObstacle == lastObstacleIndex);
        lastObstacleIndex = randomObstacle;

        do
        {
            randomPos = Random.Range(0, spawnPos.Length);
        }
        while (randomPos == lastSpawnPointIndex);
        lastSpawnPointIndex = randomPos;

        Instantiate(bgObstacles[randomObstacle], spawnPos[randomPos], Quaternion.identity);
        
    }
}
