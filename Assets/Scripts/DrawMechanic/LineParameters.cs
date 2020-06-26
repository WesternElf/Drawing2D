using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LineParameters
{
    [SerializeField] private List<Color> _lineColors;

    public List<Color> LineColors => _lineColors;
}
