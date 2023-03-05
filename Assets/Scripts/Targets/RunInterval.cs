using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunInterval : MonoBehaviour
{
    private Renderer _renderer;
    private ArrayList _materialsAvailable = new ArrayList();
    private ArrayList _materialsAvailableString = new ArrayList();
    private Material[] _materialRenderer;
    private string _nameGOFull;
    private string _nameGONote;
    private string _nameGOOctave;
    private string _nameGOAccidental;
    [HideInInspector] public bool TapState;
    [HideInInspector] public PlayNoteOnTone PlayNoteOnTone;
    private string currentMaterialName;
    private string constructedNote;
    // private ParticleSystem noteParticles;

    void Start()
    {
        GetComponents();
        SetNames();
        SetRenderer();

        StartCoroutine(IntervalChangeTarget());
        StartCoroutine(IntervalNoteAndEffect());

        void GetComponents()
        {
            // noteParticles = transform.GetChild(0).GetComponent<ParticleSystem>();
            PlayNoteOnTone = transform.parent.gameObject.GetComponent<PlayNoteOnTone>();
        }

        void SetRenderer()
        {
            _renderer = GetComponent<Renderer>();
            _materialRenderer = _renderer.materials;
        }
    }

    // Caution: Same as in OperationController, lean it down!
    private void SetNames()
    {
        _nameGOFull = transform.name;
        _nameGONote = _nameGOFull[0].ToString();
        _nameGOOctave = _nameGOFull[1].ToString();
        int digitCountBeforeAccidental = 2; // e.g. A2 = 2, A2# = 3
        string accidentalCheck = _nameGOFull.Length > digitCountBeforeAccidental ? _nameGOFull[2].ToString() : "null";
        _nameGOAccidental = accidentalCheck; // Accidental or null 
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

    public ArrayList MaterialsAvailable { get => _materialsAvailable; set => _materialsAvailable = new ArrayList(value); }
    public ArrayList MaterialsAvailableString { get => _materialsAvailableString; set => _materialsAvailableString = new ArrayList(value); }

    private Material ChooseNextMaterial()
    {
        // Copy the original array
        ArrayList fluentMaterialArray = new ArrayList(MaterialsAvailable);

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
            correctMaterial = MaterialsAvailable[MaterialsAvailableString.IndexOf(_nameGONote)] as Material;
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
            fluentMaterialArray.Remove(randomMaterial3);
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
        TapState = currentTargetName == _nameGONote ? true : false;

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
        PlayNoteOnTone.PlayNote(currentMaterialName, _nameGOOctave, _nameGOAccidental, transform.gameObject);
        // noteParticles.Play();
        yield return new WaitForSeconds(1);
        StartCoroutine(IntervalNoteAndEffect());
    }
}
