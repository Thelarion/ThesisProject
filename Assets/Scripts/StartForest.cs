using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartForest : MonoBehaviour
{
    private bool practiceModeEndState = false;
    private LevelLoader levelLoader;
    private ClosedCaptions closedCaptions;
    private bool announcePlayedState = false;

    private void Start()
    {
        closedCaptions = GameObject.Find("ClosedCaptions").GetComponent<ClosedCaptions>();
        levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
    }

    private void OnTriggerEnter(Collider other)
    {
        practiceModeEndState = true;
        if (StartMenuManager.ColourState)
        {
            closedCaptions.DisplayCaptionsPracticeEnd("Practice end! If you feel ready, press Enter to start the game.");
        }
        if (!announcePlayedState)
        {
            announcePlayedState = true;
            AkSoundEngine.PostEvent("Play_PracticeEnd", gameObject);
        }
    }

    private void Update()
    {
        if (practiceModeEndState)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                AkSoundEngine.PostEvent("Stop_AllEvents", null);
                levelLoader.LoadScene();
            }
        }
    }
}
