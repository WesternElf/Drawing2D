using System;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private PhysicsMaterial2D _lowFriction;
    [SerializeField] private PhysicsMaterial2D _normalFriction;
    private float _velocityX;
    private float _velocityY;
    
    private bool _rightTurn = true;
    [SerializeField]private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private PhysicsMaterial2D _playerMaterial;

    private const string MoveParameter = "IsMoving";
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        UpdateManager.Instance.OnUpdateEvent += MovingControl;
    }


    private void MovingControl()
    {
        RotateGroundAngle();
        _rigidbody.velocity = MoveCharacter();
        transform.localScale = RotateMirror();
       
        if (_rigidbody.velocity.x != 0)
        { 
            SetAnimParam(true);
            SetMaterial(_lowFriction);
        }
        else
        {
            SetAnimParam(false);
            SetMaterial(_normalFriction);
        
        }
    }

    private void RotateGroundAngle()
    {
        //RaycastHit hit;
        //Vector3 down = transform.TransformDirection(-Vector3.up) * 0.5f;
        //Physics.Raycast(transform.position, down, out hit);

        //Quaternion groundTilt = Quaternion.FromToRotation(Vector3.up, hit.normal);
        //Debug.Log(groundTilt);
        //transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);
        //if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        //    print("Found an object - distance: " + hit.distance);

    }
    //    Ray ray = new Ray();
    //    RaycastHit hit;
    //    Vector3 axis;
    //    float angle;
    //    ray.origin = _transform.position;
    //    ray.direction = -_transform.up;

    //    Physics.Raycast(ray, out hit);

    //    axis = Vector3.Cross(-_transform.up, -hit.normal);
    //    print(-hit.normal);
    //    Debug.DrawRay(_transform.position, -_transform.up, Color.red);
    //    Debug.DrawLine(-_transform.up, -hit.normal, Color.blue);
    //    if (axis != Vector3.zero)
    //    {
    //        _rigidbody.freezeRotation = false;
    //        print("Rotate");
    //        angle = Mathf.Atan2(Vector3.Magnitude(axis), Vector3.Dot(-_transform.up, -hit.normal));

    //        _transform.Rotate(axis, angle);
    //    }


    private void SetMaterial(PhysicsMaterial2D material)
    {
        _playerMaterial = material;
        _rigidbody.sharedMaterial = _playerMaterial;
        
    }

    private void SetAnimParam(bool paramState)
    {
        _animator.SetBool(MoveParameter, paramState);
    }

    private Vector2 MoveCharacter()
    {
        _velocityX = Input.GetAxisRaw("Horizontal");
        _velocityY = _rigidbody.velocity.y;

        return new Vector2(_velocityX * _speed, _velocityY);
    }

    private Vector3 RotateMirror()
    {
        var localScale = transform.localScale;
        if (_velocityX > 0)
        {
            _rightTurn = true;

        }
        else if (_velocityX < 0)
        {
            _rightTurn = false;
        }

        if (((_rightTurn) && (localScale.x < 0)) || ((!_rightTurn) && (localScale.x > 0)))
        {
            localScale.x *= -1f;
        }

        return localScale;
    }

    private void OnDestroy()
    {
        UpdateManager.Instance.OnUpdateEvent -= MovingControl;
    }
}


