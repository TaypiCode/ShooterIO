using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private WeaponScriptable _weaponData;
    private void Start()
    {
        _weapon.SetData(_weaponData, _playerData.Stat);
    }
    private void Update()
    {
        if (GameData.GameEnded == false)
        {
            if (_playerData.IsAlive)
            {
                if (Input.GetMouseButton(0))
                {
                    _weapon.TryShoot();
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    _weapon.ReloadWeapon();
                }
            }
        }
    }
    
}
