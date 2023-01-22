using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    private float _hp;

    public float HP { get => _hp; set => _hp = value; }
}
