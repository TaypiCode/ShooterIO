using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Destroyable : MonoBehaviour
{
    [SerializeField] private float _startHP;
    private float _hp;
    private Transform _transform;
    private UnityEvent OnDeadEvent;
    public FloatUnityEvent OnChangeHPEvent;
    public float HP { get => _hp;
        set
        {
            if (value < _hp)//damage
            {
                OnChangeHPEvent?.Invoke(_hp - value);
            }
            _hp = value;
            if (_hp <= 0)
            {
                OnDeadEvent?.Invoke();
            }
        }
    }

    public Transform GetTransform { get => _transform; }
    

    private void Start()
    {
        OnRespawn();
        _transform = transform;
    }
    public void GetDamage(float damage)
    {
        HP -= damage;
    }
    public void SetDeadAction(UnityEvent action)
    {
        OnDeadEvent = action;
    }
    public void OnRespawn()
    {
        HP = _startHP;
    }
}
[Serializable]
public class FloatUnityEvent : UnityEvent<float>
{
}