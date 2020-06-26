using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private static GameController _instance;
    [SerializeField] private LineParameters _lineParameters;

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

    private void Start()
    {
        UpdateManager.Instance.OnUpdateEvent += ColorChangeInput;
        print(_lineParameters.LineColors[0]);
        print(_lineParameters.LineColors[1]);
    }

    public void ColorChangeInput()
    {
        var linePrefab = (GameObject) Resources.Load("Prefabs/DrawLine");
        var lineRenderer = linePrefab.GetComponent<LineRenderer>();
        var input = Input.inputString;
        switch (input)
        {
            case "1":
                lineRenderer.colorGradient = ChangeColor(0);
                break;
            case "2":
                lineRenderer.colorGradient = ChangeColor(1);
                break;
            case "3":
                lineRenderer.colorGradient = ChangeColor(2);
                break;
            case "4":
                lineRenderer.colorGradient = ChangeColor(3);
                break;
            case "5":
                lineRenderer.colorGradient = ChangeColor(4);
                break;

        }
    }

    private Gradient ChangeColor(int keyCode)
    {
        var alpha = 1f;
        Gradient gradient = new Gradient();

        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(_lineParameters.LineColors[keyCode], 0.0f), new GradientColorKey(_lineParameters.LineColors[keyCode], 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );

        return gradient;
    }

    private void OnDestroy()
    {
        UpdateManager.Instance.OnUpdateEvent -= ColorChangeInput;
    }
}
