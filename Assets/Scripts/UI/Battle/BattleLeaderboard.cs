﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleLeaderboard : MonoBehaviour
{
    [SerializeField] private GameObject _leaderboardCanvas;
    [SerializeField] private Transform _contentTransform;
    [SerializeField] private GameObject _leaderboardObjPrefub;
    private List<GameObject> _objects = new List<GameObject>();
    private List<BattleObjectStat> _stats;
    public void ShowLeaderboard()
    {
        for(int i = 0; i < _objects.Count; i++)
        {
            Destroy(_objects[i]);
        }
        _objects.Clear();
        if(_stats == null)
        {
            _stats = FindObjectsOfType<BattleObjectStat>().ToList();
        }
        _stats = _stats.OrderByDescending(x => x.KillCount).ToList();
        for(int i = 0; i < _stats.Count; i++)
        {
            GameObject obj = Instantiate(_leaderboardObjPrefub, _contentTransform);
            _objects.Add(obj);
            obj.GetComponent<BattleLeaderboardObject>().SetData(i + 1, _stats[i].ObjName, _stats[i].KillCount);
        }
        _leaderboardCanvas.SetActive(true);
    }
    public void CloseLeaderboard()
    {
        _leaderboardCanvas.SetActive(false);
    }
}
