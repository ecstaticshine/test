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

    private ItemSpawner spawner;  // ← 원래 스포너에 접근하기 위해

    void Start()
    {
        // 자기 자신을 생성한 스포너 찾기
        spawner = FindAnyObjectByType<ItemSpawner>();
    }

    void Update()
    {
        // 아이템은 제자리에서 뱅글뱅글 돌아야 잘 보임
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        if (transform.position.z < -100f)
        {
            HideAndRespawn();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        // 플레이어와 닿았을 때
        if (collider.CompareTag("Player"))
        {
            if (type.Equals(ItemType.Coin))
            {
                GameManager.Instance.GetCoin();
                HideAndRespawn();
            }
            else if (type.Equals(ItemType.Heal))
            {
                GameManager.Instance.GetHealItem();
                HideAndRespawn();
            }
        }
    }

    void HideAndRespawn()
    {
        spawner.RequestRespawn(this.gameObject);
        gameObject.SetActive(false);
    }
}
