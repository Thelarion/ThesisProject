using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class OperationController : MonoBehaviour
{
    [SerializeField]
    public enum notes
    {
        C2, C2s, D2b, D2, D2s, E2b, E2, F2, F2s, G2b, G2, G2s, A2b, A2, A2s, B2b, B2,
        C3, C3s, D3b, D3, D3s, E3b, E3, F3, F3s, G3b, G3, G3s, A3b, A3, A3s, B3b, B3,
        C4, C4s, D4b, D4, D4s, E4b, E4, F4, F4s, G4b, G4, G4s, A4b, A4, A4s, B4b, B4,
    }

    [Header("Melody")]
    [SerializeField] public notes[] _melodySequence;

    [Header("General")]
    [SerializeField] private Transform[] _uI_noteDisplays;
    [Header("Notes")]
    private ArrayList _uI_notesFromMelodySequence;
    private ArrayList _uI_notesFromMelodySequenceStrings = new ArrayList();
    [Header("Background")]
    private ArrayList _uI_backgroundColours;
    private ArrayList _uI_backgroundColoursStrings;
    [Header("Octaves")]
    private ArrayList _uI_octavesAll;
    private ArrayList _uI_octavesStringsAll;
    [Header("Accidentals")]
    private ArrayList _uI_accidentalsAll;
    private ArrayList _uI_accidentalsStringsAll;
    [Header("Sequences")]
    private List<int> _uI_notesSequenceIndex = new List<int>();
    private List<string> _uI_octaveSequenceStringsSetInIdentity = new List<string>();
    private List<string> _uI_accidentalSequenceStringsSetInIdentity = new List<string>();
    [Header("Misc")]
    public static bool ColourHelpState = false;
    private TargetController _currentClosestTarget;
    private Image _currentFrameDistance = null;
    [Header("Sprites")]
    public Sprite UI_BackgroundGrey;

    private void Start()
    {
        ColourHelpState = StartMenuManager.ActivateColour;
        UpdateUI();
    }

    private void Update()
    {
        ActivateFrameDistance();
    }

    private void UpdateUI()
    {
        LoadUINotesFromResources();
        IterateSequences();

        // UI Initialization
        InitializeUINotes();
        InitializeUIBackgrounds();
        InitializeUIOctaves();
        InitializeAccidentals();

        // SetSequencesToUI();
    }

    private void SetSequencesToUI()
    {
        foreach (Transform child in transform)
        {
            Transform noteDisplay = child.transform.GetChild(0);
            string accidental = noteDisplay.GetComponent<ListItemIdentity>().Accidental;
            int indexCurrentAccidental = _uI_accidentalsStringsAll.IndexOf(accidental);
            Sprite spriteCurrentAccidental = _uI_accidentalsAll[indexCurrentAccidental] as Sprite;
            Image accidentalObject = noteDisplay.GetChild(4).GetComponent<Image>();
            accidentalObject.sprite = spriteCurrentAccidental;
        }
    }

    private void InitializeAccidentals()
    {
        var load = Resources.LoadAll("UI_Accidentals", typeof(Sprite)).Cast<Sprite>();

        _uI_accidentalsAll = new ArrayList();
        _uI_accidentalsStringsAll = new ArrayList();

        foreach (var item in load)
        {
            print(item);
            _uI_accidentalsAll.Add(item);
            _uI_accidentalsStringsAll.Add(item.name);
        }
        SetSequencesToUI();

    }

    private void InitializeUIOctaves()
    {
        var load = Resources.LoadAll("UI_Octaves", typeof(Sprite)).Cast<Sprite>();

        _uI_octavesAll = new ArrayList();
        _uI_octavesStringsAll = new ArrayList();

        foreach (var item in load)
        {
            _uI_octavesAll.Add(item);
            _uI_octavesStringsAll.Add(item.name);
        }

        foreach (Transform child in transform)
        {
            Transform noteDisplay = child.transform.GetChild(0);
            string octave = noteDisplay.GetComponent<ListItemIdentity>().Octave;
            int indexCurrentOctave = _uI_octavesStringsAll.IndexOf(octave);
            Sprite spriteCurrentOctave = _uI_octavesAll[indexCurrentOctave] as Sprite;
            noteDisplay.GetChild(3).GetComponent<Image>().sprite = spriteCurrentOctave;
        }
    }

    // public void ActivateColourMode()
    // {
    //     ColourHelpState = true;
    // }

    public List<Transform> CheckIfTonesCompleted()
    {
        List<Transform> missingTones = new List<Transform>();

        foreach (Transform child in transform)
        {
            if (child.GetComponent<ListItemIdentity>().LockState == false)
            {
                missingTones.Add(child);
            }
        }

        return missingTones;
    }

    public void ToggleListColourHelp()
    {
        ColourHelpState = ColourHelpState == true ? false : true;
        UpdateUI();
        ToggleUIFrames();
        LoadUIBackgroundsColourFromResources();
    }

    private void InitializeUIBackgrounds()
    {
        if (!ColourHelpState)
        {
            LoadUIBackgroundGrey();
        }
        else
        {
            LoadUIBackgroundsColourFromResources();
        }
    }

    private void LoadUIBackgroundGrey()
    {
        foreach (Transform item in transform)
        {
            item.GetComponent<Image>().sprite = UI_BackgroundGrey;
        }
    }

    private void ToggleUIFrames()
    {
        int x = 0;
        foreach (Transform child in transform)
        {
            if (child.GetChild(0).GetComponent<ListItemIdentity>().LockState == true)
            {
                StartCoroutine(DecreaseAlpha(GetModeAndImage(!ColourHelpState, x)));
                ActivateFrameSuccess(x++, child.name);
            }
        }
    }

    private void IterateSequences()
    {
        foreach (notes note in _melodySequence)
        {
            string objectNameAsString = note.ToString();          // Convert object name to string
            string noteName = objectNameAsString[0].ToString();   // Name
            string octave = objectNameAsString[1].ToString();     // Octave

            int digitCountBeforeAccidental = 2; // e.g. A2 = 2, A2# = 3
            string accidentalCheck = objectNameAsString.Length > digitCountBeforeAccidental ? objectNameAsString[2].ToString() : "null";
            string accidental = accidentalCheck; // Accidental or null  

            int indexOfCurrentMelody = _uI_notesFromMelodySequenceStrings.IndexOf(noteName); // Convert string back to object

            _uI_accidentalSequenceStringsSetInIdentity.Add(accidental);
            _uI_octaveSequenceStringsSetInIdentity.Add(octave);
            _uI_notesSequenceIndex.Add(indexOfCurrentMelody);
        }
    }

    private void InitializeUINotes()
    {
        int x = 0;
        foreach (Transform noteDisplay in _uI_noteDisplays)
        {
            int indexCurrentNote = _uI_notesSequenceIndex[x];
            Sprite currentNoteSprite = _uI_notesFromMelodySequence[indexCurrentNote] as Sprite;

            string currentNoteName = currentNoteSprite.ToString();
            string currentOctave = _uI_octaveSequenceStringsSetInIdentity[x];
            string currentAccidental = _uI_accidentalSequenceStringsSetInIdentity[x];

            noteDisplay.GetComponent<Image>().sprite = currentNoteSprite;
            noteDisplay.GetComponent<ListItemIdentity>().ToneName = currentNoteName;
            noteDisplay.GetComponent<ListItemIdentity>().Octave = currentOctave;
            noteDisplay.GetComponent<ListItemIdentity>().Accidental = currentAccidental;
            x++;
        }
    }

    private void LoadUINotesFromResources()
    {
        var load = Resources.LoadAll("UI_Notes", typeof(Sprite)).Cast<Sprite>();

        _uI_notesFromMelodySequence = new ArrayList();
        foreach (var ui_notes in load)
        {
            _uI_notesFromMelodySequence.Add(ui_notes);
            _uI_notesFromMelodySequenceStrings.Add(ui_notes.name);
        }
    }

    private void LoadUIBackgroundsColourFromResources()
    {
        var load = Resources.LoadAll("UI_BackgroundColour", typeof(Sprite)).Cast<Sprite>();

        _uI_backgroundColours = new ArrayList();
        _uI_backgroundColoursStrings = new ArrayList();

        foreach (var colour in load)
        {
            _uI_backgroundColours.Add(colour);
            _uI_backgroundColoursStrings.Add(colour.name);
        }

        int x = 0;
        foreach (Transform child in transform)
        {
            string melodyIterationAsString = _melodySequence[x++].ToString();          // get rid of octave information
            string melodyIterationName = melodyIterationAsString[0].ToString();

            int _indexOfChild = _uI_backgroundColoursStrings.IndexOf(melodyIterationName);

            child.GetComponent<Image>().sprite = _uI_backgroundColours[_indexOfChild] as Sprite;
        }

    }

    public void ActivateFrameSuccess(int tappedListItemIndex, string tappedName)
    {
        // Set the sprite within the list UI
        // if checks null on game stop
        if (_uI_noteDisplays[tappedListItemIndex] != null)
        {
            Image currentFrameSuccess = GetModeAndImage(ColourHelpState, tappedListItemIndex);
            _uI_noteDisplays[tappedListItemIndex].GetComponent<ListItemIdentity>().LockState = true;
            StartCoroutine(IncreaseAlpha(currentFrameSuccess));
        }
    }

    private Image GetModeAndImage(bool ColourHelpOn, int index)
    {
        int mode = ColourHelpOn ? 2 : 1;
        Image currentFrameSuccess = _uI_noteDisplays[index].GetChild(mode).GetComponent<Image>();
        return currentFrameSuccess;
    }

    private void ActivateFrameDistance()
    {
        TargetController closestTarget = DistanceToTarget.ReturnClosestTarget();

        if (closestTarget != _currentClosestTarget)
        {
            // Safety for less calculations
            _currentClosestTarget = closestTarget;

            // Make 'old' frame invisible
            if (_currentFrameDistance != null)
            {
                StartCoroutine(DecreaseAlpha(_currentFrameDistance));
            }
            // Get new list frame
            _currentFrameDistance = _uI_noteDisplays[_currentClosestTarget.getIndexInSequence()].GetChild(0).GetComponent<Image>();
            // Make it visible
            StartCoroutine(IncreaseAlpha(_currentFrameDistance));
        }
    }

    public void DecreaseAlphaIfZeroTargets()
    {
        StartCoroutine(DecreaseAlpha(_currentFrameDistance));
    }

    IEnumerator DecreaseAlpha(Image currentFrame)
    {
        float time = 0f;
        float duration = 1f;
        float startValue = 1f;
        float endValue = 0f;

        while (time < duration)
        {

            Color newColor = currentFrame.color;
            newColor.a = Mathf.Lerp(startValue, endValue, time / duration);
            currentFrame.color = newColor;

            time += Time.deltaTime;
            yield return null;
        }
        Color newColorSafety = currentFrame.color;
        newColorSafety.a = endValue;
        currentFrame.color = newColorSafety;
    }

    // https://gamedevbeginner.com/the-right-way-to-lerp-in-unity-with-examples/
    IEnumerator IncreaseAlpha(Image currentImageComponent)
    {
        float time = 0f;
        float duration = 1f;
        float startValue = 0f;
        float endValue = 1f;

        while (time < duration)
        {
            Color newColor = currentImageComponent.color;
            newColor.a = Mathf.Lerp(startValue, endValue, time / duration);
            currentImageComponent.color = newColor;

            time += Time.deltaTime;
            yield return null;
        }
        Color newColorSafety = currentImageComponent.color;
        newColorSafety.a = endValue;
        currentImageComponent.color = newColorSafety;
    }
}