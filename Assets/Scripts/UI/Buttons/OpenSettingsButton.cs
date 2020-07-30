using GameControl;
using UnityEngine;

namespace UserInterface.Buttons
{
    public class OpenSettingsButton : BaseButton
    {
        [SerializeField] private GameObject openedWindow;
        [SerializeField] private GameObject closedWindow;

        public override void ChoosedButton()
        {
            UIManager.Instance.CloseScreen(closedWindow);
            UIManager.Instance.InstantiateScreen(openedWindow);
    
        }
    }
}

