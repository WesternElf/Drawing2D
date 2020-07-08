using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _velocityX;
    private float _velocityY;

    private bool _rightTurn = true;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

    }


    private void Update()
    {
        _velocityX = Input.GetAxisRaw("Horizontal");
        _velocityY = _rigidbody.velocity.y;

        _rigidbody.velocity = new Vector2(_velocityX * _speed, _velocityY);
    }

    private void LateUpdate()
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

        if (((_rightTurn ) && (localScale.x < 0)) || ((!_rightTurn) && (localScale.x > 0)) )
        {
            localScale.x *= -1f;
        }
        transform.localScale = localScale;
    }
}
