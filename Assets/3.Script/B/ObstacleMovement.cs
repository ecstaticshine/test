using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{

    //신재호님 도움
    public float speed;

    private void Update()
    {
        gameObject.transform.Translate(Vector3.back * speed * Time.deltaTime);

        Destroy(gameObject, 5f);
    }
}
