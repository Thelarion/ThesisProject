using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameTargets : MonoBehaviour
{
    private OperationController operationController;
    private enum colors
    {
        Yellow,
        Red,
        Blue,
    }

    void Awake()
    {
        operationController = GameObject.Find("List").GetComponent<OperationController>();
        int x = 0;
        foreach (colors color in operationController._melodySequence)
        {
            transform.GetChild(x).name = color.ToString();
            x++;
        }
    }
}
