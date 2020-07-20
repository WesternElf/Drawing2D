using GameControl;
using LoadSaveData;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class CoinCounter : MonoBehaviour
    {
        private Text _coinCountText;
        private GameParameters _coinParam;
        private int _count;

        public int Count { get => _count; private set => _count = value; }

        private void Start()
        {
            UIManager.Instance.OnCoinCountChanged += ChangeCoinCount;

            _coinCountText = GetComponent<Text>();

            Initialize();
        }


        private void Initialize()
        {
            var loadedData = LoadSaveToJSON.LoadParams();

            if (loadedData != null)
            {
                _coinParam = loadedData;
            }
            else
            {
                _coinParam = new GameParameters();
            }
            _coinCountText.text = _coinParam.CoinCount.ToString();

        }

        private void ChangeCoinCount()
        {
            _coinParam.CoinCount++;
            _coinCountText.text = _coinParam.CoinCount.ToString();
            LoadSaveToJSON.SaveParams(_coinParam);
        }

        private void OnDestroy()
        {
            UIManager.Instance.OnCoinCountChanged -= ChangeCoinCount;
        }
    }
}

