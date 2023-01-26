using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{

    [SerializeField] int targetSpawnAmount = 5;
    private int targetCount;
    public GameObject[] targets;
    public Transform parent;
    private int RandomOption;

    // Start is called before the first frame update
    void Awake()
    {
        TargetSpawn();
    }

    public void TargetSpawn()
    {
        while (targetCount < targetSpawnAmount)
        {
            RandomOption = Random.Range(0, targets.Length);
            GameObject target = targets[RandomOption];
            Vector3 spawnPosition = new Vector3(Random.Range(-9, 9), Random.Range(2, 4), Random.Range(-9, 51));
            Instantiate(target, spawnPosition, target.transform.rotation, parent);
            targetCount += 1;
        }
    }
}
