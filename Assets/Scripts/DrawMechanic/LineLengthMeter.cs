﻿using GameControl;
using UnityEngine;
using UnityEngine.UI;

namespace DrawMechanic
{
    public class LineLengthMeter : MonoBehaviour
    {
        private Image _lineLengthImg;

        private void Start()
        {
            _lineLengthImg = gameObject.GetComponent<Image>();
            DrawLine.OnDrawStarted += UpdateLength;
        }

        private void UpdateLength()
        {
            if (GameController.Instance.DrawState == DrawState.Draw)
            {
                _lineLengthImg.fillAmount = 1 - (GameController.Instance.MouseDistance / GameController.Instance.MaxMouseDistance);
            }

        }

        private void OnDisable()
        {
            DrawLine.OnDrawStarted -= UpdateLength;
        }
    }
}

