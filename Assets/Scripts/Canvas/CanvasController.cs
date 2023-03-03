using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public GameObject GameUI;
    public GameObject Credits;
    public GameObject MenuStartSettings;
    public Toggle InclusionModeToggle;
    public Sprite InclusionModeOnSprite;
    public AudioMenu MenuStartSettingsAudioMenu;

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
        else if (sceneName == "Credits")
        {
            Credits.SetActive(true);
        }

        if (currentScene.name == "StartSettings" && StartMenuManager.InclusionState == true)
        {
            Image InclusionImage = InclusionModeToggle.transform.GetChild(0).GetComponent<Image>();

            InclusionImage.sprite = InclusionModeOnSprite;
            MenuStartSettingsAudioMenu.SwitchToggleAndReadAloud(InclusionModeToggle.gameObject, InclusionModeOnSprite.name);

            InclusionModeToggle.isOn = true;
        }
    }
    public void BackToStartMenu()
    {
        AkSoundEngine.StopAll();
        SceneManager.LoadScene("StartSettings");
    }

}