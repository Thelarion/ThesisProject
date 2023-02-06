using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Put this component on your enemy prefabs / objects
public class TargetController : MonoBehaviour
{
    private int _indexInSequence;
    private bool _tapState;
    private static OperationController operationController;

    // every instance registers to and removes itself from here
    private static readonly HashSet<TargetController> _instances = new HashSet<TargetController>();

    // Readonly property, I would return a new HashSet so nobody on the outside can alter the content
    public static HashSet<TargetController> Instances => new HashSet<TargetController>(_instances);

    // Add target to instances
    private void Awake()
    {
        _instances.Add(this);
        operationController = GameObject.Find("List").GetComponent<OperationController>();
    }
    private void Update()
    {
        _tapState = GetComponent<RunInterval>().TapState;

    }

    public void setIndexInSequence(int index)
    {
        _indexInSequence = index;
    }

    // Remove target from instances when destroyed
    private void OnDestroy()
    {
        // GetComponent<AkAmbient>().Stop(0);
        if (_tapState)
        {
            operationController.ActivateListFrame(_indexInSequence, transform.name);
        }
        _instances.Remove(this);
    }
}