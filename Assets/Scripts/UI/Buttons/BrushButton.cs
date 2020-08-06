using System;

namespace UserInterface.Buttons
{
    public class BrushButton : BaseButton
    {
        public static event Action OnBrushClickedEvent;

        protected override void ChoosedButton()
        {
            OnBrushClickedEvent?.Invoke();
        }
    }
}

