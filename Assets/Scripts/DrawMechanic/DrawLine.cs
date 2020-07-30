using System;
using System.Collections.Generic;
using Extensions;
using GameControl;
using UnityEngine;
using UserInterface;

namespace DrawMechanic
{
    public class DrawLine : MonoBehaviour
    {
        public Action OnDrawStarted;

        private static DrawLine _instance;

        [SerializeField] private GameObject _linePrefab;
        [SerializeField] private GameObject _currentLine;
        [SerializeField] private List<Vector2> _fingerPositions;
        [SerializeField] private  float maxMouseDistance = 500f;

        private EdgeCollider2D _lineCollider;
        private LineRenderer _lineRenderer;
        private Vector2 _tempFingerPos;
        private int _currentPoint;

        private float mouseDistance = 0f;
        private bool trackMouse = false;
        private Vector3 lastPosition;

        public float MouseDistance => mouseDistance;
        public float MaxMouseDistance => maxMouseDistance;

        public static DrawLine Instance => _instance;

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            UpdateManager.Instance.OnUpdateEvent += StateControl;
        }

        private void StateControl()
        {
            if (GameController.Instance.GameState == GameState.Play)
            {
                if (GameController.Instance.DrawState == DrawState.Draw)
                {
                    if (!UITouchHandler.IsPointerOverUIElement())
                    {
                        if (MouseDistance < MaxMouseDistance)
                        {
                            DrawLines();
                        }
                    }
                }
                else if (GameController.Instance.DrawState == DrawState.Erasure)
                {
                    ErasureLines();
                }
            }
        }

        private void DrawLines()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    DrawNewLine();
                    trackMouse = true;
                    lastPosition = Input.mousePosition;
                }

                if (touch.phase == TouchPhase.Began)
                {
                    CreateLine();
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

        private void ErasureLines()
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider.TryGetComponent(out EdgeCollider2D other))
            {
                Destroy(hit.collider.gameObject);
            }

        }
        
        private void DrawNewLine()
        {
            _tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector2.Distance(_tempFingerPos, _fingerPositions[_fingerPositions.Count - 1]) > 0.1f)
            {
                RaycastHit2D hit = Physics2D.Raycast(_tempFingerPos, Vector2.zero);
                if (hit.collider.name == "Background")
                {
                    UpdateLine(_tempFingerPos);
                }
            }
        }

        private void CreateLine()
        {
            if (GameController.Instance.DrawState == DrawState.Draw)
            {
                if (!UITouchHandler.IsPointerOverUIElement())
                {
                    _currentLine = Instantiate(_linePrefab, Vector3.zero, Quaternion.identity);
                    _currentLine.RemoveCloneFromName();


                    _lineRenderer = _currentLine.GetComponent<LineRenderer>();
                    _lineCollider = _currentLine.GetComponent<EdgeCollider2D>();

                    _fingerPositions.Clear();
                    AddFingerPos();

                    _lineCollider.points = _fingerPositions.ToArray();
                    _currentPoint = 2;
                }
            }
            else
            {
                return;
            }

        }

        private void UpdateLine(Vector2 newFingerPos)
        {
            _fingerPositions.Add(newFingerPos);
            _lineRenderer.positionCount++;
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, newFingerPos);
            _lineCollider.points = _fingerPositions.ToArray();
        }

        private void AddFingerPos()
        {
            for (int i = 0; i < 2; i++)
            {
                _fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                _lineRenderer.SetPosition(i, _fingerPositions[i]);
            }
        }
    }

}
