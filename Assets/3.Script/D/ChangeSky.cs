using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ChangeSky : MonoBehaviour
{
    private Light lightChanger;
    private Coroutine changeRoutine;// 코루틴 제어
    [Header("빛 시간 설정")]
    public float changeTime = 60f;


    [Header("빛 색상")]
    public Color CurrentColor = Color.white;
    public Color ChangeColor = Color.gray;

    private void Awake()
    {
        TryGetComponent(out lightChanger);
    }
    private void Start()
    {
        lightChanger.color = CurrentColor;
        StartCoroutine(SkyChange_co(CurrentColor,ChangeColor,changeTime));
    }
    private IEnumerator SkyChange_co(Color BasicColor, Color EndColor, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float TT = elapsedTime / duration;
            //이런 내 맘 모르고
            TT = Mathf.Clamp01(TT);
            lightChanger.color = Color.Lerp(BasicColor, EndColor, TT);
            yield return null;
        }
        //yield return new WaitForSeconds(changeTime);
        lightChanger.color = EndColor;
    }
    public void newChangeColor(Color TargetColor, float Duration)
    {
        if (changeRoutine != null)
        {
            StopCoroutine(changeRoutine);
        }
        Color newStartColor = lightChanger.color;
        changeRoutine = StartCoroutine(SkyChange_co(newStartColor, TargetColor, Duration));
    }
}

