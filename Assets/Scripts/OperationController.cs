using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class OperationController : MonoBehaviour
{
    [SerializeField]
    public enum colors
    {
        Yellow,
        Red,
        Blue,
    }

    [SerializeField] public colors[] _melodySequence;
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
        foreach (colors name in _melodySequence)
        {
            _sequenceIndex.Add(_spritesAvailableStrings.IndexOf(name.ToString()));
        }
    }

    private void InitilizeUI()
    {
        int x = 0;
        foreach (Transform L in _listUI)
        {
            L.GetComponent<Image>().sprite = _spritesAvailable[_sequenceIndex[x]] as Sprite;
            x++;
        }
    }

    private void LoadSpritesFromResources()
    {
        var load = Resources.LoadAll("SpritesBalloon", typeof(Sprite)).Cast<Sprite>();
        foreach (var material in load)
        {
            _spritesAvailable.Add(material);
            _spritesAvailableStrings.Add(material.name);

        }
    }

    public void ActivateListFrame()
    {
        // ListUI.GetComponent<Image>().sprite = sprites[];
        // print(ListUI.GetComponent<Image>().sprite);
    }
}