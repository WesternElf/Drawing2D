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
            InputControl.Instance.OnDrawStarted += UpdateLength;
        }

        private void UpdateLength()
        {
            if (GameController.Instance.State == DrawState.Draw)
            {
                _lineLengthImg.fillAmount = 1 - (InputControl.Instance.MouseDistance / InputControl.Instance.MaxMouseDistance);
            }

        }

        private void OnDisable()
        {
            InputControl.Instance.OnDrawStarted -= UpdateLength;
        }
    }
}

