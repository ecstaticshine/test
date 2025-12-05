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
    [SerializeField] private GameObject[] obstaclePrefabs;
    private Transform laneMiddlePosition;
    private float obstacleRespawnTime;

    private void SetUpLanePosition()
    {
        //lanePositions = new 
    }

}
