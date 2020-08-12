using System;
using System.Collections.Generic;
using Extensions;
using GameControl;
using UnityEngine;
using UnityEngine.UI;
using UserInterface;

namespace DrawMechanic
{
    public class DrawLine : MonoBehaviour
    {
        public static Action OnDrawStarted;

        [SerializeField] private Image _lineLengthImg;
        [SerializeField] private GameObject _linePrefab;
        [SerializeField] private GameObject _currentLine;
        [SerializeField] private List<Vector2> _fingerPositions;

        private float _mouseDistance;
        private EdgeCollider2D _lineCollider;
        private LineRenderer _lineRenderer;
        private Vector2 _tempFingerPos;
        private int _currentPoint;
        private bool trackMouse = false;
        private Vector2 lastPosition;

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
                        DrawLines();
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
                    trackMouse = true;
                    DrawNewLine();
                    lastPosition = Input.mousePosition;
                }

                if (touch.phase == TouchPhase.Began)
                {
                    CreateLine();
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    trackMouse = false;
                }

                if (trackMouse)
                {
                    var newPosition = touch.position;
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

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                _tempFingerPos = Camera.main.ScreenToWorldPoint(touch.position);
                var dis = Vector2.Distance(_tempFingerPos, _fingerPositions[_fingerPositions.Count - 1]);
                if ( dis > 0.1f && dis < 2f)
                {
                    RaycastHit2D hit = Physics2D.Raycast(_tempFingerPos, Vector2.zero);
                    if (hit.collider.name == "Background")
                    {
                        UpdateLine(_tempFingerPos);
                    }
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
