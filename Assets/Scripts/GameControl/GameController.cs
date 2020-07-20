using System;
using UnityEngine;
using UserInterface.Buttons;

public enum DrawState
{
    Draw, Erasure
}

namespace GameControl
{
    public class GameController : MonoBehaviour
    {
        public Action OnStateChanged;
        private static GameController _instance;
        private DrawState state;

        public static GameController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameController>();
                }

                if (_instance == null)
                {
                    _instance = new GameObject("GameController", typeof(GameController)).GetComponent<GameController>();
                }

                return _instance;
            }
        }

        public DrawState State
        {
            get { return state; }
            set { state = value; }

        }

        private void Start()
        {
            EraserToggle.OnToggleClicked += ChangeDrawState;
        }

        private void ChangeDrawState()
        {
            OnStateChanged?.Invoke();
        }

        public void ChangeNewColor(Color color)
        {
            var alpha = 1f;
            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(color, 0.0f), new GradientColorKey(color, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            );

            var linePrefab = (GameObject)Resources.Load("Prefabs/DrawLine");
            var lineRenderer = linePrefab.GetComponent<LineRenderer>();

            lineRenderer.colorGradient = gradient;
        }

    }
}

