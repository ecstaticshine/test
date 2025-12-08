using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPopupController : MonoBehaviour
{
    [Header("UI 연결")]
    [Tooltip("도움말 팝업창 패널(Panel_HelpPopup)을 연결하세요.")]
    public GameObject helpPopupPanel;

    // 1. 팝업 열기 (도움말 버튼에 연결)
    public void OpenPopup()
    {
        // 클릭 효과음 재생 (선택 사항)
        if (AudioManager.Instance != null) AudioManager.Instance.PlaySFX("Click");

        // 패널을 켜서 보이게 함
        if (helpPopupPanel != null)
        {
            helpPopupPanel.SetActive(true);
        }
    }

    // 2. 팝업 닫기 (닫기 버튼에 연결)
    public void ClosePopup()
    {
        // 클릭 효과음 재생 (선택 사항)
        if (AudioManager.Instance != null) AudioManager.Instance.PlaySFX("Click");

        // 패널을 꺼서 안 보이게 함
        if (helpPopupPanel != null)
        {
            helpPopupPanel.SetActive(false);
        }
    }
}
