﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class EraserToggle : MonoBehaviour
{
    public static Action OnToggleClicked; 

    private Toggle _eraserToggle;

    private void Start()
    {
        _eraserToggle = gameObject.GetComponent<Toggle>();
        _eraserToggle.onValueChanged.AddListener(delegate {EraserClicked(_eraserToggle);});
    }

    private void EraserClicked(Toggle change)
    {
        OnToggleClicked?.Invoke();
        print("Toggle clicked" + change.isOn);
    }
}