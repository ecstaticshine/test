using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance; // 어디서든 부를 수 있게 싱글톤 처리

    [System.Serializable]
    public struct Pool
    {
        public string tag;      // 장애물 이름 (예: "Null", "Error")
        public GameObject prefab;
        public int size;        // 미리 만들어둘 개수 (예: 20개)
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Awake()
    {
        Instance = this;
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        // 미리 오브젝트들을 생성해서 꺼(SetActive false) 둠
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position)
    {
        if (!poolDictionary.ContainsKey(tag)) return null;

        // 큐에서 하나 꺼냄
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = Quaternion.identity;

        // 다 쓰면 다시 큐에 넣기 위해 MoveBackwards에서 비활성화될 때 처리 필요하지만,
        // 일단 편의상 큐에 다시 넣는 로직은 간단히 '재사용' 개념으로 큐 맨 뒤로 보냄
        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
