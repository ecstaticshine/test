using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private Transform[] lanePositions;
    [SerializeField] private Transform[] laneMiddlePosition;
    [SerializeField] private GameObject[] obstaclePrefabs;

    [Header("생성 주기")]
    [SerializeField] private float startSpawnTime = 3f;
    [SerializeField] private float endSpawnTime = 0.5f;

    private float timebetObstacleRespawn;
    private float lastObstacleRespawnTime;
    private int rnd;
    private Vector3 obstacleOffset_Y = new Vector3(0f, -0.5f, 0f);
    private Vector3 obstacleOffset_Z = new Vector3(0f, 0f, 45f);

    private void Start()
    {
        timebetObstacleRespawn = startSpawnTime;
        rnd = UnityEngine.Random.Range(0,3);
    }

    private void Update()
    {
        if (B_GameManager.instance.isBoss) return;

        float timeRatio = B_GameManager.instance.gameTime / B_GameManager.instance.maxGameTime;

        timeRatio = Mathf.Clamp01(timeRatio);

        timebetObstacleRespawn = Mathf.Lerp(startSpawnTime, endSpawnTime, timeRatio);

        if (B_GameManager.instance.gameTime >= lastObstacleRespawnTime + timebetObstacleRespawn)
        {
            lastObstacleRespawnTime = B_GameManager.instance.gameTime;

            SpawnObstacle();
        }
    }

    private void SpawnObstacle()
    {
        switch (rnd)
        {
            case 0:
                B_GameManager.instance.Get
                    (
                    obstaclePrefabs[0],
                    lanePositions[UnityEngine.Random.Range(0, lanePositions.Length)].position + obstacleOffset_Z,
                    Quaternion.identity
                    );

                rnd = UnityEngine.Random.Range(0, 3);
                break;
            case 1:
                B_GameManager.instance.Get
                    (
                    obstaclePrefabs[UnityEngine.Random.Range(1, 2)],
                    laneMiddlePosition[UnityEngine.Random.Range(0, laneMiddlePosition.Length)].position + obstacleOffset_Z,
                    Quaternion.identity
                    );

                rnd = UnityEngine.Random.Range(0, 3);
                break;
            case 2:
                B_GameManager.instance.Get
                    (
                    obstaclePrefabs[3],
                    lanePositions[1].position + obstacleOffset_Y + obstacleOffset_Z,
                    Quaternion.identity
                    );

                rnd = UnityEngine.Random.Range(0, 2);
                break;
        }
    }
}
