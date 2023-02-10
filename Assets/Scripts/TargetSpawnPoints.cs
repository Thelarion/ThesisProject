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

    static Transform[] currentSpawnContainerItems = null;
    static GameObject currentSpawnContainer;
    // static int randomContainerTarget;

    void Awake()
    {
        _spawnPointsT1 = TargetContainer1.GetComponentsInChildren<Transform>();
        _spawnPointsT2 = TargetContainer2.GetComponentsInChildren<Transform>();
        _spawnPointsT3 = TargetContainer3.GetComponentsInChildren<Transform>();
        //    _spawnPointsT4 = Tone4.GetComponentsInChildren<Transform>();
        //    _spawnPointsT5 = Tone5.GetComponentsInChildren<Transform>();
    }

    public Transform ReturnRandomSpawnTransform(int sequenceIndex)
    {

        int newSpawnIndex = transform.GetChild(sequenceIndex).GetComponent<SaveLastTargetSpawn>().GetNewSpawnIndex();

        // First item is parent itself
        currentSpawnContainerItems = GetCurrentSpawnContainerItems(sequenceIndex, currentSpawnContainerItems);
        return currentSpawnContainerItems[newSpawnIndex].transform;
    }

    private Transform[] GetCurrentSpawnContainerItems(int sequenceIndex, Transform[] currentSpawnContainerItems)
    {
        switch (sequenceIndex)
        {
            case 0:
                currentSpawnContainerItems = _spawnPointsT1;
                break;
            case 1:
                currentSpawnContainerItems = _spawnPointsT2;
                break;
            case 2:
                currentSpawnContainerItems = _spawnPointsT3;
                break;
            case 3:
                currentSpawnContainerItems = _spawnPointsT4;
                break;
            case 4:
                currentSpawnContainerItems = _spawnPointsT5;
                break;
        }

        return currentSpawnContainerItems;
    }

    public Transform GetActiveSpawnPointFromSpawnContainer(int sequenceIndex)
    {
        currentSpawnContainer = GetCurrentSpawnContainer(sequenceIndex, currentSpawnContainer);
        currentSpawnContainerItems = GetCurrentSpawnContainerItems(sequenceIndex, currentSpawnContainerItems);

        int spawnPointIndexFromContainer = currentSpawnContainer.GetComponent<SaveLastTargetSpawn>().GetCurrentTargetSpawnPoint();

        return currentSpawnContainerItems[spawnPointIndexFromContainer].transform;
    }

    private GameObject GetCurrentSpawnContainer(int sequenceIndex, GameObject currentSpawnContainer)
    {
        switch (sequenceIndex)
        {
            case 0:
                currentSpawnContainer = TargetContainer1;
                break;
            case 1:
                currentSpawnContainer = TargetContainer2;
                break;
            case 2:
                currentSpawnContainer = TargetContainer3;
                break;
            case 3:
                currentSpawnContainer = TargetContainer4;
                break;
            case 4:
                currentSpawnContainer = TargetContainer5;
                break;
        }

        return currentSpawnContainer;
    }
}
