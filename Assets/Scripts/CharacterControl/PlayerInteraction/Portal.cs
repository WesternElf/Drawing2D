using System.Collections;
using GameControl;
using UnityEngine;
using UserInterface;

namespace CharacterControl
{
    public class Portal : InteractionMovement
    {
        private const string _clipName = "Teleport";
        protected override void PlayerTriggered()
        {
            GameController.Instance.AudioManager.PlaySound(_clipName);
            StartCoroutine(OpenWinWindow());

        }

        private IEnumerator OpenWinWindow()
        {
            yield return new WaitForSeconds(0.1f);
            UIManager.Instance.LoadWinWindow();
        }
    }

}

