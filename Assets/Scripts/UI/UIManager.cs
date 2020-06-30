using Extensions;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _startScreen;
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
            }

            if (_instance == null)
            {
                _instance = new GameObject("UIManager", typeof(UIManager)).GetComponent<UIManager>();
            }

            return _instance;
        }
    }

    private void Start()
    {
        ColorButton.OnColorChoosedEvent += ColorPanel;
    }

    private void ColorPanel()
    {
        GameController.Instance.ChangeNewColor(ColorButton.ButtonColor);
    }

    public void InstantiateScreen(GameObject screen)
    {
        var startScreen = Instantiate(screen, gameObject.transform);
        startScreen.transform.parent = transform;
        startScreen.RemoveCloneFromName();
    }

    private void OnDestroy()
    {
        ColorButton.OnColorChoosedEvent -= ColorPanel;
    }
}
