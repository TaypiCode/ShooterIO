using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEvents : MonoBehaviour
{
    [SerializeField] private Destroyable _destroyable;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerShoot _playerShoot;
    private UnityEvent OnDeadEvent = new UnityEvent();
    private UnityEvent OnRespawnEvent = new UnityEvent();
    private void Start()
    {
        OnDeadEvent.AddListener(_playerMovement.OnDead);
        OnDeadEvent.AddListener(_playerShoot.OnDead);
        OnDeadEvent.AddListener(OnDead);
        _destroyable.SetDeadAction(OnDeadEvent);
        OnRespawnEvent.AddListener(OnRespawn);
        OnRespawnEvent.AddListener(_destroyable.OnRespawn);
        OnRespawnEvent.AddListener(_playerMovement.OnRespawn);
        OnRespawnEvent.AddListener(_playerShoot.OnRespawn);
    }
    private void OnRespawn()
    {
        
    }
    private void OnDead()
    {
        OnRespawnEvent?.Invoke();
    }
}
