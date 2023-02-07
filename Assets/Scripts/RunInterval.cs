using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RunInterval : MonoBehaviour
{
    private Renderer _renderer;
    private ArrayList _materialsAvailable = new ArrayList();
    private ArrayList _materialsAvailableString = new ArrayList();
    private Material[] _materialRenderer;
    private int _materialIndex;
    private string _nameGameObject;
    [HideInInspector] public bool TapState;
    [HideInInspector] public PlayNoteOnTone PlayNoteOnTone;
    private string currentMaterialName;
    private ParticleSystem noteParticles;

    void Start()
    {
        noteParticles = transform.GetChild(0).GetComponent<ParticleSystem>();
        PlayNoteOnTone = transform.parent.gameObject.GetComponent<PlayNoteOnTone>();
        LoadMaterialsFromResources();
        _nameGameObject = transform.name;
        _renderer = GetComponent<Renderer>();
        _materialRenderer = _renderer.materials;
        StartCoroutine(IntervalChangeTarget());
        StartCoroutine(IntervalNoteAndEffect());
    }

    private void LoadMaterialsFromResources()
    {
        var load = Resources.LoadAll("MaterialNotes", typeof(Material)).Cast<Material>();
        foreach (var material in load)
        {
            _materialsAvailable.Add(material);
            _materialsAvailableString.Add(material.name);
        }
    }

    void ChangeMaterial()
    {
        _materialRenderer[0] = ChooseNextMaterial();
        _renderer.materials = _materialRenderer;
    }

    private int checkLastMaterial;
    Material correctMaterial = null;
    Material randomMaterial1 = null;
    Material randomMaterial2 = null;
    Material randomMaterial3 = null;
    bool materialGenerated = false;

    private Material ChooseNextMaterial()
    {
        // Copy the original array
        ArrayList fluentMaterialArray = new ArrayList(_materialsAvailable);
        // fluentMaterialArray = _materialsAvailable;

        if (!materialGenerated)
        {
            GenerateMaterials(fluentMaterialArray);
            materialGenerated = true;
        }

        // Establish material array
        Material[] chosenMaterials = new Material[] { correctMaterial, randomMaterial1, randomMaterial2, randomMaterial3 };

        CheckMaterialNotTwice();

        Material finalMaterial = chosenMaterials[checkLastMaterial - 1];

        return finalMaterial;

        void GenerateMaterials(ArrayList fluentMaterialArray)
        {
            // Get correct material
            correctMaterial = _materialsAvailable[_materialsAvailableString.IndexOf(_nameGameObject)] as Material;
            // Remove the correct material from the fluent array
            fluentMaterialArray.Remove(correctMaterial);
            // print(correctMaterial);

            // Get first random Material
            randomMaterial1 = fluentMaterialArray[Random.Range(0, fluentMaterialArray.Count)] as Material;
            // Remove this Material
            fluentMaterialArray.Remove(randomMaterial1);

            // Get second random Material
            randomMaterial2 = fluentMaterialArray[Random.Range(0, fluentMaterialArray.Count)] as Material;
            // Remove this Material
            fluentMaterialArray.Remove(randomMaterial2);

            // Get third random Material
            randomMaterial3 = fluentMaterialArray[Random.Range(0, fluentMaterialArray.Count)] as Material;
            // Remove this Material
            fluentMaterialArray.Remove(randomMaterial2);
        }

        void CheckMaterialNotTwice()
        {
            checkLastMaterial = checkLastMaterial + UnityEngine.Random.Range(1, 3);

            if (checkLastMaterial > 3)
            {
                checkLastMaterial -= 3;
            }
        }
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
        currentMaterialName = _materialRenderer[0].name;
        yield return new WaitForSeconds(3);
        StartCoroutine(IntervalChangeTarget());
    }

    IEnumerator IntervalNoteAndEffect()
    {
        PlayNoteOnTone.PlayNote(currentMaterialName, transform.gameObject);
        noteParticles.Play();
        yield return new WaitForSeconds(1);
        StartCoroutine(IntervalNoteAndEffect());
    }
}
