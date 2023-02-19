using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject GameUI;
    public GameObject MenuStartSettings;
    // public GameObject InGameSettings;
    public GameObject Slider;

    // Start is called before the first frame update
    void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of t$$anonymous$$s scene.
        string sceneName = currentScene.name;

        if (sceneName == "StartSettings")
        {
            MenuPanel.SetActive(true);
            MenuStartSettings.SetActive(true);
            Slider.SetActive(true);

        }
        else if (sceneName == "Playground")
        {
            MenuPanel.SetActive(true);
            Slider.SetActive(false);
            GameUI.SetActive(true);
            // InGameSettings.SetActive(true);
        }
    }
}
