using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ScoreManager : MonoBehaviour
{
    private int _currentScore = 0;
    public Image FirstDigitScore;
    public Image SecondDigitScore;
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

    public void CalculatePoints(int missedTaps)
    {
        _currentScore = _currentScore + getScoreSystem(missedTaps);
        SetToSprite();
    }

    private int getScoreSystem(int missedTaps)
    {
        switch (missedTaps)
        {
            case 0:
                return 10;
            case 1:
                return 8;
            case 2:
                return 4;
            default:
                return 0;
        }
    }

    private void SetToSprite()
    {
        string scoreToString = _currentScore.ToString();
        string firstDigit;
        string secondDigit;

        if (scoreToString.Length < 2)
        {

            firstDigit = "0";
            secondDigit = scoreToString[0].ToString();
        }
        else
        {
            firstDigit = scoreToString[0].ToString();
            secondDigit = scoreToString[1].ToString();
        }

        int indexOfFirstDigit = digitSpritesStrings.IndexOf(firstDigit);
        int indexOfSecondDigit = digitSpritesStrings.IndexOf(secondDigit);

        Sprite currentScoreFirstDigitSprite = digitSprites[indexOfFirstDigit] as Sprite;
        Sprite currentScoreSecondDigitSprite = digitSprites[indexOfSecondDigit] as Sprite;

        FirstDigitScore.sprite = currentScoreFirstDigitSprite;
        SecondDigitScore.sprite = currentScoreSecondDigitSprite;
    }
}
