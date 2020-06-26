using System;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public event Action OnDrawStarted;
    private static DrawLine _instance;

    [SerializeField] private GameObject _linePrefab;
    [SerializeField] private GameObject _currentLine;
    [SerializeField] private float _maxLineLength;
    [SerializeField] private List<Vector2> _fingerPositions;

    private EdgeCollider2D _lineCollider;
    private LineRenderer _lineRenderer;
    private Vector2 _tempFingerPos;
    private float _lineLength;
    private int _currentPoint;

    public float LineLength => _lineLength;
    public float MaxLineLength => _maxLineLength;
    public static DrawLine Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DrawLine>();
            }

            if (_instance == null)
            {
                _instance = new GameObject("DrawLine", typeof(DrawLine)).GetComponent<DrawLine>();
            }

            return _instance;
        }
    }

    private void Start()
    {
        UpdateManager.Instance.OnUpdateEvent += InputControl;
    }

    private void InputControl()
    {
        if (_lineLength <= _maxLineLength)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CreateLine();
            }

            if (Input.GetMouseButton(0))
            {
                OnDrawStarted?.Invoke();
                DrawNewLine();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _lineLength = 0f;
        }
    }

    private void DrawNewLine()
    {
        _tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Vector2.Distance(_tempFingerPos, _fingerPositions[_fingerPositions.Count - 1]) > 0.1f)
        {
            RaycastHit2D hit = Physics2D.Raycast(_tempFingerPos, Vector2.zero);
            if (hit.collider != null)
            {
                UpdateLine(_tempFingerPos);
            }
        }
    }

    private void CreateLine()
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

    private void UpdateLine(Vector2 newFingerPos)
    {
        _fingerPositions.Add(newFingerPos);
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, newFingerPos);
        _lineCollider.points = _fingerPositions.ToArray();

        _lineLength += Vector2.Distance(_fingerPositions[_currentPoint], _fingerPositions[_currentPoint - 1]);
    }

    private void AddFingerPos()
    {
        for (int i = 0; i < 2; i++)
        {
            _fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            _lineRenderer.SetPosition(i, _fingerPositions[i]);
        }
    }

    private void OnDisable()
    {
        UpdateManager.Instance.OnUpdateEvent -= InputControl;
    }
}
