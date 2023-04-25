using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

// Details: ScoreManager
// Manages the score on the UI

public class ScoreManager : MonoBehaviour
{
    private static int _currentScore = 0;
    public Image FirstDigitScore;
    public Image SecondDigitScore;
    private ArrayList digitSprites = new ArrayList();
    private ArrayList digitSpritesStrings = new ArrayList();
    private Sprite spriteIndex;

    public int CurrentScore { get => _currentScore; set => _currentScore = value; }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "Credits")
        {
            _currentScore = 0;
        }

        // Load all prepared digits into 'load'
        var load = Resources.LoadAll("UI_DigitsScore", typeof(Sprite)).Cast<Sprite>();

        // add them as sprites
        foreach (var item in load)
        {
            digitSprites.Add(item);
            digitSpritesStrings.Add(item.name);
        }

        // Update in UI
        SetToSprite();
    }

    public void ResetPoints()
    {
        CurrentScore = 0;
    }

    public void CalculatePoints(int missedTaps)
    {
        CurrentScore = CurrentScore + getScoreSystem(missedTaps);
        SetToSprite();
    }

    // Calculation of the score system
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
        string scoreToString = CurrentScore.ToString();
        string firstDigit;
        string secondDigit;

        // Check if scre below 10
        // In that case set first digit to 0
        if (scoreToString.Length < 2)
        {
            firstDigit = "0";
            secondDigit = scoreToString[0].ToString();
        }
        // Otherwise, continue as with both digits
        else
        {
            firstDigit = scoreToString[0].ToString();
            secondDigit = scoreToString[1].ToString();
        }

        int indexOfFirstDigit = digitSpritesStrings.IndexOf(firstDigit);
        int indexOfSecondDigit = digitSpritesStrings.IndexOf(secondDigit);

        Sprite currentScoreFirstDigitSprite = digitSprites[indexOfFirstDigit] as Sprite;
        Sprite currentScoreSecondDigitSprite = digitSprites[indexOfSecondDigit] as Sprite;

        // Set the sprites
        FirstDigitScore.sprite = currentScoreFirstDigitSprite;
        SecondDigitScore.sprite = currentScoreSecondDigitSprite;
    }
}
