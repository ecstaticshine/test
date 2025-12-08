using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // 싱글톤

    [Header("게임 스테이터스")]
    public int score = 0;
    public int currentHealth = 3;
    public int maxHealth = 3;

    [Header("아이템 설정")]
    public int coinScore = 100;      // 코인 점수
    public int healToScore = 500;    // 회복템 -> 점수 변환 시 점수

    public TMP_Text Score_Text;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Update()
    {
        Score_Text.text = string.Format("SCORE : {0:D6}", score);
    }

    // 코인 획득 시 호출
    public void GetCoin()
    {
        score += coinScore;
        Debug.Log("코인 획득! 현재 점수: " + score);

        // 효과음 재생 (아까 만든 오디오 매니저 활용)
        AudioManager.Instance.PlaySFX("Coin");
    }

    // 회복 아이템 획득 시 호출
    public void GetHealItem()
    {
        // 핵심 기획: 피가 꽉 찼으면 점수로!
        if (currentHealth >= maxHealth)
        {
            score += healToScore;
            Debug.Log("체력 Full! 점수로 변환: " + score);
            AudioManager.Instance.PlaySFX("Coin"); // 돈 버는 소리
        }
        else
        {
            currentHealth++;
            Debug.Log("체력 회복! 현재 체력: " + currentHealth);
            AudioManager.Instance.PlaySFX("Heal"); // 회복 소리
        }
    }

    // (참고) 장애물 충돌 시 호출할 함수
    public void TakeDamage()
    {
        currentHealth--;
        AudioManager.Instance.PlaySFX("Crash");

        if (currentHealth <= 0)
        {
            Debug.Log("게임 오버");
            // 여기서 게임 오버 UI 띄우기
        }
    }
}
