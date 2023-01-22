using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private WeaponScriptable _weaponData;
    private void Start()
    {
        _weapon.SetData(_weaponData);
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _weapon.TryShoot();
        }
    }
}
