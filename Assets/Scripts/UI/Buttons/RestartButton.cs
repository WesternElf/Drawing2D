using System;
using GameControl;
using UnityEngine;

namespace UserInterface.Buttons
{
    public class RestartButton : BaseButton
    {
        public static event Action OnGameRestarted;
        [SerializeField] private GameObject panelPrefab;

        public override void ChoosedButton()
        {
            UIManager.Instance.CloseScreen(panelPrefab);
            GameController.Instance.GameState = GameState.Play;
            OnGameRestarted?.Invoke();
        }
    }
}
