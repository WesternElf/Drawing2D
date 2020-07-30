using System;
using Audio;
using LoadSaveData;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        [SerializeField] private AudioManager _audioManager;
        public Action OnStateChanged;
        public Action OnRestartedGame;
        private static GameController _instance;
        private DrawState _drawState;
        private GameState _gameState;
        private GameParameters _soundParams;


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
                    DrawState = DrawState.Draw;
                }
                else
                {
                    Time.timeScale = 0.0f;
                    UIManager.Instance.ActivatingButtons(false);

                    AudioManager.PlayMusic(true);
                }

                _gameState = value;
            }
        }

        public DrawState DrawState { get => _drawState; internal set => _drawState = value; }
        public AudioManager AudioManager { get => _audioManager; private set => _audioManager = value; }
        public GameParameters SoundParams { get => _soundParams; set => _soundParams = value; }


        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                if (_instance != this)
                {
                    Destroy(gameObject);
                }
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            AudioManager.PlayMusic(true);
            InitializeSounds();
        }

        public void InitializeSounds()
        {

            var loadedData = LoadSaveToJSON.LoadParams();

            if (loadedData != null)
            {
                SoundParams = loadedData;
            }
            else
            {
                SoundParams = new GameParameters();
            }

            AudioManager.MusicVolume = SoundParams.MusicVolume / 100;
            AudioManager.SfxVolume = SoundParams.SoundVolume / 100;
        }

        public void RestartGame()
        {
            LoadSaveToJSON.ClearAllData();
            SceneManager.LoadScene(0);
            GameState = GameState.Play;
            AudioManager.PlayMusic(true);
        }

        public void PauseGame()
        {
            GameState = GameState.Pause;
            AudioManager.PlayMusic(false);
        }

        public void ResumeGame()
        {
            Instance.GameState = GameState.Play;
            AudioManager.PlayMusic(true);
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

