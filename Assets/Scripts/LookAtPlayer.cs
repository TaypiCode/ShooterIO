using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform _player;
    private Transform _transform;
    private void Start()
    {
        _transform = transform;
    }
    private void Update()
    {
        if (_player == null)
        {
            _player = FindObjectOfType<PlayerMovement>().transform;
        }
        _transform.LookAt(_player);
    }
}
