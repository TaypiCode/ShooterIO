using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon", order = 1)]
public class WeaponScriptable: ScriptableObject
{
    [ScriptableObjectId] public string itemId;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _name;
    [SerializeField] private int _itemLevel;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _timeBeetwenShoot;
    [SerializeField] private float _damage;
    [SerializeField] private int _ammoInStock;
 
    public float ReloadTime { get => _reloadTime; }
    public float TimeBeetwenShoot { get => _timeBeetwenShoot; }
    public float Damage { get => _damage; }
    public int AmmoInStock { get => _ammoInStock; }

    public Sprite GetSprite()
    {
        return _sprite;
    }
    public string GetName()
    {
        return _name;
    }
    public int GetItemLevel()
    {
        return _itemLevel;
    }
}