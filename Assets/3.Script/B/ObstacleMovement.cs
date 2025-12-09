using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{

    private void Update()
    {
        if (!B_GameManager.instance.isLive) return;

        float currentSpeed = B_GameManager.instance.currentGameSpeed;

        transform.Translate(Vector3.back * currentSpeed * Time.deltaTime);

        if (transform.position.z < -15f)
        {
            B_GameManager.instance.Return(gameObject);
        }
    }
}