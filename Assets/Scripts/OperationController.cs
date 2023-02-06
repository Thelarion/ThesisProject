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
    private ArrayList _spritesAvailable = new ArrayList();
    private ArrayList _spritesAvailableStrings = new ArrayList();
    private List<int> _sequenceIndex = new List<int>();

    private void Start()
    {
        LoadSpritesFromResources();
        InitializeSequence();
        InitilizeUI();
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
        var load = Resources.LoadAll("SpritesNotes", typeof(Sprite)).Cast<Sprite>();
        foreach (var material in load)
        {
            _spritesAvailable.Add(material);
            _spritesAvailableStrings.Add(material.name);

        }
    }

    public void ActivateListFrame(int tappedListElement, string tappedName)
    {
        int indexFrame = _spritesAvailableStrings.IndexOf(tappedName + "_frame");
        Sprite frame = _spritesAvailable[indexFrame] as Sprite;
        _listUI[tappedListElement].GetComponent<Image>().sprite = frame;
    }
}