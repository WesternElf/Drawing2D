using UnityEngine;

namespace UserInterface.Buttons
{
    public class ExitButton : BaseButton
    {
        protected override void ChoosedButton()
        {
            Application.Quit();
        }
    }
}

