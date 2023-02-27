using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InstructionManager : MonoBehaviour
{
    public LevelLoader levelLoader;
    void Start()
    {
        levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();

        int childIndex = 0;
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
        AkSoundEngine.SetSwitch("Instructions", SceneManager.GetActiveScene().name, gameObject);
        AkSoundEngine.PostEvent("Play_Instructions", gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            LoadNextScene();
        }
    }

    public void LoadNextScene()
    {
        AkSoundEngine.PostEvent("Play_Button_OnClick", gameObject);
        levelLoader.LoadScene();
    }
}
