using System;
using UnityEngine;
using UserInterface;
using UserInterface.Buttons;

public enum DrawState
{
    Draw, Erasure
}

public enum GameState
{
    Play, Pause
}

namespace GameControl
{
    public class GameController : MonoBehaviour
    {
        public Action OnStateChanged;
        private static GameController _instance;
        private DrawState _drawState;
        private GameState _gameState;

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

        public GameState GameState
        {
            get { return _gameState; }
            set
            {
                if (value == GameState.Play)
                {
                    Time.timeScale = 1.0f;
                    UIManager.Instance.ActivatingButtons(true);
                    
                }
                else
                {
                    Time.timeScale = 0.0f;
                    UIManager.Instance.ActivatingButtons(false);
                    DrawState = DrawState.Draw;
                }

                _gameState = value;
            }
        }

        public DrawState DrawState { get => _drawState; internal set => _drawState = value; }

        private void Start()
        {
            DontDestroyOnLoad(this);
            //GameState = GameState.Play;
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

        private void OnDisabled()
        {
            EraserToggle.OnToggleClicked -= ChangeDrawState;
        }

    }
}

