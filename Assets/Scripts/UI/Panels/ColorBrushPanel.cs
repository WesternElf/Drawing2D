using System.Collections.Generic;
using Extensions;
using UnityEngine;
using UnityEngine.UI;

public class ColorBrushPanel : MonoBehaviour
{

    [SerializeField] private List<Color> _colors;
    [SerializeField] private GameObject _colorButtons;

    private void Start()
    {
        ColorButton.OnColorChoosedEvent += CloseColorPanel;

        CreateButtons();
    }

    private void CloseColorPanel()
    {
        gameObject.SetActive(false);
    }

    

    private void CreateButtons()
    {
        for (int i = 0; i < _colors.Count; i++)
        { 
            var newButton = Instantiate(_colorButtons, gameObject.transform.position, gameObject.transform.rotation);
            newButton.transform.SetParent(gameObject.transform);
            newButton.RemoveCloneFromName();
            ChangeColorButton(newButton, _colors[i]);
        }
    }

    private void ChangeColorButton(GameObject newButton, Color color)
    {
        var newColor = color;
        newColor.a = 1;
        newButton.GetComponent<Image>().color = newColor;
    }

    

    private void OnDestroy()
    {
        ColorButton.OnColorChoosedEvent -= CloseColorPanel;

    }

}
