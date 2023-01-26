using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleObjectStat : MonoBehaviour
{
    private string _objName;
    private int _killCount = 0;

    public int KillCount { get => _killCount; set => _killCount = value; }
    public string ObjName { get => _objName; set => _objName = value; }
}
