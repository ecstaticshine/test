using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class C_TitleController : MonoBehaviour
{
    public void OnClickStart()
    {
        // 효과음 재생 (선택 사항)
        if (AudioManager.Instance != null) AudioManager.Instance.PlaySFX("Click");

        // 캐릭터 선택 씬으로 이동
        SceneManager.LoadScene("CharacterSelect");
    }
}
