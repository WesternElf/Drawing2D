using UnityEngine;
using UnityEngine.UI;

public class LineLengthMeter : MonoBehaviour
{
    private Image _lineLengthImg;

    private void Start()
    {
        _lineLengthImg = gameObject.GetComponent<Image>();
        CalculateMousePos.Instance.OnDrawStarted += UpdateLength;
    }

    private void UpdateLength()
    {
        _lineLengthImg.fillAmount = 1 - (CalculateMousePos.Instance.MouseDistance / CalculateMousePos.Instance.MaxMouseDistance);
    }

    private void OnDisable()
    {
        CalculateMousePos.Instance.OnDrawStarted -= UpdateLength;
    }
}
