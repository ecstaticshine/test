using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro

public class C_ResultScoreViewer : MonoBehaviour
{
    public TextMeshProUGUI currentScoreText; // 이번 판 점수
    public TextMeshProUGUI highScoreText;    // 최고 점수

    void Start()
    {
        // 1. 저장된 점수 불러오기
        int lastScore = PlayerPrefs.GetInt("LastScore", 0);
        int bestScore = PlayerPrefs.GetInt("HighScore", 0);

        // 2. 텍스트 표시
        if (currentScoreText != null)
            currentScoreText.text = "Score: " + lastScore.ToString("N0"); // N0은 1,000 단위 쉼표

        if (highScoreText != null)
            highScoreText.text = "Best: " + bestScore.ToString("N0");

        // (선택) 신기록이면 축하 효과음
        if (lastScore >= bestScore && lastScore > 0)
        {
            // AudioManager.Instance.PlaySFX("Win");
        }
    }
}
