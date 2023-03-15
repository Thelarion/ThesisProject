using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using StarterAssets;

public class CanvasController : MonoBehaviour
{
    [Header("Misc")]
    public GameObject GameUI;
    public GameObject Credits;
    public GameObject MenuStartSettings;
    public Toggle InclusionModeToggle;
    public Sprite InclusionModeOnSprite;
    public AudioMenu MenuStartSettingsAudioMenu;

    [Header("Menus")]

    public GameObject MenuInGame;
    public GameObject MenuStart;
    public GameObject MenuCredits;
    private ScoreManager scoreManager;

    [Header("Variables")]
    private string sceneName;
    private FirstPersonController firstPersonController;

    // Start is called before the first frame update
    void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        sceneName = currentScene.name;

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
            scoreManager = GameObject.Find("ScoreSystem").GetComponent<ScoreManager>();
        }
        else if (sceneName == "Credits")
        {
            Credits.SetActive(true);
            AkSoundEngine.PostEvent("Play_ThankYou", gameObject);
        }

        if (currentScene.name == "StartSettings" && StartMenuManager.InclusionState == true)
        {
            Image InclusionImage = InclusionModeToggle.transform.GetChild(0).GetComponent<Image>();

            InclusionImage.sprite = InclusionModeOnSprite;
            MenuStartSettingsAudioMenu.SwitchToggleAndReadAloud(InclusionModeToggle.gameObject, InclusionModeOnSprite.name);

            InclusionModeToggle.isOn = true;
        }
    }

    private void Start()
    {
        if (sceneName == "Forest")
        {
            firstPersonController = GameObject.Find("Player").GetComponent<FirstPersonController>();
        }
    }

    private void Update()
    {
        CheckCursorLockState();
    }
    void CheckCursorLockState()
    {
        if (MenuStart.activeSelf || MenuInGame.activeSelf || MenuCredits.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (sceneName == "Forest") { firstPersonController.MenuToggle = true; }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (sceneName == "Forest") { firstPersonController.MenuToggle = false; }
        }


    }

    public void BackToStartMenu()
    {
        // AkSoundEngine.StopAll();

        if (scoreManager != null)
        {
            scoreManager.CurrentScore = 0;
        }

        if (sceneName == "Credits")
        {
            GameObject.Find("ScoreSystem").GetComponent<ScoreManager>().CurrentScore = 0;
        }

        AkSoundEngine.PostEvent("Stop_AllEvents", gameObject);
        SceneManager.LoadScene("StartScreen");
    }

}