using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType { Coin, Heal }

    [Header("아이템 설정")]
    public ItemType type;      // 인스펙터에서 코인인지 힐인지 선택
    public float rotateSpeed = 100f; // 뱅글뱅글 회전 속도

    private ItemSpawner spawner;

    public float maxLifetime = 30f;
    private float lifetime = 0f;

    void Start()
    {
        spawner = FindAnyObjectByType<ItemSpawner>();
    }


    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);

        lifetime += Time.deltaTime;

        if (lifetime >= maxLifetime)
        {
            Despawn();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 플레이어와 닿았을 때
        if (other.CompareTag("Player"))
        {
            if (type == ItemType.Coin)
            {
                GameManager.Instance.GetCoin();
                Despawn();
            }
            else if (type == ItemType.Heal)
            {
                GameManager.Instance.GetHealItem();
                Despawn();
            }
            
        }
    }

    void Despawn()
    {
        spawner.RespawnCoin(gameObject);
    }
}
