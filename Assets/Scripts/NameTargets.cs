using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameTargets : MonoBehaviour
{
    private OperationController _operationController;
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
