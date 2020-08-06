using UnityEngine;

namespace UserInterface.Buttons
{
    public class CloseSettingsButton : BaseButton
    {
        [SerializeField] private GameObject optionsWindow;

        protected override void ChoosedButton()
        {
            UIManager.Instance.CloseScreen(GameObject.Find("SettingsPanel"));
            UIManager.Instance.InstantiateScreen(optionsWindow);
        }
        
    }
}

