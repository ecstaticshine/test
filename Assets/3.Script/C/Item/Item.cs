using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType { Coin, Heal }

    [Header("아이템 설정")]
    public ItemType type;
    public float rotateSpeed = 100f;

    public float maxLifetime = 10f;
    private float lifetime = 0f;

    void OnEnable()
    {
        lifetime = 0f;
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
        if (other.CompareTag("Player"))
        {
            if (type == ItemType.Coin)
            {
                B_GameManager.instance.GetCoin();
                Despawn();
            }
            else if (type == ItemType.Heal)
            {
                B_GameManager.instance.GetHealItem();
                Despawn();
            }
        }
    }

    public void Despawn()
    {
        if (ItemPool.Instance != null)
        {
            transform.SetParent(ItemPool.Instance.transform);

            if (type == ItemType.Coin)
            {
                ItemPool.Instance.ReturnItem(gameObject, "Coin");
            }
            else if (type == ItemType.Heal)
            {
                ItemPool.Instance.ReturnItem(gameObject, "Heart");
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}