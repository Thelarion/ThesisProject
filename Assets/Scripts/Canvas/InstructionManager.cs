using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Details: InstructionManager
// Manage the instructions

public class InstructionManager : MonoBehaviour
{
    private LevelLoader levelLoader;
    private int childIndex = 0;
    private GameObject instructionButtons;

    // Check for the current instructions page
    void Start()
    {
        instructionButtons = GameObject.Find("InstructionButtons");
        levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();

        switch (SceneManager.GetActiveScene().name)
        {
            case "I1":
                break;
            case "I2":
                childIndex += 1;
                break;
            case "I3":
                childIndex += 2;
                break;
            case "I4":
                childIndex += 3;
                break;
            case "I5":
                childIndex += 4;
                break;
        }
        transform.GetChild(childIndex).gameObject.SetActive(true);
        CheckInclusionMode();
    }

    // If inclusion mode is on, read the instructions aloud
    private void CheckInclusionMode()
    {
        if (StartMenuManager.InclusionState)
        {
            instructionButtons.transform.GetChild(1).transform.gameObject.SetActive(true);
            AkSoundEngine.SetSwitch("Instructions", SceneManager.GetActiveScene().name, gameObject);
            Invoke("PlayInstructionVoice", 1f);
        }
        else
        {
            instructionButtons.transform.GetChild(0).transform.gameObject.SetActive(true);
        }

    }

    // Play the event
    void PlayInstructionVoice()
    {
        AkSoundEngine.PostEvent("Play_Instructions", gameObject);
    }

    void Update()
    {
        if (!StartMenuManager.InclusionState)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                LoadNextScene();
            }
        }
    }

    // The player can listen again to the instructions
    private bool isPlaying = false;
    public void ListenAgain()
    {
        if (!isPlaying)
        {
            StartCoroutine(CheckPlaying());
        }
    }

    // Make sure the player does not accidentaly plays the instructions twice (within 3 seconds)
    IEnumerator CheckPlaying()
    {
        isPlaying = true;
        AkSoundEngine.PostEvent("Play_Instructions", gameObject);
        yield return new WaitForSeconds(3f);
        isPlaying = false;
    }

    public void LoadNextScene()
    {
        if (childIndex == 4)
        {
            AkSoundEngine.PostEvent("Stop_AllEvents", null);
        }

        AkSoundEngine.PostEvent("Play_Button_OnClick", gameObject);
        AkSoundEngine.PostEvent("Stop_Instructions", gameObject);
        levelLoader.LoadScene();
    }
}
