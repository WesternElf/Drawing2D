using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField] private GameObject _linePrefab;
    [SerializeField] private GameObject _currentLine;
    [SerializeField] private List<Vector2> _fingerPositions;

    private EdgeCollider2D _lineCollider;
    private LineRenderer _lineRenderer;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(tempFingerPos, _fingerPositions[_fingerPositions.Count - 1]) > 0.1f)
            {
                UpdateLine(tempFingerPos);
            }
        }
    }

    private void CreateLine()
    {
        _currentLine = Instantiate(_linePrefab, Vector3.zero, Quaternion.identity);

        _lineRenderer = _currentLine.GetComponent<LineRenderer>();
        _lineCollider = _currentLine.GetComponent<EdgeCollider2D>();

        _fingerPositions.Clear();
        _fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        _fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        _lineRenderer.SetPosition(0, _fingerPositions[0]);
        _lineRenderer.SetPosition(1, _fingerPositions[1]);
        _lineCollider.points = _fingerPositions.ToArray();
    }

    private void UpdateLine(Vector2 newFingerPos)
    {
        _fingerPositions.Add(newFingerPos);
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, newFingerPos);
        _lineCollider.points = _fingerPositions.ToArray();
    }
}
