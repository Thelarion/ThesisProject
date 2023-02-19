using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public GameObject GameUI;
    public GameObject MenuStartSettings;

    // Start is called before the first frame update
    void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;

        if (sceneName == "StartSettings")
        {
            MenuStartSettings.SetActive(true);

        }
        else if (sceneName == "Playground")
        {
            GameUI.SetActive(true);
        }
        else if (sceneName == "Forest")
        {
            GameUI.SetActive(true);
        }
    }
}
