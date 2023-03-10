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
        ReSetWeapon();
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
                    Cursor.lockState = CursorLockMode.Locked;
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    _weapon.ReloadWeapon();
                }
            }
        }
    }
    public void ReSetWeapon()
    {
        _weapon.SetData(_weaponData, _playerData.Stat);
    }
}
