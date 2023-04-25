using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using StarterAssets;

// Details: CanvasController
// Overarching controller for child elements of the UI

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

    // Determine the currenct scene and set the UI accordingly
    void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        sceneName = currentScene.name;

        // Startmenu
        if (sceneName == "StartSettings")
        {
            MenuStartSettings.SetActive(true);

        }

        // Playground
        else if (sceneName == "Playground")
        {
            GameUI.SetActive(true);
        }

        // Practice mode or Forest
        else if (sceneName == "Forest" || sceneName == "PracticeMode")
        {
            GameUI.SetActive(true);
            scoreManager = GameObject.Find("ScoreSystem").GetComponent<ScoreManager>();
        }

        // Credits
        else if (sceneName == "Credits")
        {
            Credits.SetActive(true);
            AkSoundEngine.PostEvent("Play_ThankYou", gameObject);
        }

        // Switch to inclusion mode (contrasting interface)
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
        if (sceneName == "Forest" || sceneName == "PracticeMode")
        {
            firstPersonController = GameObject.Find("Player").GetComponent<FirstPersonController>();
        }
    }

    private void Update()
    {
        CheckCursorLockState();
    }

    // If any menu is open, activate the visibility of the cursor
    void CheckCursorLockState()
    {
        if (MenuStart.activeSelf || MenuInGame.activeSelf || MenuCredits.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (sceneName == "Forest" || sceneName == "PracticeMode") { firstPersonController.MenuToggle = true; }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (sceneName == "Forest" || sceneName == "PracticeMode") { firstPersonController.MenuToggle = false; }
        }
    }

    public void BackToStartMenu()
    {
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