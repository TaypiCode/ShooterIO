using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _aiPrefubs;
    [SerializeField] private int _aiCount;
    [SerializeField] private Transform[] _spawns;
    private Vector3 _plusSpawnPos = new Vector3(0, 1, 0);
    private void Start()
    {
        CreateAI();
    }
    private void CreateAI()
    {
        for (int i = 0; i < _aiCount; i++)
        {
            GameObject aiPrefub = _aiPrefubs[Random.Range(0, _aiPrefubs.Length)];
            Instantiate(aiPrefub, GetRandomPosition(), Quaternion.identity);
        }

    }
    public Vector3 GetRandomPosition()
    {
        return _spawns[Random.Range(0, _spawns.Length)].position + _plusSpawnPos;
    }
    public void ResetPosition(Transform obj)
    {
        obj.position = GetRandomPosition();
    }
}
