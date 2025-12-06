using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeText : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;
    private float remainTime;
    private int min;
    private int sec;

    private void Start()
    {
        timeText.enabled = true;
    }

    private void Update()
    {
        remainTime = B_GameManager.instance.maxGameTime - B_GameManager.instance.gameTime;
        min = Mathf.FloorToInt(remainTime / 60);
        sec = Mathf.FloorToInt(remainTime % 60);
        timeText.text = string.Format("{0:D2}:{1:D2}", min, sec);

        if (B_GameManager.instance.gameTime >= B_GameManager.instance.maxGameTime)
        {
            timeText.enabled = false;

            B_GameManager.instance.isBoss = true;
        }
    }
}
