using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RunInterval : MonoBehaviour
{
    private Renderer _renderer;
    private ArrayList _materialsAvailable = new ArrayList();
    private Material[] _materialRenderer;
    private int _materialIndex;
    private string _nameGameObject;
    [HideInInspector] public bool TapState;


    void Awake()
    {
        LoadMaterialsFromResources();
        _nameGameObject = transform.name;
        _renderer = GetComponent<Renderer>();
        _materialRenderer = _renderer.materials;
        StartCoroutine(IntervalChangeTarget());
    }

    private void LoadMaterialsFromResources()
    {
        var load = Resources.LoadAll("MaterialBalloon", typeof(Material)).Cast<Material>();
        foreach (var material in load)
        {
            _materialsAvailable.Add(material);
        }
    }

    void ChangeMaterial()
    {
        _materialIndex = Random.Range(0, _materialsAvailable.Count);
        _materialRenderer[0] = _materialsAvailable[_materialIndex] as Material;
        _renderer.materials = _materialRenderer;
    }

    private void CheckTargetState()
    {
        string[] splitArray = _renderer.material.name.Split(char.Parse(" "));
        string currentTargetName = splitArray[0];
        TapState = currentTargetName == _nameGameObject ? true : false;
    }

    IEnumerator IntervalChangeTarget()
    {
        ChangeMaterial();
        CheckTargetState();
        yield return new WaitForSeconds(3);
        StartCoroutine(IntervalChangeTarget());
    }
}
