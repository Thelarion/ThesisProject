using UnityEngine;

// Details: SaveLastTargetSpawn
// Remember the last spawn point to avoid it next time

public class SaveLastTargetSpawn : MonoBehaviour
{
    [SerializeField] private int _checkLastTargetSpawnPoint;

    // Save the current spawn point
    public int GetCurrentTargetSpawnPoint()
    {
        return _checkLastTargetSpawnPoint;
    }

    // Get a new spawn point
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
