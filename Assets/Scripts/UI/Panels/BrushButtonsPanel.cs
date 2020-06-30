using System.Collections.Generic;
using UnityEngine;

public class BrushButtonsPanel : MonoBehaviour
{
    [SerializeField] private List<GameObject> _brushPanels;

    private void Start()
    {
        BrushButton.OnBrushClickedEvent += ActivationPanel;
    }

    private void ActivationPanel()
    {
        for (int i = 0; i < _brushPanels.Count; i++)
        {
            if (!_brushPanels[i].activeInHierarchy)
            {
                _brushPanels[i].SetActive(true);
            }
        }
    }

    private void OnDestroy()
    {
        BrushButton.OnBrushClickedEvent -= ActivationPanel;
    }

}
