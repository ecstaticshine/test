using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    [SerializeField] private Transform[] floors;
    [SerializeField] private float scrollSpeed = 10f;

    private float floorSize;

    private void Awake()
    {
        SetUpFloor();

        floorSize = floors[0].GetComponent<BoxCollider>().size.z * floors[0].transform.localScale.z;
    }

    private void Update()
    {
        MoveFloor();
    }

    private void SetUpFloor()
    {
        floors = new Transform[transform.childCount];

        for(int i = 0; i < floors.Length; i++)
        {
            floors[i] = transform.GetChild(i).transform;
        }
    }

    private void MoveFloor()
    {
        for(int i = 0; i < floors.Length; i++)
        {
            floors[i].Translate(Vector3.back * scrollSpeed * Time.deltaTime);

            if(floors[i].transform.position.z <= -floorSize)
            {
                floors[i].Translate(Vector3.forward * floorSize * floors.Length);
            }
        }
    }
}
