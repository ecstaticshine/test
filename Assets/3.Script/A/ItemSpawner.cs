using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    [SerializeField] private GameObject plane1;
    [SerializeField] private GameObject plane2;


    void Start()
    {
        // 처음부터 여러 줄을 깔고 싶으면 여기에 spawn 반복
        for (int i = 0; i < 50; i++)
        {
            SpawnCoinLine(15);
        }
    }

    public void SpawnCoinLine(int count)
    {
        int[] lanes = { -2, 0, 2 };

        for (int i = 0; i < count; i++)
        {
            GameObject coin = ItemPool.Instance.GetCoin();

            // 뒤쪽 plane 기준으로 생성
            Transform backPlane = GetBackPlane();

            float zBack = Mathf.Max(plane1.transform.position.z, plane2.transform.position.z)/4;
            float spawnZ = zBack + i * 2f;

            int laneIndex = UnityEngine.Random.Range(0, 3);

            coin.transform.SetParent(backPlane, true);
            coin.transform.position = new Vector3(lanes[laneIndex], 1, spawnZ);
        }
    }

    public Transform GetBackPlane()
    {
        return plane1.transform.position.z > plane2.transform.position.z ?
            plane1.transform : plane2.transform;
    }

    public void RespawnCoin(GameObject coin)
    {
        ItemPool.Instance.ReturnCoin(coin);
        SpawnCoinLine(1); // 하나 사라지면 하나 새로 스폰
    }
}