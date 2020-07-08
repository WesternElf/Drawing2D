using UnityEngine;

public class DamagerMovement : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    private Transform _objTransform;

    private void Start()
    {
        _objTransform = gameObject.GetComponent<Transform>();
    }

    private void Update()
    {
        _objTransform.Rotate(new Vector3(0f, 0f, -1f) * _rotateSpeed);
    }
}
