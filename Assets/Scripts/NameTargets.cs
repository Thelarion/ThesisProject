using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameTargets : MonoBehaviour
{
    private OperationController _operationController;
    private enum colors
    {
        Yellow,
        Red,
        Blue,
    }

    void Awake()
    {
        _operationController = GameObject.Find("List").GetComponent<OperationController>();
        int x = 0;
        foreach (colors color in _operationController._melodySequence)
        {
            Transform _childTarget = transform.GetChild(x);
            _childTarget.name = color.ToString();
            _childTarget.GetComponent<TargetController>().setIndexInSequence(x);
            x++;
        }
    }
}
