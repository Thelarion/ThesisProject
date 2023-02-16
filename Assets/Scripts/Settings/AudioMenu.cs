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
    public AK.Wwise.Event Play_ButtonReadAloud;
    public AK.Wwise.Event Stop_ButtonReadAloud;


    public int Selection { get { return selection; } set { selection = value; ButtonReadAloud(); } }

    public void AudioMenuToggle(bool value)
    {
        if (value)
        {
            AkSoundEngine.SetState("AudioMenuState", "AudioMenuOn");
        }
        else
        {
            AkSoundEngine.SetState("AudioMenuState", "AudioMenuOff");
        }
    }

    public void SwitchToggleAndReadAloud(GameObject switchGO, string subSwitchValue)
    {
        AkSoundEngine.SetSwitch("ButtonReadAloud", switchGO.name, gameObject);
        AkSoundEngine.SetSwitch(switchGO.name, subSwitchValue, gameObject);
        PostEvents();
    }

    public void ToggleReadAloud(GameObject switchGO)
    {
        AkSoundEngine.SetSwitch("ButtonReadAloud", switchGO.name, gameObject);

        int x = 0;
        foreach (Transform item in transform)
        {
            if (item.name != switchGO.name)
            {
                x++;
            }
            else
            {
                break;
            }
        }
        print(x);
        Selection = x;
        PostEvents();
    }

    private void PostEvents()
    {
        Stop_ButtonReadAloud.Post(gameObject);
        Play_ButtonReadAloud.Post(gameObject);
    }

    private void Start()
    {
        AkSoundEngine.SetState("AudioMenuState", "AudioMenuOff");
        print(toggles[Selection].name);
        Invoke("ButtonReadAloud", 1);
    }

    void Update()
    {
        toggles[Selection].Select(); // Always highlight the selected button

        GetArrowDown();
        GetArrowUp();
        GetArrowLeft();
        GetArrowRight();
        GetReturnKey();
        GetSpaceKey();
    }

    private void ButtonReadAloud()
    {
        AkSoundEngine.SetSwitch("ButtonReadAloud", toggles[Selection].name, gameObject);
        PostEvents();
    }

    private void GetArrowRight()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            string toggleName = toggles[Selection].name;

            if (toggleName == "VoiceVolume" || toggleName == "MusicVolume" || toggleName == "EffectsVolume")
            {
                Slider slider = toggles[Selection].transform.GetChild(1).GetComponent<Slider>();
                slider.value = slider.value + 1;
            }
            else if (toggleName != "Start" && toggleName != "Quit")
            {
                toggles[Selection].isOn = !toggles[Selection].isOn;
            }
        }
    }

    private void GetArrowLeft()
    {
        string toggleName = toggles[Selection].name;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (toggleName == "VoiceVolume" || toggleName == "MusicVolume" || toggleName == "EffectsVolume")
            {
                Slider slider = toggles[Selection].transform.GetChild(1).GetComponent<Slider>();
                slider.value = slider.value - 1;
            }
            else if (toggleName != "Start" && toggleName != "Quit")
            {
                print("Left");
                toggles[Selection].isOn = !toggles[Selection].isOn;
            }
        }
    }

    private void GetArrowUp()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if ((Selection - 1) < 0)
            {
                Selection = (toggles.Length - 1);
            }
            else
            {
                Selection--;
            }
            // print(toggles[selection].name);
            // buttons[selection].Select();
        }
    }

    private void GetArrowDown()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if ((Selection + 1) > (toggles.Length - 1))
            {
                Selection = 0;
            }
            else
            {
                Selection++;
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
            toggles[Selection].isOn = !toggles[Selection].isOn;
        }
    }
}


