using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveSpeedControl = 5f;
    [SerializeField] float _moveSmoothTime = 0.2f;
    [SerializeField] float _rotationSpeed = 5f;

    [SerializeField] Vector3 _movementArea;
    public Vector3 MovementArea { get { return _movementArea; } }

    Vector2 _rawInput;
    Vector2 _currentInput;
    Vector2 _smoothInputVelocity; //Not gonna use

    Rigidbody _rb;
    Animator _animator;
    Health _health;
    Weapon _weapon;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
        _weapon = GetComponent<Weapon>();
    }

    void Update()
    {
        if (_health.IsDead) { return; }
        FaceToMouse();
    }

    void FixedUpdate()
    {
        if (_health.IsDead || _weapon.IsShooting)
        {
            _rb.velocity = Vector3.zero;
            return;
        }
        MovePlayer();
    }

    void OnMove(InputValue value)
    {
        _rawInput = value.Get<Vector2>();
    }

    void MovePlayer()
    {
        ConstrainMovement();

        //Making raw input smooth
        _currentInput = Vector2.SmoothDamp(_currentInput, _rawInput, ref _smoothInputVelocity, _moveSmoothTime);

        //Making movement in a way to move it's forward
        Vector3 moveSpeed = transform.forward * _moveSpeedControl * _currentInput.y;

        //Making player move
        _rb.velocity = moveSpeed;

        PlayMovementAnimation();
    }

    void ConstrainMovement()
    {
        //Constraining player to make it can not escape specified area
        if (transform.position.x > _movementArea.x)
        {
            transform.position = new Vector3(_movementArea.x, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -_movementArea.x)
        {
            transform.position = new Vector3(-_movementArea.x, transform.position.y, transform.position.z);
        }
        if (transform.position.z > _movementArea.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _movementArea.z);
        }
    }

    void FaceToMouse()
    {
        //taking look direction to rotate player but not on y
        Vector3 lookDirection = MousePosition3D.Transform.position - transform.position;
        lookDirection.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);

        //smoothing the rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    void PlayMovementAnimation()
    {
        if (_rawInput.y > 0)
        {
            _animator.SetTrigger("RunForward");
            _animator.SetBool("Idle", false);
        }
        else if (_rawInput.y < 0)
        {
            _animator.SetTrigger("RunBackward");
            _animator.SetBool("Idle", false);
        }
        else
        {
            _animator.SetBool("Idle", true);
        }
    }
}
