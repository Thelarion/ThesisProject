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
    private ArrayList _uI_notesFromMelodySequence = new ArrayList();
    private ArrayList _uI_notesFromMelodySequenceStrings = new ArrayList();
    [Header("Background")]
    private ArrayList _uI_backgroundColours = new ArrayList();
    private ArrayList _uI_backgroundColoursStrings = new ArrayList();
    [Header("Octaves")]
    private ArrayList _uI_octavesAll = new ArrayList();
    private ArrayList _uI_octavesStringsAll = new ArrayList();
    [Header("Accidentals")]
    private ArrayList _uI_accidentalsAll = new ArrayList();
    private ArrayList _uI_accidentalsStringsAll = new ArrayList();
    [Header("Sequences")]
    private List<int> _uI_notesSequenceIndex = new List<int>();
    private List<string> _uI_octaveSequenceStringsSetInIdentity = new List<string>();
    private List<string> _uI_accidentalSequenceStringsSetInIdentity = new List<string>();
    [Header("Misc")]
    public static bool ColourHelpState = false;
    private TargetIdentity _currentClosestTarget;
    private bool _distanceFrameDelayState = false;
    private Image _currentFrameDistance = null;
    private bool _initializationDone = false;
    [Header("Sprites")]
    public Sprite UI_ListBackground;
    public Sprite UI_ListBackground_Contrast;
    [Header("Wwise")]
    public AK.Wwise.Event DistanceFrameTransition;
    private bool blockInitialPost = false;

    private void Start()
    {
        ColourHelpState = StartMenuManager.ActivateColour;
        UpdateUI();
        // ActivateFrameDistance();
    }

    private void Update()
    {
        ActivateFrameDistance();
    }

    private void UpdateUI()
    {
        if (!_initializationDone)
        {
            // Loading
            InitializeUINotes();
            InitializeUIOctaves();
            InitializeUIAccidentals();
            IterateSequences();
            SetInfosToIdentity();
            _initializationDone = true;
        }

        // Background - decide for normal or colour mode
        InitializeUIBackgrounds();

        // Set Sequences to the UI
        SetSequencesToUI();
    }

    private void InitializeUINotes()
    {
        var loaded_ui_notes = Resources.LoadAll("UI_Notes", typeof(Sprite)).Cast<Sprite>();

        foreach (var ui_note in loaded_ui_notes)
        {
            _uI_notesFromMelodySequence.Add(ui_note);
            _uI_notesFromMelodySequenceStrings.Add(ui_note.name);
        }
    }

    private void InitializeUIOctaves()
    {
        var loaded_ui_octaves = Resources.LoadAll("UI_Octaves", typeof(Sprite)).Cast<Sprite>();

        foreach (var octave in loaded_ui_octaves)
        {
            _uI_octavesAll.Add(octave);
            _uI_octavesStringsAll.Add(octave.name);
        }
    }

    private void InitializeUIAccidentals()
    {
        var loaded_ui_accidentals = Resources.LoadAll("UI_Accidentals", typeof(Sprite)).Cast<Sprite>();

        foreach (var accidental in loaded_ui_accidentals)
        {
            _uI_accidentalsAll.Add(accidental);
            _uI_accidentalsStringsAll.Add(accidental.name);
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

    private void SetInfosToIdentity()
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

    private void SetSequencesToUI()
    {
        foreach (Transform childGO in transform)
        {
            Transform noteGO = childGO.GetChild(0);
            Transform octaveGO = noteGO.GetChild(3);
            Transform accidentalGO = noteGO.GetChild(4);

            // Set up octaves
            string octave = noteGO.GetComponent<ListItemIdentity>().Octave;
            int indexCurrentOctave = _uI_octavesStringsAll.IndexOf(octave);
            Sprite spriteCurrentOctave = _uI_octavesAll[indexCurrentOctave] as Sprite;
            Image octaveImage = octaveGO.GetComponent<Image>();
            octaveImage.sprite = spriteCurrentOctave;

            // Set up accidentals
            string accidental = noteGO.GetComponent<ListItemIdentity>().Accidental;
            int indexCurrentAccidental = _uI_accidentalsStringsAll.IndexOf(accidental);
            Sprite spriteCurrentAccidental = _uI_accidentalsAll[indexCurrentAccidental] as Sprite;
            Image accidentalImage = accidentalGO.GetComponent<Image>();
            accidentalImage.sprite = spriteCurrentAccidental;
        }
    }

    public List<Transform> CheckIfTonesCompleted()
    {
        List<Transform> missingTones = new List<Transform>();

        foreach (Transform child in transform)
        {
            if (child.GetChild(0).GetComponent<ListItemIdentity>().LockState == false)
            {
                missingTones.Add(child.GetChild(0));
            }
        }

        return missingTones;
    }

    private void InitializeUIBackgrounds()
    {
        if (!ColourHelpState)
        {
            if (StartMenuManager.InclusionState == false)
            {
                SetListBackground();
            }
            else if (StartMenuManager.InclusionState == true)
            {
                SetListBackgroundContrast();
            }
        }
        else
        {
            InitializeUIBackgroundColour();
            UpdateBackgroundsToColour();
        }

        void SetListBackground()
        {
            foreach (Transform item in transform)
            {
                item.GetComponent<Image>().sprite = UI_ListBackground;
            }
        }

        void SetListBackgroundContrast()
        {
            foreach (Transform item in transform)
            {
                item.GetComponent<Image>().sprite = UI_ListBackground_Contrast;
            }
        }

        void InitializeUIBackgroundColour()
        {
            var load = Resources.LoadAll("UI_ListColours/" + GameManager.ChosenPalette, typeof(Sprite)).Cast<Sprite>();

            foreach (var colour in load)
            {
                _uI_backgroundColours.Add(colour);
                _uI_backgroundColoursStrings.Add(colour.name);
            }
        }

        void UpdateBackgroundsToColour()
        {
            int x = 0;
            foreach (Transform child in transform)
            {
                string melodyIterationAsString = _melodySequence[x++].ToString();          // get rid of octave information
                string melodyIterationName = melodyIterationAsString[0].ToString();

                int _indexOfChild = _uI_backgroundColoursStrings.IndexOf(melodyIterationName);

                child.GetComponent<Image>().sprite = _uI_backgroundColours[_indexOfChild] as Sprite;
            }
        }
    }

    public void ToggleListColourHelp()
    {
        ColourHelpState = ColourHelpState == true ? false : true;
        UpdateUI();
        ToggleUISuccessFrameColour();
    }

    private void ToggleUISuccessFrameColour()
    {
        int x = 0;
        foreach (Transform child in transform)
        {
            if (child.GetChild(0).GetComponent<ListItemIdentity>().LockState == true)
            {
                StartCoroutine(DecreaseAlpha(GetModeAndImage(!ColourHelpState, x)));
                print(child.name);
                ActivateFrameSuccess(x, child.name);
            }
            x++;
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

    public void DelayDistanceFrameCoroutine(float value)
    {
        StartCoroutine(DelayDistanceFrame(value));
    }

    public IEnumerator DelayDistanceFrame(float value)
    {
        _distanceFrameDelayState = true;
        yield return new WaitForSeconds(value);
        _distanceFrameDelayState = false;
    }

    private void ActivateFrameDistance()
    {

        if (!_distanceFrameDelayState)
        {
            StartCoroutine(DelayDistanceFrame(2.5f));
            TargetIdentity closestTarget = DistanceToTarget.CurrentLoopObjectShortestDistance;

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
                // Play transition sound
                if (blockInitialPost)
                {
                    DistanceFrameTransition.Post(gameObject);
                }
                blockInitialPost = true;
            }
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