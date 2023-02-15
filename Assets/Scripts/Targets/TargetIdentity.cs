using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Put this component on your enemy prefabs / objects
public class TargetIdentity : MonoBehaviour
{
    private static int targetCount;
    private int _indexInSequence;
    private bool _tapState;
    private static OperationController operationController;
    [HideInInspector] public TargetSpawnPoints targetSpawnPoints;

    // every instance registers to and removes itself from here
    private static readonly HashSet<TargetIdentity> _instances = new HashSet<TargetIdentity>();

    // Readonly property, I would return a new HashSet so nobody on the outside can alter the content
    public static HashSet<TargetIdentity> Instances => new HashSet<TargetIdentity>(_instances);

    // Add target to instances
    private void Awake()
    {
        _instances.Add(this);
        operationController = GameObject.Find("List").GetComponent<OperationController>();
        targetSpawnPoints = GameObject.Find("SpawnPoints").GetComponent<TargetSpawnPoints>();
    }

    private void Start()
    {
        gameObject.transform.position = targetSpawnPoints.ReturnRandomSpawnTransform(_indexInSequence).position;
    }
    private void Update()
    {
        _tapState = GetComponent<RunInterval>().TapState;
    }

    public void setIndexInSequence(int index)
    {
        _indexInSequence = index;
    }

    public int getIndexInSequence()
    {
        return _indexInSequence;
    }

    public void ResetPositionToSpawnPoint()
    {
        // transform.position = targetSpawnPoints.GetActiveSpawnPointFromSpawnContainer(_indexInSequence).position;
        gameObject.transform.position = targetSpawnPoints.ReturnRandomSpawnTransform(_indexInSequence).position;
    }

    // Remove target from instances when destroyed
    private void OnDestroy()
    {
        if (_tapState)
        {
            operationController.ActivateFrameSuccess(_indexInSequence, transform.name);
            operationController.DelayDistanceFrameCoroutine();
        }
        _instances.Remove(this);
    }
}