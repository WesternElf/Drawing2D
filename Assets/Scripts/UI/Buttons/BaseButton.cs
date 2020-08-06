using GameControl;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Buttons
{
    public class BaseButton : MonoBehaviour
    {
        private Button _button;
        private const string _audioClip = "ClickSFX";

        private void Awake()
        {
            _button = gameObject.GetComponent<Button>();
            _button.onClick.AddListener(ChoosedButton);
            _button.onClick.AddListener(PlaySound);
        }

        protected virtual void ChoosedButton()
        {
           
        }

        private void PlaySound()
        {
            GameController.Instance.AudioManager.PlaySound(_audioClip);
        }
    }

}
