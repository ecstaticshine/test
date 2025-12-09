using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private ItemSpawner itemSpawner;
    [SerializeField] private Transform[] lanePositions;
    [SerializeField] private Transform[] laneMiddlePosition;
    [SerializeField] private GameObject[] obstaclePrefabs;

    [Header("거리 기반 생성 설정")]
    [SerializeField] private float minDistance = 30f;
    [SerializeField] private float maxDistance = 50f;

    [Header("코인 위치 보정")]
    [SerializeField] private float straightCoinOffset = 5f;
    [SerializeField] private float jumpCoinOffset = 6f;

    private float currentDistMoved = 0f;
    private float targetDistToNextSpawn = 0f;

    private int rnd;
    private Vector3 obstacleOffset_Z = new Vector3(0f, 0f, 45f);
    private Vector3 obstacleOffset_Y = new Vector3(0f, 0.0f, 0f);

    private void Start()
    {
        SetNextSpawnDistance();
        rnd = UnityEngine.Random.Range(0, 3);
    }

    private void Update()
    {
        if (!B_GameManager.instance.isLive || B_GameManager.instance.isClear) return;

        float moveStep = B_GameManager.instance.currentGameSpeed * Time.deltaTime;
        currentDistMoved += moveStep;

        if (currentDistMoved >= targetDistToNextSpawn)
        {
            SpawnObstacle();
            currentDistMoved = 0f;
            SetNextSpawnDistance();
        }
    }

    private void SetNextSpawnDistance()
    {
        targetDistToNextSpawn = UnityEngine.Random.Range(minDistance, maxDistance);
    }

    private void SpawnObstacle()
    {
        float obstacleZ = lanePositions[0].position.z + obstacleOffset_Z.z;
        GameObject spawnedObstacle = null;

        switch (rnd)
        {
            case 0: // [1칸 장애물]
                int obsLane = UnityEngine.Random.Range(0, lanePositions.Length);
                spawnedObstacle = B_GameManager.instance.Get(
                    obstaclePrefabs[0],
                    lanePositions[obsLane].position + obstacleOffset_Z,
                    Quaternion.identity
                );

                AttachToFloor(spawnedObstacle, obstacleZ);

                if (spawnedObstacle != null && spawnedObstacle.activeSelf)
                {
                    int safeLane = GetRandomLaneExcluding(obsLane);
                    itemSpawner.SpawnPattern(safeLane, obstacleZ - straightCoinOffset, false);
                }

                rnd = UnityEngine.Random.Range(0, 3);
                break;

            case 1: // [2칸 장애물]
                int midIndex = UnityEngine.Random.Range(0, laneMiddlePosition.Length);
                spawnedObstacle = B_GameManager.instance.Get(
                    obstaclePrefabs[UnityEngine.Random.Range(1, 3)],
                    laneMiddlePosition[midIndex].position + obstacleOffset_Z,
                    Quaternion.identity
                );

                AttachToFloor(spawnedObstacle, obstacleZ);

                if (spawnedObstacle != null && spawnedObstacle.activeSelf)
                {
                    int targetLane = (midIndex == 0) ? 2 : 0;
                    itemSpawner.SpawnPattern(targetLane, obstacleZ - straightCoinOffset, false);
                }

                rnd = UnityEngine.Random.Range(0, 3);
                break;

            case 2: // [3칸 점프 장애물]
                spawnedObstacle = B_GameManager.instance.Get(
                    obstaclePrefabs[3],
                    lanePositions[1].position - obstacleOffset_Y + obstacleOffset_Z,
                    Quaternion.identity
                );

                AttachToFloor(spawnedObstacle, obstacleZ);

                if (spawnedObstacle != null && spawnedObstacle.activeSelf)
                {
                    int randomLane = UnityEngine.Random.Range(0, 3);
                    itemSpawner.SpawnPattern(randomLane, obstacleZ - jumpCoinOffset, true);
                }

                rnd = UnityEngine.Random.Range(0, 2);
                break;
        }
    }

    private void AttachToFloor(GameObject obstacle, float zPos)
    {
        if (obstacle != null && Scroll.Instance != null)
        {
            Transform floor = Scroll.Instance.GetFloorAtPosition(zPos);
            obstacle.transform.SetParent(floor, true);

            var moveScript = obstacle.GetComponent<ObstacleMovement>();
            if (moveScript != null) moveScript.enabled = false;
        }
    }

    private int GetRandomLaneExcluding(int excludeLane)
    {
        List<int> lanes = new List<int>() { 0, 1, 2 };
        lanes.Remove(excludeLane);
        return lanes[UnityEngine.Random.Range(0, lanes.Count)];
    }
}