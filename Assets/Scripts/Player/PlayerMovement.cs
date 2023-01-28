using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
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

    private void Start()
    {
        _transform = transform;
    }
    private void Update()
    {
        if (GameData.GameEnded == false)
        {
            if (_playerData.IsAlive)
            {
                DefaultMovement();
            }
        }
    }
    private void FixedUpdate()
    {
        if (GameData.GameEnded == false)
        {
            if (_playerData.IsAlive)
            {
                _controller.Move(_moveDirection * Time.deltaTime);
                _transform.rotation = Quaternion.Euler(0, _povCameraTransform.eulerAngles.y, 0);
            }
        }
    }
    public void SetRandomSpawnPosition()
    {
        _controller.enabled = false;
        _transform.position= _spawnManager.GetRandomPosition();
        _controller.enabled = true;
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
}
