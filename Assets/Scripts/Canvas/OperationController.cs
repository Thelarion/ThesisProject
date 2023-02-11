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
    private ArrayList _spritesUINotesFromMelodySequence;
    private ArrayList _spritesUINotesFromMelodySequenceStrings = new ArrayList();
    private ArrayList _backgroundColours;
    private ArrayList _backgroundColoursStrings;
    private List<int> _uI_sequenceIndex = new List<int>();
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
        InitializeUI();
        InitializeUIBackground();
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

    private void InitializeUIBackground()
    {
        if (!ColourHelpOn)
        {
            foreach (Transform item in transform)
            {
                item.GetComponent<Image>().sprite = UI_BackgroundGrey;
            }
        }
        else
        {

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
            string noteNameAsString = note.ToString();          // get rid of octave information
            string noteName = noteNameAsString[0].ToString();   // get rid of octave information

            _uI_sequenceIndex.Add(_spritesUINotesFromMelodySequenceStrings.IndexOf(noteName));
        }
    }

    private void InitializeUI()
    {
        int x = 0;
        foreach (Transform noteDisplay in _NoteDisplays)
        {
            noteDisplay.GetComponent<Image>().sprite = _spritesUINotesFromMelodySequence[_uI_sequenceIndex[x]] as Sprite;
            noteDisplay.GetComponent<ListItemIdentity>().toneName = _spritesUINotesFromMelodySequenceStrings[_uI_sequenceIndex[x++]].ToString();
        }
    }

    private void LoadUINotesFromResources()
    {
        var load = Resources.LoadAll("UI_Notes", typeof(Sprite)).Cast<Sprite>();

        _spritesUINotesFromMelodySequence = new ArrayList();
        foreach (var ui_notes in load)
        {
            _spritesUINotesFromMelodySequence.Add(ui_notes);
            _spritesUINotesFromMelodySequenceStrings.Add(ui_notes.name);
        }
    }

    private void LoadUIBackgroundsColourFromResources()
    {
        var load = Resources.LoadAll("UI_BackgroundColour", typeof(Sprite)).Cast<Sprite>();

        _backgroundColours = new ArrayList();
        _backgroundColoursStrings = new ArrayList();

        foreach (var colour in load)
        {
            _backgroundColours.Add(colour);
            _backgroundColoursStrings.Add(colour.name);
        }

        int x = 0;
        foreach (Transform child in transform)
        {
            string melodyIterationAsString = _melodySequence[x++].ToString();          // get rid of octave information
            string melodyIterationName = melodyIterationAsString[0].ToString();

            int _indexOfChild = _backgroundColoursStrings.IndexOf(melodyIterationName);

            child.GetComponent<Image>().sprite = _backgroundColours[_indexOfChild] as Sprite;
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