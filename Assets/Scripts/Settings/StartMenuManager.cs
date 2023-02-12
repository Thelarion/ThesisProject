using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    private static bool _inclusionState;
    private static bool _activateColour;

    public static bool InclusionState
    {
        get { return _inclusionState; }
        set { _inclusionState = true; }
    }

    public static bool ActivateColour
    {
        get { return _activateColour; }
        set { _activateColour = true; }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Playground");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
