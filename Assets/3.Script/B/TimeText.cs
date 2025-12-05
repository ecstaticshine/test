using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeText : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private float gameTime;
    [SerializeField] private float maxGameTime = 2 * 10f;
    private float remainTime;
    private int min;
    private int sec;

    private void Update()
    {
        gameTime += Time.deltaTime;
    }

    private void LateUpdate()
    {
        remainTime = maxGameTime - gameTime;
        min = Mathf.FloorToInt(remainTime / 60);
        sec = Mathf.FloorToInt(remainTime % 60);
        timeText.text = string.Format("{0:D2}:{1:D2}", min, sec);
    }
}
