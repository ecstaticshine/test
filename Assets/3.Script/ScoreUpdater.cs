using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUpdater : MonoBehaviour
{
    public TMP_InputField nameInput;


    public void OnConfirmButton()
    {
        string playerName; 

        if (nameInput.text.Length > 3)
        {
            playerName = nameInput.text.Substring(0, 3);
        }
        else
        {
            playerName = nameInput.text;
        }

        // 예: 랭킹 저장
        ScoreManager.instance.UpdateScore(playerName, PlayerPrefs.GetInt("Score"));

        SceneLoader.Instance.LoadResultScene();
    }

    private void Start()
    {
        AudioManager.Instance.PlaySFX_PauseBGM("Clap");   
    }

}
