﻿using UnityEngine;
using UnityEngine.UI;

public class LineLenthMeter : MonoBehaviour
{
    private Image _lineLengthImg;

    private void Start()
    {
        _lineLengthImg = gameObject.GetComponent<Image>();
        DrawLine.Instance.OnDrawStarted += UpdateLength;
    }

    private void UpdateLength()
    {
        _lineLengthImg.fillAmount = 1 - (DrawLine.Instance.LineLength / DrawLine.Instance.MaxLineLength);
    }

    private void OnDestroy()
    {
        DrawLine.Instance.OnDrawStarted -= UpdateLength;
    }
}
