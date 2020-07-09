using UnityEngine;

public class DamagerMovement : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    private Transform _objTransform;

    private void Start()
    {
        _objTransform = gameObject.GetComponent<Transform>();
        UpdateManager.Instance.OnUpdateEvent += SawRotate;
    }

    private void SawRotate()
    {
        _objTransform.Rotate(new Vector3(0f, 0f, -1f) * _rotateSpeed);
    }
}
