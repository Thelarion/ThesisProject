using System.Collections;
using UnityEngine;

// Details: RunInterval
// Note chages materials after a specific interval
// Gathering of all possible materials, selection of palette and application of next material

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

    // Start
    void Start()
    {
        // Set up components, names and first render
        GetComponents();
        SetNames();
        SetRenderer();

        StartCoroutine(IntervalChangeTarget());
        StartCoroutine(IntervalNoteAndEffect());

        void GetComponents()
        {
            PlayNoteOnTone = transform.parent.gameObject.GetComponent<PlayNoteOnTone>();
        }

        void SetRenderer()
        {
            _renderer = GetComponent<Renderer>();
            _materialRenderer = _renderer.materials;
        }
    }

    // Dissmantle the name of the gameObject as the name contains all relevant information
    private void SetNames()
    {
        // Full name
        _nameGOFull = transform.name;
        // Note name
        _nameGONote = _nameGOFull[0].ToString();
        // Octave name
        _nameGOOctave = _nameGOFull[1].ToString();
        // Make sure to account for the note name plus octave = 2 digits
        int digitCountBeforeAccidental = 2; // e.g. A2 = 2, A2# = 3
        // Check for accidentals
        string accidentalCheck = _nameGOFull.Length > digitCountBeforeAccidental ? _nameGOFull[2].ToString() : "null";
        // Set to accidental or null 
        _nameGOAccidental = accidentalCheck;
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

        // Change every 3 seconds
        yield return new WaitForSeconds(3);
        StartCoroutine(IntervalChangeTarget());
    }

    IEnumerator IntervalNoteAndEffect()
    {
        // Pass the information on to the Wwise Event
        PlayNoteOnTone.PlayNote(currentMaterialName, _nameGOOctave, _nameGOAccidental, transform.gameObject);

        // Change every 3 seconds
        yield return new WaitForSeconds(3);
        StartCoroutine(IntervalNoteAndEffect());
    }
}
