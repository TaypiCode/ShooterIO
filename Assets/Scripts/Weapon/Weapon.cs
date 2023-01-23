using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _rayStartTransform;
    [SerializeField] private Transform _bulletStartTransform;
    private WeaponScriptable _weaponData;
    private float _reloadTimer;
    private float _timerBetweenShots;
    private int _ammo;
    private void Start()
    {
        _reloadTimer = 0;
        _timerBetweenShots = 0;
    }
    private void FixedUpdate()
    {
        _reloadTimer -=  Time.deltaTime;
        _timerBetweenShots -= Time.deltaTime;
    }
    public void SetData(WeaponScriptable data)
    {
        _weaponData = data;
        _ammo = _weaponData.AmmoInStock;
    }
    public void TryShoot()
    {
        if (CanShoot())
        {
            Shoot();
            CheckCollision();
            if(_ammo <= 0)
            {
                ReloadWeapon();
            }
        }
    }
    private bool CanShoot()
    {
        if (_weaponData == null)
        {
            return false;
        }
        if (_reloadTimer < 0 && _timerBetweenShots < 0 && _ammo > 0)
        {
            return true;
        }
        return false;
    }
    private void Shoot()
    {
        _ammo -= 1;
        _timerBetweenShots = _weaponData.TimeBeetwenShoot;
    }
    private void ReloadWeapon()
    {
        _reloadTimer = _weaponData.ReloadTime;
        _ammo = _weaponData.AmmoInStock; // test
    }
    private void CheckCollision()
    {
        Ray ray = new Ray(_rayStartTransform.position, _rayStartTransform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Destroyable destroyable;
            if(destroyable = hit.collider.GetComponent<Destroyable>())
            {
                destroyable.GetDamage(_weaponData.Damage);
            }
        }
    }
}
