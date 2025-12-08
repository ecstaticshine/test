using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private GameObject backGround;
    [SerializeField] private Material Day;
    [SerializeField] private Material Night;

    private Light lightChanger;
    private Coroutine changeRoutine;
    private Renderer rd;

    [Header("빛 시간 설정")]
    public float StageTime = 60f;
    public float changeTime = 3f;

    [Header("빛 색상")]
    public LightSettings DayLightSetting;
    public LightSettings NightLightSetting;

    private void Awake()
    {
        TryGetComponent(out lightChanger);
        rd = backGround.GetComponent<Renderer>();
    }

    private void Start()
    {
        if (lightChanger != null)
        {
            lightChanger.color = DayLightSetting.lightColor;
            lightChanger.intensity = DayLightSetting.lightIntensity;

            if (rd != null) rd.material = Day;

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
            Material nextSkybox;

            yield return new WaitForSeconds(StageTime);

            if (currentState == TimeState.Day)
            {
                fromSettings = DayLightSetting;
                toSettings = NightLightSetting;
                nextSkybox = Night;
                currentState = TimeState.Night;
            }
            else
            {
                fromSettings = NightLightSetting;
                toSettings = DayLightSetting;
                nextSkybox = Day;
                currentState = TimeState.Day;
            }

            yield return StartCoroutine(Transition_co(fromSettings, toSettings, changeTime));

            if (rd != null) rd.material = nextSkybox;
        }
    }

    private IEnumerator Transition_co(LightSettings fromSettings, LightSettings toSettings, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float progress = Mathf.Clamp01(elapsedTime / duration);

            lightChanger.color = Color.Lerp(fromSettings.lightColor, toSettings.lightColor, progress);
            lightChanger.intensity = Mathf.Lerp(fromSettings.lightIntensity, toSettings.lightIntensity, progress);

            yield return null;
        }

        lightChanger.color = toSettings.lightColor;
        lightChanger.intensity = toSettings.lightIntensity;
    }
}