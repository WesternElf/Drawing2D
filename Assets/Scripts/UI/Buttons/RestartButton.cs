using System;
using GameControl;
using UnityEngine;


namespace UserInterface.Buttons
{
    public class RestartButton : BaseButton
    {
        [SerializeField] private GameObject panelPrefab;

        public override void ChoosedButton()
        {
            UIManager.Instance.CloseScreen(panelPrefab);
            GameController.Instance.RestartGame();
        }
    }
}
