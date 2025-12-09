using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // 싱글톤 인스턴스

    [Header("오디오 소스 연결")]
    public AudioSource bgmPlayer; // 배경음악용 (Loop)
    public AudioSource sfxPlayer; // 효과음용 (OneShot)

    [Header("오디오 클립 등록")]
    public AudioClip[] bgmClips;  // BGM 목록
    public AudioClip[] sfxClips;  // 효과음 목록

    void Awake()
    {
        // 싱글톤 패턴 구현
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 파괴되지 않음
        }
        else
        {
            Destroy(gameObject); // 중복 생성 방지
        }
    }

    // --- BGM 관련 메서드 ---

    // 이름으로 BGM 재생
    public void PlayBGM(string bgmName)
    {
        AudioClip clip = Array.Find(bgmClips, x => x.name == bgmName);

        if (clip == null)
        {
            Debug.LogWarning("BGM을 찾을 수 없습니다: " + bgmName);
            return;
        }

        // 이미 같은 음악이 나오고 있다면 다시 재생하지 않음
        if (bgmPlayer.clip == clip && bgmPlayer.isPlaying) return;

        bgmPlayer.clip = clip;
        bgmPlayer.loop = true; // BGM은 무한 반복
        bgmPlayer.Play();
    }

    public void PlaySFX_PauseBGM(string sfxName)
    {
        StartCoroutine(PlaySFXPauseRoutine(sfxName));
    }

    private IEnumerator PlaySFXPauseRoutine(string sfxName)
    {
        bgmPlayer.Pause();              // 1) BGM 잠깐 멈춤
        AudioClip clip = Array.Find(sfxClips, x => x.name == sfxName);

        if (clip == null)
        {
            Debug.LogWarning("SFX를 찾을 수 없습니다: " + sfxName);
            yield break;
        }

        sfxPlayer.PlayOneShot(clip);   // 효과음 재생
        yield return new WaitForSeconds(clip.length); // 효과음 길이만큼 기다림

        bgmPlayer.UnPause();   // 다시 재생


    }

    // --- SFX (효과음) 관련 메서드 ---

    // 이름으로 효과음 재생
    public void PlaySFX(string sfxName)
    {
        AudioClip clip = Array.Find(sfxClips, x => x.name == sfxName);

        if (clip == null)
        {
            Debug.LogWarning("SFX를 찾을 수 없습니다: " + sfxName);
            return;
        }

        // PlayOneShot은 소리가 겹쳐도 끊기지 않고 겹쳐서 재생됨 (효과음 필수 기능)
        sfxPlayer.PlayOneShot(clip);
    }
}
