using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int _currentScore = 0;
    // private int _currentMaxPoints = 5;
    // private IEnumerator coroutine;

    public Text UI_CountDisplay;

    // Start is called before the first frame update
    void Start()
    {
        // coroutine = CountThreeMinutes();
        // StartCoroutine(coroutine);
    }

    public void AddPoint()
    {
        _currentScore++;

        print("CURRENT SCORE: " + _currentScore);
        UI_CountDisplay.text = _currentScore.ToString();
    }

    public void SubtractPoint()
    {
        if ((_currentScore - 1) <= 0)
        {
            _currentScore = 0;
        }
        else
        {
            _currentScore--;
        }

        print("CURRENT SCORE: " + _currentScore);
        UI_CountDisplay.text = _currentScore.ToString();
    }


    // IEnumerator CountThreeMinutes()
    // {
    //     while (true)
    //     {
    //         yield return new WaitForSeconds(10);

    //         if ((_currentMaxPoints - 1) <= 0)
    //         {
    //             _currentMaxPoints = 0;
    //             StopCoroutine(coroutine);
    //         }
    //         else
    //         {
    //             _currentMaxPoints--;
    //         }
    //         print(_currentMaxPoints);
    //     }
    // }
}
