using System;

public class BrushButton : BaseButton
{
    public static event Action OnBrushClickedEvent;

    public override void ChoosedButton()
    {
        OnBrushClickedEvent?.Invoke();
    }
}
