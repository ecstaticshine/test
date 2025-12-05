using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movebackwards : MonoBehaviour
{
    // static으로 선언해서 어디서든 속도를 조절할 수 있게 함 (나중에 빨라지게 하려고)
    public static float globalSpeed = 10f;

    void Update()
    {
        // 매 프레임 뒤쪽(Back)으로 이동
        transform.Translate(Vector3.back * globalSpeed * Time.deltaTime, Space.World);

        // 카메라 뒤로 너무 멀리 지나가면 스스로 파괴 (청소)
        if (transform.position.z < -20f)
        {
            //Destroy(gameObject);
        }
    }
}
