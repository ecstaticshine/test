using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text HighScoreText;
    [SerializeField] private Transform leftContent;
    [SerializeField] private Transform rightContent;
    [SerializeField] private GameObject rankingPrefab;

    public void updateRanking()
    {
        foreach (Transform child in leftContent)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in rightContent)
        {
            Destroy(child.gameObject);
        }
        SaveData data = ScoreManager.instance.showData();
        HighScoreText.text = string.Format("HI-SCORE : {0}",data.HighScore);

        for (int i = 0; i< data.list.Count; i++)
        {
            GameObject obj = Instantiate(rankingPrefab);

            RankingPrefabUI objData = obj.GetComponent<RankingPrefabUI>();
            objData.SetData(i+1, data.list[i].playerName, data.list[i].playerScore);

            if (i< 5)
            {
                obj.transform.SetParent(leftContent,false);
            }
            else
            {
                obj.transform.SetParent(rightContent, false);
            }
        }

    }


    private void Update()
    {
        updateRanking();
    }

}
