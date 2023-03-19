using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    private static bool _inclusionState = false;
    private static bool _colourState = false;
    public static bool InclusionState
    {
        get { return _inclusionState; }
        set { _inclusionState = value; }
    }
    public static bool ColourState
    {
        get { return _colourState; }
        set { _colourState = value; }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("PracticeMode");
        AkSoundEngine.PostEvent("Stop_AllEvents", null);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
