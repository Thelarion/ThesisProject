using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InstructionManager : MonoBehaviour
{
    private LevelLoader levelLoader;
    private int childIndex = 0;
    void Start()
    {
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
        print(childIndex);
        if (childIndex == 4)
        {
            print("Event time");
            AkSoundEngine.PostEvent("Stop_BGM_Menu_Placeholder", null);
        }

        AkSoundEngine.PostEvent("Play_Button_OnClick", gameObject);
        levelLoader.LoadScene();
    }
}
