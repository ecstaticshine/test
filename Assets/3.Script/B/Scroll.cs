using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public static Scroll Instance;

    [SerializeField] private Transform[] floors;
    private float floorSize;

    private void Awake()
    {
        Instance = this;
        SetUpFloor();
        if (floors.Length > 0)
            floorSize = floors[0].GetComponent<BoxCollider>().size.z * floors[0].transform.localScale.z;
    }

    private void Update()
    {
        if (!B_GameManager.instance.isLive || B_GameManager.instance.isClear) return;
        MoveFloor();
    }

    private void MoveFloor()
    {
        float currentSpeed = B_GameManager.instance.currentGameSpeed;

        for (int i = 0; i < floors.Length; i++)
        {
            floors[i].Translate(Vector3.back * currentSpeed * Time.deltaTime);

            if (floors[i].transform.position.z <= -floorSize)
            {
                CleanUpFloor(floors[i]);

                floors[i].Translate(Vector3.forward * floorSize * floors.Length);
            }
        }
    }

    private void CleanUpFloor(Transform floor)
    {
        for (int i = floor.childCount - 1; i >= 0; i--)
        {
            Transform child = floor.GetChild(i);

            if (child.gameObject.activeSelf)
            {
                Item item = child.GetComponent<Item>();
                if (item != null)
                {
                    item.Despawn();
                    continue;
                }

                if (child.GetComponent<ObstacleMovement>() != null || child.CompareTag("Obstacle"))
                {
                    child.SetParent(null);

                    var moveScript = child.GetComponent<ObstacleMovement>();
                    if (moveScript != null) moveScript.enabled = true;

                    B_GameManager.instance.Return(child.gameObject);
                }
            }
        }
    }

    private void SetUpFloor()
    {
        floors = new Transform[transform.childCount];
        for (int i = 0; i < floors.Length; i++)
        {
            floors[i] = transform.GetChild(i).transform;
        }
    }

    public Transform GetFloorAtPosition(float worldZ)
    {
        foreach (Transform floor in floors)
        {
            if (Mathf.Abs(floor.position.z - worldZ) < floorSize / 2)
            {
                return floor;
            }
        }
        return floors[0];
    }
}