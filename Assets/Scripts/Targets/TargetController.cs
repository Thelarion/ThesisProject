using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TargetController : MonoBehaviour
{
    private OperationController _operationController;
    public int _targetCount;
    public string chosenPalette;
    private enum notes
    {
        C2, C2s, D2b, D2, D2s, E2b, E2, F2, F2s, G2b, G2, G2s, A2b, A2, A2s, B2b, B2,
        C3, C3s, D3b, D3, D3s, E3b, E3, F3, F3s, G3b, G3, G3s, A3b, A3, A3s, B3b, B3,
        C4, C4s, D4b, D4, D4s, E4b, E4, F4, F4s, G4b, G4, G4s, A4b, A4, A4s, B4b, B4,
    }
    private ArrayList _materialsAvailable = new ArrayList();
    private ArrayList _materialsAvailableString = new ArrayList();

    void Awake()
    {
        _operationController = GameObject.Find("List").GetComponent<OperationController>();
        int x = 0;
        foreach (notes note in _operationController._melodySequence)
        {
            Transform _childTarget = transform.GetChild(x);
            _childTarget.name = note.ToString();
            _childTarget.GetComponent<TargetIdentity>().setIndexInSequence(x);
            x++;
        }
        InitializeMaterials();
    }

    private void Update()
    {
        CheckTargetCount();
    }

    private void InitializeMaterials()
    {
        var load = Resources.LoadAll("UI_Materials/" + GameManager.ChosenPalette, typeof(Material)).Cast<Material>();
        foreach (var material in load)
        {
            _materialsAvailable.Add(material);
            _materialsAvailableString.Add(material.name);
        }

        foreach (Transform item in transform)
        {
            item.transform.GetComponent<RunInterval>().MaterialsAvailable = _materialsAvailable;
            item.transform.GetComponent<RunInterval>().MaterialsAvailableString = _materialsAvailableString;
        }
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
            child.transform.GetComponent<TargetIdentity>().ResetPositionToSpawnPoint();
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
}
