using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    public static ItemPool Instance;

    [Header("코인 프리팹")]
    public GameObject coinPrefab;

    [Header("초기 생성 개수")]
    public int initialCount = 100;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        Instance = this;
        CreatePool();
    }

    void CreatePool()
    {
        for (int i = 0; i < initialCount; i++)
        {
            GameObject obj = Instantiate(coinPrefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetCoin()
    {
        if (pool.Count == 0)
        {
            // 풀에 남은게 없으면 하나 더 만든다
            GameObject extra = Instantiate(coinPrefab);
            extra.SetActive(false);
            return extra;
        }

        GameObject coin = pool.Dequeue();
        coin.SetActive(true);
        return coin;
    }

    public void ReturnCoin(GameObject coin)
    {
        coin.SetActive(false);
        pool.Enqueue(coin);
    }
}
