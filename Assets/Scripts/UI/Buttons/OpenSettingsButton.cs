using UnityEngine;

namespace UserInterface.Buttons
{
    public class OpenSettingsButton : BaseButton
    {
        [SerializeField] private GameObject settingsWindow;

        public override void ChoosedButton()
        {
            UIManager.Instance.CloseScreen(GameObject.Find("OptionsPanel"));
            UIManager.Instance.InstantiateScreen(settingsWindow);
        }
    }
}

