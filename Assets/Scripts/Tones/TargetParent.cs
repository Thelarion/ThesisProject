using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetParent : MonoBehaviour
{
    private OperationController _operationController;
    public int _targetCount;
    private enum notes
    {
        C2, D2, E2, F2, G2, A2, B2,
        C3, D3, E3, F3, G3, A3, B3,
        C4, D4, E4, F4, G4, A4, B4,
    }

    private void Update()
    {
        CheckTargetCount();
    }

    public void ToggleInclusionTargets()
    {
        foreach (Transform child in transform)
        {
            bool _currentInclusion = child.GetComponent<TargetMove>().InclusionIO;

            if (!_currentInclusion)
            {
                child.GetComponent<TargetMove>().InclusionIO = true;
            }
            else
            {
                child.GetComponent<TargetMove>().InclusionIO = false;
            }
        }
    }

    public void InitializeMovement()
    {
        foreach (Transform child in transform)
        {

            child.GetComponent<TargetMove>().InitializeMovementAfterMissOrInclusion();
        }
    }

    public void StopMovement()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<TargetMove>().StopMovementWhenMissOrInclusion();
        }
    }

    public void ResetNotePositionToSpawnPoint()
    {
        foreach (Transform child in transform)
        {
            child.transform.GetComponent<TargetController>().ResetPositionToSpawnPoint();
        }
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
