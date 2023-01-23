using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class AI : MonoBehaviour
{
    [SerializeField] private float _targetRange;
    [SerializeField] private Destroyable _destroyable;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private WeaponScriptable _weaponData;
    [SerializeField] private Transform _rayForVision;
    [SerializeField] private Transform _bodyTranform;
    private bool _isAlive = true;
    private Transform _transform;
    private AIManager _aiManager;
    private Destroyable _target = null;
    private UnityEvent OnDeadEvent = new UnityEvent();
    private UnityEvent OnRespawnEvent = new UnityEvent();
    private void Start()
    {
        _transform = transform;
        _aiManager = FindObjectOfType<AIManager>();
        _aiManager.AddDestroyable(_destroyable);
        _weapon.SetData(_weaponData);
        OnDeadEvent.AddListener(OnDead);
        _destroyable.SetDeadAction(OnDeadEvent);
        OnRespawnEvent.AddListener(_destroyable.OnRespawn);
        OnRespawnEvent.AddListener(OnRespawn);
    }
    private void Update()
    {
        if (_isAlive)
        {
            if (_target != null)
            {
                if (_target.HP > 0)
                {
                    if (Vector3.Distance(_bodyTranform.position, _target.GetTransform.position) <= _targetRange)
                    {
                        FindTarget();
                    }
                    LookAtTarget();
                    if (AimedOnTarget())
                    {
                        _agent.isStopped=true;
                        _weapon.TryShoot();
                    }
                    else
                    {
                        MoveToTarget();
                    }
                }
                else
                {
                    FindTarget();
                }
            }
            else
            {
                FindTarget();
            }
        }
    }
    private void OnRespawn()
    {
        _isAlive = true;
        _aiManager.Respawn(_transform);
    }
    private void OnDead()
    {
        _isAlive = false;
        OnRespawnEvent?.Invoke();
    }
    private bool AimedOnTarget()
    {
        if(_target == null)
        {
            return false;
        }
        Ray ray = new Ray(_rayForVision.position, _rayForVision.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Destroyable destroyable;
            if (destroyable = hit.collider.GetComponent<Destroyable>())
            {
                return true;
            }
        }
        return false;
    }
    private void MoveToTarget()
    {
        _agent.isStopped = false;
        _agent.SetDestination(_target.GetTransform.position);
    }
    private void LookAtTarget()
    {
        Vector3 targetPoint = new Vector3(_target.GetTransform.position.x, _bodyTranform.position.y, _target.GetTransform.position.z) - _bodyTranform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetPoint, Vector3.up);
        _bodyTranform.rotation = Quaternion.Slerp(_bodyTranform.rotation, targetRotation, Time.deltaTime * 5.0f);
        _rayForVision.LookAt(_target.GetTransform);
    }
    private void FindTarget()
    {
        _target = _aiManager.GetNearEnemy(_destroyable);
    }
}
