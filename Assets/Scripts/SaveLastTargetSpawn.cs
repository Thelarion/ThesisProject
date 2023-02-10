using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLastTargetSpawn : MonoBehaviour
{
    [SerializeField] private int _checkLastTargetSpawnPoint;

    public int GetCurrentTargetSpawnPoint()
    {
        return _checkLastTargetSpawnPoint;
    }

    public int GetNewSpawnIndex()
    {
        _checkLastTargetSpawnPoint = _checkLastTargetSpawnPoint + UnityEngine.Random.Range(1, 3);
        if (_checkLastTargetSpawnPoint > 3)
        {
            _checkLastTargetSpawnPoint -= 3;
        }

        return _checkLastTargetSpawnPoint;
    }
}
