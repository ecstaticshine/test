using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("코인 프리팹 & 코인 갯수")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int coinMaxCount = 10;

    [SerializeField] private GameObject plane1;
    [SerializeField] private GameObject plane2;

    [SerializeField] private GameObject heartPrefab;

    List<GameObject> itemList = new List<GameObject>();


    public void Awake()
    {
        Setup();
    }

    public Vector3 setRandomPosition()
    {
        System.Random rnd = new System.Random();
        int random = rnd.Next() % 3;
        int[] PosX = { -2, 0, 2 };
        
        Vector3 position = new Vector3(PosX[random], 1, UnityEngine.Random.Range(5,30));

        return position;
    }

    public void Setup()
    {
        for (int i = 0; i < coinMaxCount; i++)
        {
            Vector3 spawnPos = setRandomPosition();
            GameObject coin = Instantiate(coinPrefab, spawnPos, Quaternion.identity);
            coin.transform.SetParent(GetBackPlane());
            itemList.Add(coin);
        }
        Vector3 heartPos = setRandomPosition();
        GameObject heart = Instantiate(heartPrefab, heartPos, Quaternion.identity);
        heart.transform.SetParent(GetBackPlane());
        itemList.Add(heart);

    }

    public void RequestRespawn(GameObject item)
    {
        StartCoroutine(RespawnCoroutine(item));
    }

    IEnumerator RespawnCoroutine(GameObject item)
    {
        yield return new WaitForSeconds(10f);

        // plane 랜덤 재배정
        item.transform.SetParent(GetBackPlane(), true);

        // 위치 재배정
        item.transform.position = setRandomPosition();

        // 다시 활성화
        item.SetActive(true);
    }

    public Transform GetBackPlane()
    {
        // Z가 더 큰 plane이 화면 뒤쪽에 있다고 가정
        return plane1.transform.position.z > plane2.transform.position.z
            ? plane1.transform
            : plane2.transform;
    }
}