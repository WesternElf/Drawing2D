using GameControl;
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
            DrawLine.Instance.OnDrawStarted += UpdateLength;
        }

        private void UpdateLength()
        {
            if (GameController.Instance.DrawState == DrawState.Draw)
            {
                _lineLengthImg.fillAmount = 1 - (DrawLine.Instance.MouseDistance / DrawLine.Instance.MaxMouseDistance);
            }

        }

        private void OnDisable()
        {
            DrawLine.Instance.OnDrawStarted -= UpdateLength;
        }
    }
}

