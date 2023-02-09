using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameTargets : MonoBehaviour
{
    private OperationController _operationController;
    public int _targetCount;
    private enum notes
    {
        C,
        D,
        E,
        F,
        G,
        A,
        B,
    }

    private void Update()
    {
        CheckTargetCount();
    }

    private void CheckTargetCount()
    {
        if (CountChildrenTargets(transform) <= 0)
        {
            print("No targets left");
            GameObject.Find("List").GetComponent<OperationController>().DecreaseAlphaIfZeroTargets();
        }
    }

    public int CountChildrenTargets(Transform t)
    {
        int k = 0;
        foreach (Transform c in t)
        {
            if (c.gameObject.activeSelf)
                k++;
        }
        return k;
    }

    void Awake()
    {
        _operationController = GameObject.Find("List").GetComponent<OperationController>();
        int x = 0;
        foreach (notes note in _operationController._melodySequence)
        {
            Transform _childTarget = transform.GetChild(x);
            _childTarget.name = note.ToString();
            _childTarget.GetComponent<TargetController>().setIndexInSequence(x);
            x++;
        }
    }
}
