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
        C,
        D,
        E,
        F,
        G,
        A,
        B,
    }

    [SerializeField] public notes[] _melodySequence;
    [SerializeField] private Transform[] _listUI;
    private ArrayList _spritesAvailable;
    private ArrayList _spritesAvailableStrings = new ArrayList();
    private List<int> _sequenceIndex = new List<int>();
    [HideInInspector] public bool ColourHelpOn = false;
    private string currentColourState = "SpritesNotesGrey";
    private TargetController currentClosestTarget;
    private Image currentFrameDistance = null;

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
        LoadSpritesFromResources();
        InitializeSequence();
        InitializeUI();
    }

    public void ToggleListColourHelp()
    {
        currentColourState = currentColourState == "SpritesNotesGrey" ? "SpritesNotesColour" : "SpritesNotesGrey";
        ColourHelpOn = ColourHelpOn == true ? false : true;
        UpdateUI();
        ChangeListItemsColourMode();
    }

    private void ChangeListItemsColourMode()
    {
        int x = 0;
        foreach (Transform child in transform)
        {
            if (child.GetComponent<ListItemLockState>().LockState == true)
            {
                StartCoroutine(DecreaseAlpha(GetModeAndImage(!ColourHelpOn, x)));
                ActivateFrameSuccess(x++, child.name);
            }
        }
    }

    private void InitializeSequence()
    {
        foreach (notes name in _melodySequence)
        {
            _sequenceIndex.Add(_spritesAvailableStrings.IndexOf(name.ToString()));
        }
    }

    private void InitializeUI()
    {
        int x = 0;
        foreach (Transform L in _listUI)
        {
            L.GetComponent<Image>().sprite = _spritesAvailable[_sequenceIndex[x++]] as Sprite;
        }
    }

    private void LoadSpritesFromResources()
    {
        var load = Resources.LoadAll(currentColourState, typeof(Sprite)).Cast<Sprite>();
        _spritesAvailable = new ArrayList();
        foreach (var material in load)
        {
            _spritesAvailable.Add(material);
            _spritesAvailableStrings.Add(material.name);

        }
    }

    public void ActivateFrameSuccess(int tappedListItemIndex, string tappedName)
    {
        // Set the sprite within the list UI
        // if checks null on game stop
        if (_listUI[tappedListItemIndex] != null)
        {
            Image currentFrameSuccess = GetModeAndImage(ColourHelpOn, tappedListItemIndex);
            _listUI[tappedListItemIndex].GetComponent<ListItemLockState>().LockState = true;
            StartCoroutine(IncreaseAlpha(currentFrameSuccess));
        }
    }

    private Image GetModeAndImage(bool ColourHelpOn, int index)
    {
        int mode = ColourHelpOn ? 2 : 1;
        Image currentFrameSuccess = _listUI[index].GetChild(mode).GetComponent<Image>();
        print(mode);
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
            currentFrameDistance = _listUI[currentClosestTarget.getIndexInSequence()].GetChild(0).GetComponent<Image>();
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