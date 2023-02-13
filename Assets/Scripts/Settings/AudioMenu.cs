using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioMenu : MonoBehaviour
{
    public Button[] buttons;
    private Button chosenButton;
    private int selection = 0;

    private void Start()
    {
        // buttons[selection].Select();
    }
    void Update()
    {
        buttons[selection].Select();
        GetArrowDown();
        GetArrowUp();
        GetEnterKey();
    }

    private void GetArrowUp()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if ((selection - 1) < 0)
            {
                selection = (buttons.Length - 1);
            }
            else
            {
                selection--;
            }
            print(buttons[selection].name);
            // buttons[selection].Select();
        }
    }

    private void GetArrowDown()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if ((selection + 1) > (buttons.Length - 1))
            {
                selection = 0;
            }
            else
            {
                selection++;
                print(selection);
                print(buttons.Length);

            }
            print(buttons[selection].name);
            // buttons[selection].Select();
        }
    }

    private void GetEnterKey()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            print(buttons[selection].name);
            // buttons[selection].Select();
            // buttons[selection].onClick.Invoke();
        }
    }
}
