using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("ÄÚÀÎ ÇÁ¸®ÆÕ & ÄÚÀÎ °¹¼ö")]
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
            coin.transform.SetParent(plane1.transform);
            itemList.Add(coin);
        }
        Vector3 heartPos = setRandomPosition();
        GameObject heart = Instantiate(heartPrefab, heartPos, Quaternion.identity);
        heart.transform.SetParent(plane1.transform);
        itemList.Add(heart);

    }

    public void Update()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (!itemList[i].activeSelf)
            {
                itemList[i].SetActive(true);
            }

        }
    }
}