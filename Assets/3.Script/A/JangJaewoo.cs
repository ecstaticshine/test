using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JangJaewoo : MonoBehaviour
{
    private void Update()
    {
        gameObject.transform.Rotate(new Vector3(16f, 21f, 33f) * Time.deltaTime * 10);
    }
}
