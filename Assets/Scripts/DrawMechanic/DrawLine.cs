﻿using System.Collections.Generic;
using Extensions;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField] private GameObject _linePrefab;
    [SerializeField] private GameObject _currentLine;
    [SerializeField] private List<Vector2> _fingerPositions;

    private EdgeCollider2D _lineCollider;
    private LineRenderer _lineRenderer;
    private Vector2 _tempFingerPos;
    private int _currentPoint;
    
    private void Start()
    {
        UpdateManager.Instance.OnUpdateEvent += InputControl;

    }

    private void InputControl()
    {
        if (GameController.Instance.State == DrawState.Draw)
        {
            DrawLines();
        }
        else if (GameController.Instance.State == DrawState.Erasure)
        {
            ErasureLines();
        }
    }

    private void DrawLines()
    {
        if (!UITouchHandler.IsPointerOverUIElement())
        {
            if (CalculateMousePos.Instance.MouseDistance < CalculateMousePos.Instance.MaxMouseDistance)
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
        }
    }

    private void ErasureLines()
    {
        if (Input.GetMouseButton(0))
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider.name != "Background")
            {
                Destroy(hit.collider.gameObject);
                print(hit.collider);
            }
              
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
