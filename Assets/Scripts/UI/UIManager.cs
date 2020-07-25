using System;
using System.Collections.Generic;
using CoinPool;
using Extensions;
using GameControl;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.Buttons;

namespace UserInterface
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _gameButtons;
        [SerializeField] private GameObject _loseWindow;
        [SerializeField] private GameObject _winWindow;
        
        private static UIManager _instance;
        public event Action OnCoinCountChanged;

        public static UIManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<UIManager>();
                }

                if (_instance == null)
                {
                    _instance = new GameObject("UIManager", typeof(UIManager)).GetComponent<UIManager>();
                }

                return _instance;
            }
        }

        private void Start()
        {
            ColorButton.OnColorChoosedEvent += ColorPanel;
            Coin.OnPickedUp += CoinCountChanging;
        }

        public void LoadWinWindow()
        {
            InstantiateScreen(_winWindow);
            GameController.Instance.GameState = GameState.Pause;
        }

        public void LoadLoseWindow()
        {
            InstantiateScreen(_loseWindow);
            GameController.Instance.GameState = GameState.Pause;
        }

        internal void ActivatingButtons(bool activeStatus)
        {
            foreach (var button in _gameButtons)
            {
                button.SetActive(activeStatus);
            }
        }

        private void CoinCountChanging()
        {
            OnCoinCountChanged?.Invoke();
        }

        private void ColorPanel()
        {
            GameController.Instance.ChangeNewColor(ColorButton.ButtonColor);
        }

        internal void InstantiateScreen(GameObject screen)
        {
            var startScreen = Instantiate(screen, gameObject.transform);
            startScreen.transform.parent = transform;
            startScreen.RemoveCloneFromName();
        }

        internal void CloseScreen(GameObject screen)
        {
            Destroy(GameObject.Find(screen.name));
        }

        private void OnDestroy()
        {
            ColorButton.OnColorChoosedEvent -= ColorPanel;
            Coin.OnPickedUp -= CoinCountChanging;
        }
    }

}
