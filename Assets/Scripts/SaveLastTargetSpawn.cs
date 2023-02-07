using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLastTargetSpawn : MonoBehaviour
{
    private int lastTargetSpawnPoint;

    public int GetNewSpawnIndex()
    {
        lastTargetSpawnPoint = lastTargetSpawnPoint + UnityEngine.Random.Range(1, 3);
        if (lastTargetSpawnPoint > 3)
        {
            lastTargetSpawnPoint -= 3;
        }

        return lastTargetSpawnPoint;
    }

}
