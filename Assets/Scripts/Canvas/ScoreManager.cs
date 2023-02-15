using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ScoreManager : MonoBehaviour
{
    private int _currentScore = 0;
    public Image DigitScore;
    private ArrayList digitSprites = new ArrayList();
    private ArrayList digitSpritesStrings = new ArrayList();
    private Sprite spriteIndex;

    private void Start()
    {
        var load = Resources.LoadAll("UI_DigitsScore", typeof(Sprite)).Cast<Sprite>();

        foreach (var item in load)
        {
            digitSprites.Add(item);
            digitSpritesStrings.Add(item.name);
        }
    }

    private void Update()
    {
        print(_currentScore);
    }

    public void AddPoints()
    {
        _currentScore += 5;
        SetToSprite();
    }

    public void SubtractPoints()
    {
        if ((_currentScore - 5) <= 0)
        {
            _currentScore = 0;
        }
        else
        {
            _currentScore -= 5;
        }

        SetToSprite();
    }

    private void SetToSprite()
    {
        int indexOfDigit = digitSpritesStrings.IndexOf(_currentScore.ToString());
        Sprite currentScoreDigitSprite = digitSprites[indexOfDigit] as Sprite;

        DigitScore.sprite = currentScoreDigitSprite;
    }
}
