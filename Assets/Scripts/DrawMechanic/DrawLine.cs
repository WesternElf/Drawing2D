using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField] private GameObject _linePrefab;
    [SerializeField] private GameObject _currentLine;
    [SerializeField] private List<Vector2> _fingerPositions;

    private EdgeCollider2D _lineCollider;
    private LineRenderer _lineRenderer;
    private Vector2 _tempFingerPos;

    private void Start()
    {
        UpdateManager.Instance.OnUpdateEvent += InputControl;
    }

    private void InputControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }

        if (Input.GetMouseButton(0))
        {
            DrawNewLine();
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

        _lineRenderer = _currentLine.GetComponent<LineRenderer>();
        _lineCollider = _currentLine.GetComponent<EdgeCollider2D>();

        _fingerPositions.Clear();
        AddFingerPos();

        _lineCollider.points = _fingerPositions.ToArray();
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

    private void OnDestroy()
    {
        UpdateManager.Instance.OnUpdateEvent -= InputControl;
    }
}
