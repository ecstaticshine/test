using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    private void Update()
    {
        gameObject.transform.Rotate(new Vector3(10f, 20f, 30f) * Time.deltaTime * 10);
    }
}
