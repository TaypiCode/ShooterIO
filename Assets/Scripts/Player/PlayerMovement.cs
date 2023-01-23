using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private Transform _povCameraTransform;
    [Header("Movement")]
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _jumpForce = 13.0f;
    [SerializeField] private float _antiBump = 4.5f;
    [SerializeField] private float _gravity = 30.0f;
    private Transform _transform;
    private Vector3 _moveDirection;
    private bool _haveUIForCloseWhenMove = false;
    private bool _isAlive = true;

    private void Start()
    {
        _transform = transform;
        OnRespawn();
    }
    private void Update()
    {
        if (_isAlive)
        {
            DefaultMovement();
        }
    }
    private void FixedUpdate()
    {
        if (_isAlive)
        {
            _controller.Move(_moveDirection * Time.deltaTime);
            _transform.rotation = Quaternion.Euler(0, _povCameraTransform.eulerAngles.y, 0);
        }
    }


    private void DefaultMovement()
    {
        if (_controller.isGrounded)
        {

            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (input.x != 0 || input.y != 0)
            {
                if (_haveUIForCloseWhenMove)
                {
                    CloseOpenedUI();
                    _haveUIForCloseWhenMove = false;
                }
            }

            if (input.x != 0 && input.y != 0)
            {
                input *= 0.777f;
            }

            _moveDirection.x = input.x * _speed;
            _moveDirection.z = input.y * _speed;
            _moveDirection.y = -_antiBump;

            _moveDirection = _transform.TransformDirection(_moveDirection);

            if (Input.GetKey(KeyCode.Space))
            {
                if (_haveUIForCloseWhenMove)
                {
                    CloseOpenedUI();
                    _haveUIForCloseWhenMove = false;
                }
                Jump();
            }
        }
        else
        {
            _moveDirection.y -= _gravity * Time.deltaTime;
        }
    }

    private void Jump()
    {
        _moveDirection.y += _jumpForce;
    }
    private void CloseOpenedUI()
    {
        // action
    }
    public void OnRespawn()
    {
        _controller.Move(_spawnManager.GetRandomPosition());
        _isAlive = true;
    }
    public void OnDead()
    {
        _isAlive = false;
    }
}
