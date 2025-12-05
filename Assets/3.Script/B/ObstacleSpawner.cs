using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    /*
     * 레인 포지션
     * 레인 포지션 (0과 1 중간 / 1과 2 중간)
     * 장애물 프리팹
     * 장애물끼리의 거리 (시간)
     */

    [SerializeField] private Transform[] lanePositions;
    [SerializeField] private Transform[] laneMiddlePosition;
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private float timebetObstacleRespawn = 2f;
    private float lastObstacleRespawnTime;
    private int rnd;
    private Vector3 obstacleOffset_Y = new Vector3(0f, -0.5f, 0f);
    private Vector3 obstacleOffset_Z = new Vector3(0f, 0f, 45f);

    private void Start()
    {
        rnd = UnityEngine.Random.Range(0,3);
    }

    private void Update()
    {
        if (Time.time >= lastObstacleRespawnTime + timebetObstacleRespawn)
        {
            lastObstacleRespawnTime = Time.time;

            switch (rnd)
            {
                case 0:
                    Instantiate
                        (
                        obstaclePrefabs[0],
                        lanePositions[UnityEngine.Random.Range(0, lanePositions.Length)].position + obstacleOffset_Z,
                        Quaternion.identity
                        );

                    rnd = UnityEngine.Random.Range(0, 3);
                    break;
                case 1:
                    Instantiate
                        (
                        obstaclePrefabs[UnityEngine.Random.Range(1, 2)],
                        laneMiddlePosition[UnityEngine.Random.Range(0, laneMiddlePosition.Length)].position + obstacleOffset_Z,
                        Quaternion.identity
                        );

                    rnd = UnityEngine.Random.Range(0, 3);
                    break;
                case 2:
                    Instantiate
                        (
                        obstaclePrefabs[3],
                        lanePositions[1].position + obstacleOffset_Y + obstacleOffset_Z,
                        Quaternion.identity
                        );

                    rnd = UnityEngine.Random.Range(0, 3);
                    break;
            }
        }
    }


}
