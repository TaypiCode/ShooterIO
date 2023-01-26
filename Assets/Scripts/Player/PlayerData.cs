using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private BattleObjectStat _stat;
    [SerializeField] private Destroyable _destroyable;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerUI _playerUI;
    private bool _isAlive;

    public bool IsAlive { get => _isAlive; }
    public BattleObjectStat Stat { get => _stat;  }

    private void Start()
    {
        OnRespawn();
        _stat.ObjName = "Игрок";
        _playerUI.SetMaxHP(_destroyable.StartHP);
        _playerUI.SetHP(_destroyable.HP);
    }
    public void OnRespawn()
    {
        _playerMovement.SetRandomSpawnPosition();
        _isAlive = true;
    }
    public void OnDead()
    {
        _isAlive = false;
    }
}
