using System;
using UnityEngine;

namespace GameControl
{
    public class InputControl : MonoBehaviour
    {
        public static event Action OnTouchMoved;
        public static event Action OnTouchBegan;

        public Action OnDrawStarted;

        [SerializeField] private float maxMouseDistance = 500f;
        private float mouseDistance = 0f;
        private bool trackMouse = false;
        private Vector3 lastPosition;

        private static InputControl _instance;

        public static InputControl Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<InputControl>();
                }

                if (_instance == null)
                {
                    _instance = new GameObject("InputControl", typeof(InputControl)).GetComponent<InputControl>();
                }

                return _instance;
            }
        }

        public float MouseDistance => mouseDistance;
        public float MaxMouseDistance => maxMouseDistance;

        private void Start()
        {
            UpdateManager.Instance.OnUpdateEvent += TouchControl;
        }

        private void TouchControl()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    OnTouchMoved?.Invoke();
                    trackMouse = true;
                    lastPosition = Input.mousePosition;
                }

                if (touch.phase == TouchPhase.Began)
                {
                    OnTouchBegan?.Invoke();
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    trackMouse = false;
                    mouseDistance = 0f;
                }

                if (trackMouse)
                {
                    OnDrawStarted?.Invoke();
                    var newPosition = Input.mousePosition;
                    mouseDistance += (newPosition - lastPosition).magnitude;
                    lastPosition = newPosition;
                }

            }
        }
    }
}

