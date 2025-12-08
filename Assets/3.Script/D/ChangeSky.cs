using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.RayTracingAccelerationStructure;

public enum TimeState
{
    Day,
    Night
}

[System.Serializable]
public struct LightSettings
{
    public Color lightColor;    // 조명의 색상
    public float lightIntensity; // 조명의 밝기 (Intensity)
}

public class ChangeSky : MonoBehaviour
{
    private Light lightChanger;
    private Coroutine changeRoutine;// 코루틴 제어

    [Header("빛 시간 설정")]
    public float StageTime = 60f;
    public float changeTime = 3f;


    [Header("빛 색상")]
    public LightSettings DayLightSetting;
    public LightSettings NightLightSetting;

    private void Awake()
    {
        TryGetComponent(out lightChanger);
    }
    private void Start()
    {
        if (lightChanger != null)
        {
            lightChanger.color = DayLightSetting.lightColor;
            lightChanger.intensity = DayLightSetting.lightIntensity;
            changeRoutine = StartCoroutine(DayNight_co());
        }
    }
    private IEnumerator DayNight_co()
    {
        TimeState currentState = TimeState.Day;

        while (true)
        {
            LightSettings fromSettings;
            LightSettings toSettings;
            TimeState nextState;
            if (currentState == TimeState.Day)
            {
                // 낮 -> 밤 전환 준비
                fromSettings = DayLightSetting;
                toSettings = NightLightSetting;
                nextState = TimeState.Night;
            }
            else // TimeState.Night
            {
                // 밤 -> 낮 전환 준비
                fromSettings = NightLightSetting;
                toSettings = DayLightSetting;
                nextState = TimeState.Day;
            }

            yield return new WaitForSeconds(StageTime);
            yield return StartCoroutine(Transition_co(fromSettings, toSettings, changeTime));

            currentState = nextState;

        }

    }
    private IEnumerator Transition_co(LightSettings fromSettings, LightSettings toSettings, float duration)
    {
        float elapsedTime = 0f;
        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime; 

            // 0.0 ~ 1.0 사이의 진행률 계산 및 클램프
            float progress = Mathf.Clamp01(elapsedTime / duration);

            lightChanger.color = Color.Lerp(fromSettings.lightColor, toSettings.lightColor, progress);

            lightChanger.intensity = Mathf.Lerp(fromSettings.lightIntensity, toSettings.lightIntensity, progress);

            yield return null; // 다음 프레임까지 대기
        }

        // 최종 목표 상태로 정확히 설정하여 오차 방지
        lightChanger.color = toSettings.lightColor;
        lightChanger.intensity = toSettings.lightIntensity;
    }
}

