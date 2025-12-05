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

    void Update()
    {
        // 아이템은 제자리에서 뱅글뱅글 돌아야 잘 보임
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // 플레이어와 닿았을 때
        if (other.CompareTag("Player"))
        {
            if (type == ItemType.Coin)
            {
                GameManager.Instance.GetCoin();
            }
            else if (type == ItemType.Heal)
            {
                GameManager.Instance.GetHealItem();
            }
            
            // 먹었으니 사라짐 (오브젝트 풀링 사용 시 SetActive(false))
            // 풀링이 아직 적용 안 된 상태라면 Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
