using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance; // 어디서든 접근 가능한 정적 변수

    private void Awake()
    {
        // 싱글톤 패턴 구현
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 파괴되지 않음!
        }
        else
        {
            // 이미 만들어진 SceneLoader가 있다면, 새로 생긴 나는 파괴됨 (중복 방지)
            Destroy(gameObject);
        }
    }

    // 1. [타이틀 -> 캐릭터 선택]
    public void LoadSelectScene()
    {
        PlayClickSound();
        SceneManager.LoadScene("CharacterSelect");
    }

    // 2. [캐릭터 선택 -> 메인 게임]
    // 버튼 OnClick에서 매개변수(0, 1, 2)를 입력하세요.
    public void SelectCharacterAndStart(int charIndex)
    {
        PlayClickSound();

        // 선택한 캐릭터 번호 저장
        PlayerPrefs.SetInt("SelectedCharacter", charIndex);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Main");
    }

    // 3. [메인 게임 -> 결과창]
    // 점수를 받아서 저장하고 넘어갑니다.
    public void LoadInputScoreScene(int score)
    {
        // 점수 저장
        PlayerPrefs.SetInt("Score", score);

        SceneManager.LoadScene("InputScore");
    }

    // 3. [메인 게임 -> 결과창]
    // 점수를 받아서 저장하고 넘어갑니다.
    public void LoadResultScene()
    {
        SceneManager.LoadScene("Result");
    }

    // 4. [결과창 -> 게임 재시작] (Retry)
    public void RestartGame()
    {
        PlayClickSound();
        Time.timeScale = 1f; // 멈춘 시간 다시 흐르게
        B_GameManager.instance.canGameOverInput = false;
        SceneManager.LoadScene("Main"); // 바로 메인으로
    }

    // 5. [결과창 -> 타이틀로] (Home)
    public void LoadTitleScene()
    {
        PlayClickSound();
        Time.timeScale = 1f; // 멈춘 시간 다시 흐르게
        SceneManager.LoadScene("Title");
    }

    // 6. 게임 종료
    public void QuitGame()
    {
        PlayClickSound();
        Application.Quit();
    }

    // (보조) 소리 재생
    private void PlayClickSound()
    {
        // 오디오 매니저가 존재할 때만 재생
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX("Click");
    }
}
