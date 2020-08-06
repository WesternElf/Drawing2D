using GameControl;
using UnityEngine;

namespace UserInterface.Buttons
{
    public class ResumeButton : BaseButton
    {
        [SerializeField] private GameObject panelPrefab;

        protected override void ChoosedButton()
        {
            UIManager.Instance.CloseScreen(panelPrefab);
            GameController.Instance.ResumeGame();
        }
    }
}

