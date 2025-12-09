using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Benjathemaker;

public class ItemSpawner : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private Transform[] lanePosition;

    [Header("생성 설정")]
    [SerializeField] private float spawnY = 1.0f;
    [SerializeField] private float itemInterval = 1.5f;

    [Header("곡선(점프) 설정")]
    [SerializeField] private float jumpArcHeight = 3.5f;
    [SerializeField] private float jumpDistance = 12f;

    [Header("힐 아이템 설정 (시간제)")]
    [SerializeField] private float healSpawnInterval = 15f;
    [SerializeField] private float healSpawnZOffset = 65f;
    private float healTimer = 0f;

    private const int COIN_COUNT = 3;

    private void Update()
    {
        if (!B_GameManager.instance.isLive || B_GameManager.instance.isClear) return;

        healTimer += Time.deltaTime;

        if (healTimer >= healSpawnInterval)
        {
            SpawnHealItem();
            healTimer = 0f;
        }
    }

    private void SpawnHealItem()
    {
        GameObject heart = ItemPool.Instance.GetHeart();

        int randomLane = Random.Range(0, lanePosition.Length);

        float spawnZ = lanePosition[0].position.z + healSpawnZOffset;

        heart.transform.position = new Vector3(
            lanePosition[randomLane].position.x,
            spawnY,
            spawnZ
        );

        AttachToFloor(heart, spawnZ);
    }

    public void SpawnPattern(int safeLaneIndex, float startZ, bool isCurve = false)
    {
        if (isCurve) SpawnJumpArc(safeLaneIndex, startZ);
        else SpawnStraightLine(safeLaneIndex, startZ);
    }

    private void SpawnStraightLine(int laneIndex, float startZ)
    {
        for (int i = 0; i < COIN_COUNT; i++)
        {
            GameObject coin = ItemPool.Instance.GetCoin();

            float posX = lanePosition[laneIndex].position.x;
            float posZ = startZ + (i * itemInterval);

            coin.transform.position = new Vector3(posX, spawnY, posZ);
            AttachToFloor(coin, posZ);
        }
    }

    private void SpawnJumpArc(int laneIndex, float startZ)
    {
        float posX = lanePosition[laneIndex].position.x;

        for (int i = 0; i < COIN_COUNT; i++)
        {
            GameObject coin = ItemPool.Instance.GetCoin();

            float t = (float)i / (COIN_COUNT - 1);
            float currentZ = startZ + (t * jumpDistance);
            float arcY = spawnY + (4 * jumpArcHeight * t * (1 - t));

            coin.transform.position = new Vector3(posX, arcY, currentZ);
            AttachToFloor(coin, currentZ);
        }
    }

    private void AttachToFloor(GameObject item, float worldZ)
    {
        if (Scroll.Instance != null)
        {
            Transform targetFloor = Scroll.Instance.GetFloorAtPosition(worldZ);
            if (targetFloor != null)
            {
                item.transform.SetParent(targetFloor, true);

                var anim = item.GetComponent<Benjathemaker.SimpleGemsAnim>();
                if (anim != null)
                {
                    anim.ResetPosition();
                }
            }
        }
    }
}