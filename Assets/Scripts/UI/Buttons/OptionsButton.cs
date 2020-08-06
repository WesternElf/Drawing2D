using GameControl;
using UnityEngine;

namespace UserInterface.Buttons
{
    public class OptionsButton : BaseButton
    {
        [SerializeField] private GameObject panelPrefab;

        protected override void ChoosedButton()
        {
            UIManager.Instance.InstantiateScreen(panelPrefab);
            GameController.Instance.PauseGame();
            
        }
    }
}

