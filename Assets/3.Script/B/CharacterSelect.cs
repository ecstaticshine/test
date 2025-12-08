using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] private RectTransform[] characterPanels;

    [SerializeField] private float moveDistance = 50f;
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float waitTime = 0.5f;

    private bool isSelecting = false;

    public void OnClick(int index)
    {
        if (isSelecting) return;

        StartCoroutine(Select_co(index));
    }

    private IEnumerator Select_co(int index)
    {
        isSelecting = true;

        RectTransform targetPanel = characterPanels[index];

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX("Click");
        }

        Vector2 startPos = targetPanel.anchoredPosition;
        Vector2 targetPos = startPos + new Vector2(0, moveDistance);

        float timer = 0f;
        while (timer < moveSpeed)
        {
            timer += Time.deltaTime;
            float progress = timer / moveSpeed;

            targetPanel.anchoredPosition = Vector2.Lerp(startPos, targetPos, Mathf.SmoothStep(0, 1, progress));

            yield return null;
        }

        targetPanel.anchoredPosition = targetPos;

        yield return new WaitForSeconds(waitTime);

        SceneLoader.Instance.SelectCharacterAndStart(index);

    }
}
