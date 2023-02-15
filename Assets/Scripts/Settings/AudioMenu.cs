using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioMenu : MonoBehaviour
{
    public Toggle[] toggles;
    private Toggle chosenToggle;
    private int selection = 0;


    void Update()
    {
        toggles[selection].Select();
        GetArrowDown();
        GetArrowUp();
        GetArrowLeft();
        GetArrowRight();
        GetReturnKey();
        GetSpaceKey();
    }

    private void GetArrowRight()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (toggles[selection].name == "AudioMenuVolume" || toggles[selection].name == "MusicVolume" || toggles[selection].name == "EffectsVolume")
            {
                Slider slider = toggles[selection].transform.GetChild(1).GetComponent<Slider>();
                slider.value = slider.value + 1;
            }
        }
    }

    private void GetArrowLeft()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (toggles[selection].name == "AudioMenuVolume" || toggles[selection].name == "MusicVolume" || toggles[selection].name == "EffectsVolume")
            {
                Slider slider = toggles[selection].transform.GetChild(1).GetComponent<Slider>();
                slider.value = slider.value - 1;
            }
        }
    }

    private void GetArrowUp()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if ((selection - 1) < 0)
            {
                selection = (toggles.Length - 1);
            }
            else
            {
                selection--;
            }
            // print(toggles[selection].name);
            // buttons[selection].Select();
        }
    }

    private void GetArrowDown()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if ((selection + 1) > (toggles.Length - 1))
            {
                selection = 0;
            }
            else
            {
                selection++;
                // print(selection);
                // print(toggles.Length);

            }
            // print(toggles[selection].name);
            // buttons[selection].Select();
        }
    }

    private void GetReturnKey()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            print("Return");
        }
    }

    private void GetSpaceKey()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            toggles[selection].isOn = !toggles[selection].isOn;
        }
    }
}


