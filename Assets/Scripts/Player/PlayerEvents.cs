using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEvents : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cinemachineCamera;
    [SerializeField] private Destroyable _destroyable;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private PlayerShoot _playerShoot;
    private UnityEvent OnDeadEvent = new UnityEvent();
    private UnityEvent OnRespawnEvent = new UnityEvent();
    private CinemachinePOV _cameraPOV;
    private float _cameraXSpeed;
    private float _cameraYSpeed;
    private void Start()
    {
        _cameraPOV = _cinemachineCamera.GetCinemachineComponent<CinemachinePOV>();
        if (_cameraPOV != null)
        {
            _cameraXSpeed = _cameraPOV.m_HorizontalAxis.m_MaxSpeed;
            _cameraYSpeed = _cameraPOV.m_VerticalAxis.m_MaxSpeed;
        }
        OnDeadEvent.AddListener(_playerData.OnDead);
        OnDeadEvent.AddListener(OnDead);
        _destroyable.SetDeadAction(OnDeadEvent);
        OnRespawnEvent.AddListener(OnRespawn);
        OnRespawnEvent.AddListener(_destroyable.OnRespawn);
        OnRespawnEvent.AddListener(_playerData.OnRespawn);
        OnRespawnEvent.AddListener(_playerShoot.ReSetWeapon);
    }
    private void OnRespawn()
    {
        UnLockCamera();
    }
    private void OnDead()
    {
        LockCamera();
        OnRespawnEvent?.Invoke();
    }
    public void LockCamera()
    {
        if (_cameraPOV != null)
        {
            //_cameraPOV.m_HorizontalAxis.m_MaxSpeed = 0;
            //_cameraPOV.m_VerticalAxis.m_MaxSpeed = 0;
        }
    }
    public void UnLockCamera()
    {
        if (_cameraPOV != null)
        {
            //_cameraPOV.m_HorizontalAxis.m_MaxSpeed = _cameraXSpeed;
            //_cameraPOV.m_VerticalAxis.m_MaxSpeed = _cameraYSpeed;
        }
    }
}
