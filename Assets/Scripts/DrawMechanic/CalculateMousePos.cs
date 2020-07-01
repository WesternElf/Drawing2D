using System;
using UnityEngine;

public class CalculateMousePos : MonoBehaviour
{
    public Action OnDrawStarted;

    [SerializeField] private float maxMouseDistance = 500f;
    private float mouseDistance = 0f;
    private bool trackMouse = false;
    private Vector3 lastPosition;

    private const string fireButton = "Fire1";

    private static CalculateMousePos _instance;

    public static CalculateMousePos Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CalculateMousePos>();
            }

            if (_instance == null)
            {
                _instance = new GameObject("CalculateMousePos", typeof(CalculateMousePos)).GetComponent<CalculateMousePos>();
            }

            return _instance;
        }
    }

    public float MouseDistance => mouseDistance;
    public float MaxMouseDistance => maxMouseDistance; 

    private void Start()
    {
        UpdateManager.Instance.OnUpdateEvent += CalculateLineLength;
    }

    private void CalculateLineLength()
    {
        if (Input.GetButtonDown(fireButton))
        {
            trackMouse = true;
            lastPosition = Input.mousePosition;
        }
        if (Input.GetButtonUp(fireButton))
        {
            trackMouse = false;
            mouseDistance = 0f;
        }
        if (trackMouse)
        {
            OnDrawStarted?.Invoke();
            var newPosition = Input.mousePosition;
            mouseDistance += (newPosition - lastPosition).magnitude;
            lastPosition = newPosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseDistance = 0f;
        }
    }

    public float GetLineLength()
    {
    
        return MouseDistance;
    }
}
