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
        C2, D2, E2, F2, G2, A2, B2,
        C3, D3, E3, F3, G3, A3, B3,
        C4, D4, E4, F4, G4, A4, B4,
    }

    [SerializeField] public notes[] _melodySequence;

    [Header("General")]
    [SerializeField] private Transform[] _NoteDisplays;
    private ArrayList _uI_NotesFromMelodySequence;
    private ArrayList _uI_NotesFromMelodySequenceStrings = new ArrayList();
    private ArrayList _uI_backgroundColours;
    private ArrayList _uI_backgroundColoursStrings;
    private ArrayList _uI_octavesAll;
    private ArrayList _uI_octavesStringsAll;
    private List<int> _uI_NotesSequenceIndex = new List<int>();
    private List<string> _uI_OctaveSequenceStringsSetInIdentity = new List<string>();
    [HideInInspector] public static bool ColourHelpOn = false;
    private TargetController currentClosestTarget;
    private Image currentFrameDistance = null;
    public Sprite UI_BackgroundGrey;

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        ActivateFrameDistance();
    }

    private void UpdateUI()
    {
        LoadUINotesFromResources();
        InitializeSequence();
        InitializeUINotes();
        InitializeUIBackgrounds();
        InitializeUIOctaves();
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

    public void ActivateColourMode()
    {
        ColourHelpOn = true;
    }

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
        ColourHelpOn = ColourHelpOn == true ? false : true;
        UpdateUI();
        ToggleUIFrames();
        LoadUIBackgroundsColourFromResources();
    }

    private void InitializeUIBackgrounds()
    {
        if (!ColourHelpOn)
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
                StartCoroutine(DecreaseAlpha(GetModeAndImage(!ColourHelpOn, x)));
                ActivateFrameSuccess(x++, child.name);
            }
        }
    }

    private void InitializeSequence()
    {
        foreach (notes note in _melodySequence)
        {
            string noteNameAsString = note.ToString();
            string noteName = noteNameAsString[0].ToString();   // Name without octave information
            string octave = noteNameAsString[1].ToString();     // Octave
            int indexOfCurrentMelody = _uI_NotesFromMelodySequenceStrings.IndexOf(noteName);

            _uI_OctaveSequenceStringsSetInIdentity.Add(octave);
            _uI_NotesSequenceIndex.Add(indexOfCurrentMelody);
        }
    }

    private void InitializeUINotes()
    {
        int x = 0;
        foreach (Transform noteDisplay in _NoteDisplays)
        {
            int indexCurrentNote = _uI_NotesSequenceIndex[x];
            Sprite currentNoteSprite = _uI_NotesFromMelodySequence[indexCurrentNote] as Sprite;
            string currentNoteName = currentNoteSprite.ToString();
            string currentOctave = _uI_OctaveSequenceStringsSetInIdentity[x];

            noteDisplay.GetComponent<Image>().sprite = currentNoteSprite;
            noteDisplay.GetComponent<ListItemIdentity>().ToneName = currentNoteName;
            noteDisplay.GetComponent<ListItemIdentity>().Octave = currentOctave;
            x++;
        }
    }

    private void LoadUINotesFromResources()
    {
        var load = Resources.LoadAll("UI_Notes", typeof(Sprite)).Cast<Sprite>();

        _uI_NotesFromMelodySequence = new ArrayList();
        foreach (var ui_notes in load)
        {
            _uI_NotesFromMelodySequence.Add(ui_notes);
            _uI_NotesFromMelodySequenceStrings.Add(ui_notes.name);
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
        if (_NoteDisplays[tappedListItemIndex] != null)
        {
            Image currentFrameSuccess = GetModeAndImage(ColourHelpOn, tappedListItemIndex);
            _NoteDisplays[tappedListItemIndex].GetComponent<ListItemIdentity>().LockState = true;
            StartCoroutine(IncreaseAlpha(currentFrameSuccess));
        }
    }

    private Image GetModeAndImage(bool ColourHelpOn, int index)
    {
        int mode = ColourHelpOn ? 2 : 1;
        Image currentFrameSuccess = _NoteDisplays[index].GetChild(mode).GetComponent<Image>();
        return currentFrameSuccess;
    }

    private void ActivateFrameDistance()
    {
        TargetController closestTarget = DistanceToTarget.ReturnClosestTarget();

        if (closestTarget != currentClosestTarget)
        {
            // Safety for less calculations
            currentClosestTarget = closestTarget;

            // Make 'old' frame invisible
            if (currentFrameDistance != null)
            {
                StartCoroutine(DecreaseAlpha(currentFrameDistance));
            }
            // Get new list frame
            currentFrameDistance = _NoteDisplays[currentClosestTarget.getIndexInSequence()].GetChild(0).GetComponent<Image>();
            // Make it visible
            StartCoroutine(IncreaseAlpha(currentFrameDistance));
        }
    }

    public void DecreaseAlphaIfZeroTargets()
    {
        StartCoroutine(DecreaseAlpha(currentFrameDistance));
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
    IEnumerator IncreaseAlpha(Image currentFrame)
    {
        float time = 0f;
        float duration = 1f;
        float startValue = 0f;
        float endValue = 1f;

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
}