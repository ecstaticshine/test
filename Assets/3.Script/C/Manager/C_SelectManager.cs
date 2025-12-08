using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class C_SelectManager : MonoBehaviour
{
    // 버튼을 누를 때 이 함수를 호출 (0, 1, 2 숫자를 넣어서)
    public void SelectCharacter(int charIndex)
    {
        // 1. 선택한 캐릭터 번호를 저장 (컴퓨터에 영구 저장됨)
        PlayerPrefs.SetInt("SelectedCharacter", charIndex);
        PlayerPrefs.Save();

        Debug.Log(charIndex + "번 캐릭터 선택됨!");

        // 효과음
        if (AudioManager.Instance != null) AudioManager.Instance.PlaySFX("Select");

        // 2. 메인 게임 씬으로 이동
        SceneManager.LoadScene("Main");
    }
}
