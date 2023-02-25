using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    private static bool _inclusionState = false;
    private static bool _activateColour;
    public static bool InclusionState
    {
        get { return _inclusionState; }
        set { _inclusionState = value; }
    }

    public static bool ActivateColour
    {
        get { return _activateColour; }
        set { _activateColour = value; }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Forest");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
