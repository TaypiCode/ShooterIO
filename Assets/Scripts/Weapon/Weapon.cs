using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _rayStartTransform;
    [SerializeField] private Transform _bulletStartTransform;
    [SerializeField] private Animator _animator;
    private WeaponScriptable _weaponData;
    private int _ammo;
    private Timer _reloadTimer;
    private Timer _timerBetweenShots;
    public FloatUnityEvent OnChangeAmmoEvent;
    private BattleObjectStat _stat;

    public int Ammo { get => _ammo;
        private set 
        {
            _ammo = value;
            OnChangeAmmoEvent?.Invoke(_ammo);
        }
    }

    private void Start()
    {
        _reloadTimer = this.gameObject.AddComponent<Timer>();
        _timerBetweenShots = this.gameObject.AddComponent<Timer>(); 
    }
    private void Update()
    {
        if(_ammo <= 0 && _reloadTimer.GetTime() < 0)
        {
            Ammo = _weaponData.AmmoInStock; // test
        }
    }
    public void SetData(WeaponScriptable data, BattleObjectStat stat)
    {
        _weaponData = data;
        Ammo = _weaponData.AmmoInStock;
        _stat = stat;
    }
    public void TryShoot()
    {
        if (CanShoot())
        {
            Shoot();
            CheckCollision();
        }
    }
    private bool CanShoot()
    {
        if (_weaponData == null)
        {
            return false;
        }
        if (_reloadTimer.GetTime() < 0 && _timerBetweenShots.GetTime() < 0 && _ammo > 0)
        {
            return true;
        }
        return false;
    }
    private void Shoot()
    {
        _animator?.SetTrigger("Shoot");
        Ammo -= 1;
        _timerBetweenShots.SetTimer(_weaponData.TimeBeetwenShoot);
        if (_ammo <= 0)
        {
            ReloadWeapon();
        }
    }
    public void ReloadWeapon()
    {
        _animator?.SetTrigger("Reload");
        Ammo = 0;
        _reloadTimer.SetTimer(_weaponData.ReloadTime);
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
                destroyable.GetDamage(_weaponData.Damage, _stat);
            }
        }
    }
}
