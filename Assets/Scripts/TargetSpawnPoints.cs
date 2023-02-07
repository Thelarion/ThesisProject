using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawnPoints : MonoBehaviour
{
    public GameObject TargetContainer1;
    public GameObject TargetContainer2;
    public GameObject TargetContainer3;
    public GameObject TargetContainer4;
    public GameObject TargetContainer5;

    private static Transform[] _spawnPointsT1;
    private static Transform[] _spawnPointsT2;
    private static Transform[] _spawnPointsT3;
    private static Transform[] _spawnPointsT4;
    private static Transform[] _spawnPointsT5;

    static Transform[] currentSpawnContainer = null;
    // static int randomContainerTarget;

    void Awake()
    {
        _spawnPointsT1 = TargetContainer1.GetComponentsInChildren<Transform>();
        _spawnPointsT2 = TargetContainer2.GetComponentsInChildren<Transform>();
        _spawnPointsT3 = TargetContainer3.GetComponentsInChildren<Transform>();
        //    _spawnPointsT4 = Tone4.GetComponentsInChildren<Transform>();
        //    _spawnPointsT5 = Tone5.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    public Transform ReturnRandomSpawnTransform(int sequenceIndex)
    {
        // randomContainerTarget = randomContainerTarget + UnityEngine.Random.Range(1, 3);
        // if (randomContainerTarget > 3)
        // {
        //     randomContainerTarget -= 3;
        // }

        int newSpawnIndex = transform.GetChild(sequenceIndex).GetComponent<SaveLastTargetSpawn>().GetNewSpawnIndex();

        // print(randomContainerTarget);
        // First item is parent itself
        currentSpawnContainer = GetCurrentSpawnContainer(sequenceIndex, currentSpawnContainer);
        return currentSpawnContainer[newSpawnIndex].transform;
    }

    private Transform[] GetCurrentSpawnContainer(int sequenceIndex, Transform[] currentSpawnContainer)
    {
        switch (sequenceIndex)
        {
            case 0:
                currentSpawnContainer = _spawnPointsT1;
                break;
            case 1:
                currentSpawnContainer = _spawnPointsT2;
                break;
            case 2:
                currentSpawnContainer = _spawnPointsT3;
                break;
            case 3:
                currentSpawnContainer = _spawnPointsT4;
                break;
            case 4:
                currentSpawnContainer = _spawnPointsT5;
                break;
        }

        return currentSpawnContainer;
    }
}
