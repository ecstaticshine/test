using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    public static ItemPool Instance;

    [Header("프리팹 설정")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject heartPrefab;

    [Header("초기 설정")]
    [SerializeField] private int initialCoinCount = 50;
    [SerializeField] private int initialHeartCount = 5;

    private Queue<GameObject> coinPool = new Queue<GameObject>();
    private Queue<GameObject> heartPool = new Queue<GameObject>();

    void Awake()
    {
        Instance = this;
        InitializePool();
    }

    private void InitializePool()
    {
        // 코인 생성
        for (int i = 0; i < initialCoinCount; i++)
        {
            CreateNewItem(coinPrefab, coinPool);
        }

        // 하트 생성
        for (int i = 0; i < initialHeartCount; i++)
        {
            CreateNewItem(heartPrefab, heartPool);
        }
    }

    private GameObject CreateNewItem(GameObject prefab, Queue<GameObject> pool)
    {
        GameObject obj = Instantiate(prefab);
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        pool.Enqueue(obj);
        return obj;
    }

    public GameObject GetCoin()
    {
        return GetItem(coinPool, coinPrefab);
    }

    public GameObject GetHeart()
    {
        return GetItem(heartPool, heartPrefab);
    }

    private GameObject GetItem(Queue<GameObject> pool, GameObject prefab)
    {
        GameObject item;

        if (pool.Count > 0)
        {
            item = pool.Dequeue();
        }
        else
        {
            item = CreateNewItem(prefab, pool);
            pool.Dequeue();
        }

        item.SetActive(true);
        return item;
    }

    public void ReturnItem(GameObject item, string type)
    {
        item.SetActive(false);
        item.transform.SetParent(transform);

        if (type == "Coin") coinPool.Enqueue(item);
        else if (type == "Heart") heartPool.Enqueue(item);
    }
}