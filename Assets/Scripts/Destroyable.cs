using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Destroyable : MonoBehaviour
{
    [SerializeField] private float _startHP;
    [SerializeField] private float _invulnerableTime = 3;
    private float _hp;
    private Transform _transform;
    private UnityEvent OnDeadEvent;
    public FloatUnityEvent OnGetDamageEvent;
    public FloatUnityEvent OnChangeHPEvent;
    private Timer _invulnerableTimer;
    public float HP { get => _hp;
        set
        {
            bool needInvokeEvent = true;
            if (value < _hp)//damage
            {
                if (_invulnerableTimer.GetTime() > 0)
                {
                    value = _hp;
                    needInvokeEvent = false;
                }
                else
                {
                    OnGetDamageEvent?.Invoke(_hp - value);
                }
            }
            _hp = value;
            if (_hp <= 0)
            {
                OnDeadEvent?.Invoke();
            }
            if (needInvokeEvent)
            {
                OnChangeHPEvent?.Invoke(_hp);
            }
        }
    }

    public Transform GetTransform { get => _transform; }
    public float StartHP { get => _startHP; }

    private void Start()
    {
        _invulnerableTimer = gameObject.AddComponent<Timer>();
        OnRespawn();
        _transform = transform;
    }
    public void GetDamage(float damage, BattleObjectStat from)
    {
        if (_hp - damage <= 0)
        {
            from.KillCount++;
        }
        HP -= damage;
    }
    public void SetDeadAction(UnityEvent action)
    {
        OnDeadEvent = action;
    }
    public void OnRespawn()
    {
        _invulnerableTimer.SetTimer(_invulnerableTime);
        HP = _startHP;
    }
}
[Serializable]
public class FloatUnityEvent : UnityEvent<float>
{
}