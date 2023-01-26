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
    private UnityEvent OnDeadEvent = new UnityEvent();
    private UnityEvent OnRespawnEvent = new UnityEvent();
    private CinemachinePOV _cameraPOV;
    private float _cameraXSpeed;
    private float _cameraYSpeed;
    private void Start()
    {
        _cameraPOV = _cinemachineCamera.GetCinemachineComponent<CinemachinePOV>();
        OnDeadEvent.AddListener(_playerData.OnDead);
        OnDeadEvent.AddListener(OnDead);
        _destroyable.SetDeadAction(OnDeadEvent);
        OnRespawnEvent.AddListener(OnRespawn);
        OnRespawnEvent.AddListener(_destroyable.OnRespawn);
        OnRespawnEvent.AddListener(_playerData.OnRespawn);
    }
    private void OnRespawn()
    {
        
    }
    private void OnDead()
    {
        OnRespawnEvent?.Invoke();
    }
    public void LockCamera()
    {
        if (_cameraPOV != null)
        {
            _cameraXSpeed = _cameraPOV.m_HorizontalAxis.m_MaxSpeed;
            _cameraPOV.m_HorizontalAxis.m_MaxSpeed = 0;
            _cameraYSpeed = _cameraPOV.m_VerticalAxis.m_MaxSpeed;
            _cameraPOV.m_VerticalAxis.m_MaxSpeed = 0;
        }
    }
    public void UnLockCamera()
    {
        if (_cameraPOV != null)
        {
            _cameraPOV.m_HorizontalAxis.m_MaxSpeed = _cameraXSpeed;
            _cameraPOV.m_VerticalAxis.m_MaxSpeed = _cameraYSpeed;
        }
    }
}
