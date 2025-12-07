using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    //신재호님 어시스트
    public float speed;

    private void Update()
    {
        gameObject.transform.Translate(Vector3.back * speed * Time.deltaTime);

        if (transform.position.z < -15f)
        {
            B_GameManager.instance.Return(gameObject);
        }
    }
}
