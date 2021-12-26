using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float _walkingSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _deceleration;
    [SerializeField] private float _extremumWalkVelocity;
    [SerializeField] private float _extremumRunVelocity;
    private float _currentExtremum;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private float _velocityZ = 0f;
    private float _velocityX = 0f;
    private bool _forwardPressed, _backwardPressed, _rightPressed, _leftPressed, _runPressed;
    private int _velocityZHash, _velocityXHash;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _rigidbody.centerOfMass = new Vector3(0f,.1f,0f);
        _velocityZHash = Animator.StringToHash("Velocity Z");
        _velocityXHash = Animator.StringToHash("Velocity X");
    }

    private void Update()
    {
        GetInput();
        Accelerate();
        Decelerate();
        Animate();
    }

    private void FixedUpdate()
    {
        // _rigidbody.velocity = Vector3.forward * _speed * Time.deltaTime;
    }

    private void GetInput()
    {
        _forwardPressed = Input.GetKey(KeyCode.W);
        _backwardPressed = Input.GetKey(KeyCode.S);
        
        _leftPressed = Input.GetKey(KeyCode.A);
        _rightPressed = Input.GetKey(KeyCode.D);

        _runPressed = Input.GetKey(KeyCode.LeftShift);

        _currentExtremum = _runPressed ? _extremumRunVelocity : _extremumWalkVelocity;
    }

    private void Animate()
    {
        _animator.SetFloat(_velocityZHash, _velocityZ);
        _animator.SetFloat(_velocityXHash, _velocityX);
    }

    private void Accelerate()
    {
        AcceleratePositively(_forwardPressed, ref _velocityZ);
        AccelerateNegatively(_backwardPressed, ref _velocityZ);
        AcceleratePositively(_rightPressed, ref _velocityX);
        AccelerateNegatively(_leftPressed, ref _velocityX);
    }
    
    private void AcceleratePositively(bool key, ref float dir)
    {
        if (key && dir < _currentExtremum)
        {
            dir += _acceleration * Time.deltaTime;
        }
    }

    private void AccelerateNegatively(bool key, ref float dir)
    {
        if (key && dir > -_currentExtremum)
        {
            dir -= _acceleration * Time.deltaTime;
        }
    }

    private void Decelerate()
    {
        DeceleratePositively(_forwardPressed, ref _velocityZ);
        DecelerateNegatively(_backwardPressed, ref _velocityZ);
        DeceleratePositively(_leftPressed, ref _velocityX);
        DecelerateNegatively(_rightPressed, ref _velocityX);

        ResetVelocity(_forwardPressed, _backwardPressed, ref _velocityZ);
        ResetVelocity(_leftPressed, _rightPressed, ref _velocityX);

        if (_forwardPressed && _runPressed && _velocityZ > _currentExtremum)
        {
            _velocityZ = _currentExtremum;
        }
        else if (_forwardPressed && _velocityZ > _currentExtremum)
        {
            _velocityZ -= _deceleration * Time.deltaTime;
            if (_velocityZ > _currentExtremum && _velocityZ < (_currentExtremum + .05f))
            {
                _velocityZ = _currentExtremum;
            }
        }
        else if (_forwardPressed && _velocityZ < _currentExtremum && _velocityZ > (_currentExtremum - .05f))
        {
            _velocityZ = _currentExtremum;
        }
    }

    private void DeceleratePositively(bool key, ref float dir)
    {
        if (!key && dir > 0f)
        {
            dir -= _deceleration * Time.deltaTime;
        }
    }

    private void DecelerateNegatively(bool key, ref float dir)
    {
        if (!key && dir < 0f)
        {
            dir += _deceleration * Time.deltaTime;
        }
    }

    private void ResetVelocity(bool key1, bool key2, ref float velocity)
    {
        if (!key1 && !key2 && velocity != 0f && (velocity > -_currentExtremum && velocity < _currentExtremum))
        {
            velocity = 0f;
        }
    }
}
