using UnityEngine;
using UnityEngine.SceneManagement;

// Details: StartMenuManager
// Start of the practice mode
// Stores the chosen game mode
// Either inclusionState or colourState

public class StartMenuManager : MonoBehaviour
{
    private static bool _inclusionState = false;
    private static bool _colourState = false;

    // Set up the inclusion state
    public static bool InclusionState
    {
        get { return _inclusionState; }
        set { _inclusionState = value; }
    }

    // Set up the colour state
    public static bool ColourState
    {
        get { return _colourState; }
        set { _colourState = value; }
    }

    // Menu start the game
    public void StartGame()
    {
        SceneManager.LoadScene("PracticeMode");
        AkSoundEngine.PostEvent("Stop_AllEvents", null);
    }

    // Menu Quit
    public void Quit()
    {
        Application.Quit();
    }

}
