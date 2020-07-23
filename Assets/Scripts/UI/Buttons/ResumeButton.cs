using GameControl;
using UnityEngine;

namespace UserInterface.Buttons
{
    public class ResumeButton : BaseButton
    {
        [SerializeField] private GameObject panelPrefab;

        public override void ChoosedButton()
        {
            UIManager.Instance.CloseScreen(panelPrefab);
            GameController.Instance.GameState = GameState.Play;
        }
    }
}

