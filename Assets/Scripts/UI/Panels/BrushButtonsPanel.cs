using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrushButtonsPanel : MonoBehaviour
{
    [SerializeField] private List<GameObject> _brushPanels;


    private void Start()
    {
        BrushButton.OnBrushClickedEvent += ActivationPanel;
        EraserToggle.OnToggleClicked += ChangeDrawState;
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

    private void ChangeDrawState()
    {
        if (GameController.Instance.State == DrawState.Draw)
        {
            GameController.Instance.State = DrawState.Erasure;
        }
        else
        {
            GameController.Instance.State = DrawState.Draw;
        }

    }

    private void OnDestroy()
    {
        BrushButton.OnBrushClickedEvent -= ActivationPanel;
        EraserToggle.OnToggleClicked -= ChangeDrawState;
    }

}
