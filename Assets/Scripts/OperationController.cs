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
    public AK.Wwise.Event Pling;
    [HideInInspector] public bool ColourHelpOn = false;
    private string currentColourState = "SpritesNotesGrey";
    private TargetController currentClosestTarget;
    private Image currentFrame = null;

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        ActivateDistanceFrame();
    }

    private void UpdateUI()
    {
        LoadSpritesFromResources();
        InitializeSequence();
        InitilizeUI();
    }

    public void ToggleListColourHelp()
    {
        currentColourState = currentColourState == "SpritesNotesGrey" ? "SpritesNotesColour" : "SpritesNotesGrey";
        UpdateUI();
    }

    private void InitializeSequence()
    {
        foreach (notes name in _melodySequence)
        {
            _sequenceIndex.Add(_spritesAvailableStrings.IndexOf(name.ToString()));
        }
    }

    private void InitilizeUI()
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

    public void ActivateListFrame(int tappedListElement, string tappedName)
    {
        // Get index of the GameObject name within the array
        int indexFrame = _spritesAvailableStrings.IndexOf(tappedName + "_frame");
        // Initiate the sprite wtih the index of the GameObject
        Sprite frame = _spritesAvailable[indexFrame] as Sprite;
        // Set the sprite within the list UI
        _listUI[tappedListElement].GetComponent<Image>().sprite = frame;
        Pling.Post(gameObject);
    }

    private void ActivateDistanceFrame()
    {
        TargetController closestTarget = DistanceToTarget.ReturnClosestTarget();

        if (closestTarget != currentClosestTarget)
        {
            // Safety for less calculations
            currentClosestTarget = closestTarget;


            // Make 'old' frame invisible
            if (currentFrame != null)
            {
                StartCoroutine(DecreaseAlpha(currentFrame));
            }
            // Get new list frame
            currentFrame = _listUI[currentClosestTarget.getIndexInSequence()].GetChild(0).GetComponent<Image>();
            // Make it visible
            StartCoroutine(IncreaseAlpha(currentFrame));
        }


        // pulsating alpha?
    }

    private IEnumerator DecreaseAlpha(Image currentFrame)
    {
        var time = 0f;

        while (time <= 1)
        {
            Color newColor = currentFrame.color;
            newColor.a = newColor.a - 0.01f;
            currentFrame.color = newColor;

            time += Time.deltaTime;
            yield return null;
        }
        Color newColorSafety = currentFrame.color;
        newColorSafety.a = 0f;
        currentFrame.color = newColorSafety;
    }

    private IEnumerator IncreaseAlpha(Image currentFrame)
    {
        var time = 0f;

        while (time <= 1)
        {
            Color newColor = currentFrame.color;
            newColor.a = newColor.a + 0.01f;
            currentFrame.color = newColor;

            time += Time.deltaTime;
            yield return null;
        }
        Color newColorSafety = currentFrame.color;
        newColorSafety.a = 1f;
        currentFrame.color = newColorSafety;

    }
}