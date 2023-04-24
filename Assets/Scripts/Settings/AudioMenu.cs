using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Details: AudioMenu
// Controls the voice overs in the menus

public class AudioMenu : MonoBehaviour
{
    public Toggle[] toggles;
    private Toggle chosenToggle;
    private int selection = 0;
    public AK.Wwise.Event Play_ButtonReadAloud;
    public AK.Wwise.Event Stop_ButtonReadAloud;
    public AK.Wwise.Event Play_Button_OnClick;
    private static bool menuInitializedState = false;


    public int Selection { get { return selection; } set { selection = value; ButtonReadAloud(); } }

    public static bool MenuInitializedState { get => menuInitializedState; set => menuInitializedState = value; }

    // Activate the voice over menu based on the chosen mode
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

    // Parse the button name and read it out loud
    public void SwitchToggleAndReadAloud(GameObject switchGO, string subSwitchValue)
    {
        // Switch between the buttons, related to the switches created in Wwise
        AkSoundEngine.SetSwitch("ButtonReadAloud", switchGO.name, gameObject);
        AkSoundEngine.SetSwitch(switchGO.name, subSwitchValue, gameObject);
        // Stop and Play the events
        PostEvents();
    }

    // Iterate through the array of buttons
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

    // Set switch to selection
    public void ButtonReadAloud()
    {
        AkSoundEngine.SetSwitch("ButtonReadAloud", toggles[Selection].name, gameObject);
        PostEvents();
    }

    // Main function to post the events
    private void PostEvents()
    {
        Stop_ButtonReadAloud.Post(gameObject);
        Play_ButtonReadAloud.Post(gameObject);
    }

    private void Start()
    {
        // If the current scene is the start menu, then play a welcome and offer to switch on the inclusion mode
        if (!MenuInitializedState && SceneManager.GetActiveScene().name == "StartSettings")
        {
            AkSoundEngine.SetState("AudioMenuState", "AudioMenuOff");
            // Delay for 1 second to make sure everything is loaded correctly
            Invoke("ReadInclusionWelcome", 1);
            MenuInitializedState = true;
        }
    }

    // Read the start menu message
    private void ReadInclusionWelcome()
    {
        AkSoundEngine.SetSwitch("ButtonReadAloud", "WelcomeInclusion", gameObject);
        Stop_ButtonReadAloud.Post(gameObject);
        Play_ButtonReadAloud.Post(gameObject);
    }

    // Manage the button presses
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

    // Logic for Arrow Right clicks
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
            else if (toggleName != "Start" && toggleName != "Quit" && toggleName != "Resume" && toggleName != "BackToMainMenu" && toggleName != "Continue" && toggleName != "ListenAgain")
            {
                toggles[Selection].isOn = !toggles[Selection].isOn;
            }
        }
    }

    // Logic for Arrow Left clicks
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
            else if (toggleName != "Start" && toggleName != "Quit" && toggleName != "Resume" && toggleName != "BackToMainMenu" && toggleName != "Continue" && toggleName != "ListenAgain")
            {
                // print("Left");
                toggles[Selection].isOn = !toggles[Selection].isOn;
            }
        }
    }

    // Logic for Arrow Up clicks
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

    // Logic for Arrow Down clicks
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

    public void OnClickSound()
    {
        if (!(toggles[Selection].name == "Resume"))
        {
            Play_Button_OnClick.Post(gameObject);
        }
    }

}


