using System;
using System.Collections;
using GameControl;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.Buttons;

namespace UserInterface
{
    public class Timer : MonoBehaviour
    {
        private Text _timerText;
        private float timeCount;
        private const float startValue = 60f;
        private const string _clipName = "Timer";

        private void Start()
        {
            _timerText = GetComponent<Text>();
            timeCount = startValue;
            _timerText.text = timeCount.ToString();
            StartCoroutine(StartTimer());
            GameController.Instance.OnRestartedGame += ClearTimer;
        }

        private void ClearTimer()
        {
            timeCount = 60f;
        }

        private IEnumerator StartTimer()
        {
            while (timeCount > 0f)
            {
                yield return new WaitForSeconds(1f);
                timeCount--;
                _timerText.text = timeCount.ToString();
                if (timeCount == 5f)
                {
                    GameController.Instance.AudioManager.PlaySound(_clipName);
                }
            }
            UIManager.Instance.LoadLoseWindow();
        }

        private void OnDestroy()
        {
            GameController.Instance.OnRestartedGame -= ClearTimer;
        }

    }
}
