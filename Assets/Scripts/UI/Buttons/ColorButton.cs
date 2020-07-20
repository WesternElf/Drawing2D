using System;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Buttons
{
    public class ColorButton : BaseButton
    {
        public static Color ButtonColor;
        public static event Action OnColorChoosedEvent;

        public override void ChoosedButton()
        {
            ButtonColor = GetColor();
            OnColorChoosedEvent?.Invoke();
        }

        private Color GetColor()
        {
            var color = gameObject.GetComponent<Image>().color;
            return color;
        }
    }
}

