using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    public Transform enemyTransform; // 적의 Transform
    public RectTransform hpBarUI;   // HP 바 UI의 RectTransform
    public Camera mainCamera;       // 메인 카메라

    void Update()
    {
        if (enemyTransform != null && mainCamera != null)
        {
            // 월드 좌표를 화면 좌표로 변환
            Vector3 screenPos = mainCamera.WorldToScreenPoint(enemyTransform.position + Vector3.up * 2.0f); // 적 머리 위로 오프셋
            hpBarUI.position = screenPos;
        }
    }
}
