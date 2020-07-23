using UnityEngine;

namespace UserInterface.Buttons
{
    public class ExitButton : BaseButton
    {
        public override void ChoosedButton()
        {
            Application.Quit();
        }
    }
}

