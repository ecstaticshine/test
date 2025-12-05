using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankingPrefabUI : MonoBehaviour
{
    [SerializeField] private TMP_Text RankText;
    [SerializeField] private TMP_Text NameText;
    [SerializeField] private TMP_Text ScoreText;

    public void SetData(int RankNumber, string playerName, int Score)
    {
        RankText.text = string.Format("{0}." , RankNumber);
        NameText.text = string.Format("{0}", playerName);
        ScoreText.text = string.Format("{0}", Score);
    }
}
