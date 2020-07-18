using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    private Text _coinCountText;
    private int _count;

    private void Start()
    {
        _count = 0;
        UIManager.Instance.OnCoinCountChanged += ChangeCoinCount;

        _coinCountText = GetComponent<Text>();
    }

    private void ChangeCoinCount()
    {
        _count++;
        _coinCountText.text = _count.ToString();
    }

    private void OnDestroy()
    {
        UIManager.Instance.OnCoinCountChanged -= ChangeCoinCount;
    }
}
