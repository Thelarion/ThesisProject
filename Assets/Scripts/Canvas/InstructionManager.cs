using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InstructionManager : MonoBehaviour
{
    private LevelLoader levelLoader;
    private int childIndex = 0;
    private GameObject instructionButtons;
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

    private void CheckInclusionMode()
    {
        if (StartMenuManager.InclusionState)
        {
            AkSoundEngine.SetSwitch("Instructions", SceneManager.GetActiveScene().name, gameObject);
            AkSoundEngine.PostEvent("Play_Instructions", gameObject);
            instructionButtons.transform.GetChild(1).transform.gameObject.SetActive(true);
        }
        else
        {
            instructionButtons.transform.GetChild(0).transform.gameObject.SetActive(true);
        }

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

    private bool isPlaying = false;
    public void ListenAgain()
    {
        if (!isPlaying)
        {
            StartCoroutine(CheckPlaying());
        }
    }

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
        levelLoader.LoadScene();
    }
}
