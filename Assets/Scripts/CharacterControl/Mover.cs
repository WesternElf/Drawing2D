using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _velocityX;
    private float _velocityY;

    private bool _rightTurn = true;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private const string MoveParameter = "IsMoving";

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        UpdateManager.Instance.OnUpdateEvent += MovingControl;
    }


    private void MovingControl()
    {
        _rigidbody.velocity = MoveCharacter();
        transform.localScale = RotateMirror();
       
        if (_rigidbody.velocity.x != 0)
        { 
            SetAnimParam(true);
        }
        else
        {
            SetAnimParam(false);
        }
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
}
