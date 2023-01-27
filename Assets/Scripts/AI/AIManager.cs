using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AIManager : MonoBehaviour
{
    [SerializeField] private SpawnManager _spawn;
    private List<Destroyable> destroyables = new List<Destroyable>();
    private void Start()
    {
        destroyables.Add(FindObjectOfType<PlayerMovement>().GetComponent<Destroyable>());
    }
    public Destroyable GetNearEnemy(Destroyable from)
    {
        try
        {
            Destroyable[] arr = destroyables.Where(x => (x.HP > 0) && (x != from)).OrderBy(y => Vector3.Distance(y.GetTransform.position, from.GetTransform.position)).ToArray();
            if (arr.Length > 0)
            {
                return arr[0];
            }
            else return null;
        } catch { return null; };
    }
    public void AddDestroyable(Destroyable obj)
    {
        destroyables.Add(obj);
    }
    public void Respawn(Transform value)
    {
        _spawn.ResetPosition(value);
    }
}
