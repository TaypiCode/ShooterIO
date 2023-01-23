using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private WeaponScriptable _weaponData;
    private bool _isAlive = true;
    private void Start()
    {
        _weapon.SetData(_weaponData);
    }
    private void Update()
    {
        if (_isAlive)
        {
            if (Input.GetMouseButton(0))
            {
                _weapon.TryShoot();
            }
        }
    }
    public void OnRespawn()
    {
        _isAlive = true;
    }
    public void OnDead()
    {
        _isAlive = false;
    }
}
