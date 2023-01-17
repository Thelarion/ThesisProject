using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Put this component on your enemy prefabs / objects
public class TargetController : MonoBehaviour
{
    // every instance registers to and removes itself from here
    private static readonly HashSet<TargetController> _instances = new HashSet<TargetController>();

    // Readonly property, I would return a new HashSet so nobody on the outside can alter the content
    public static HashSet<TargetController> Instances => new HashSet<TargetController>(_instances);

    private void Awake()
    {
        _instances.Add(this);
    }

    private void OnDestroy()
    {
        _instances.Remove(this);
    }
}