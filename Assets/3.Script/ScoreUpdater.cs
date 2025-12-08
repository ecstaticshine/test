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
        string playerName = nameInput.text;
        Debug.Log("입력한 이름: " + playerName);

        // 예: 랭킹 저장
        ScoreManager.instance.UpdateScore(playerName, PlayerPrefs.GetInt("Score"));

        SceneLoader.Instance.LoadResultScene();
    }
}
