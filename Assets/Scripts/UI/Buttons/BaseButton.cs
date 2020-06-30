using UnityEngine;
using UnityEngine.UI;

public class BaseButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = gameObject.GetComponent<Button>();
        _button.onClick.AddListener(ChoosedButton);
    }

    public virtual void ChoosedButton()
    {
        print("Click");
    }

}
