using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [HideInInspector] public OperationController operationController;
    private GameObject playerStop;
    private LevelLoader levelLoader;
    private ScoreManager scoreManager;
    public bool QuickTest = false;
    private void Start()
    {
        operationController = GameObject.Find("List").GetComponent<OperationController>();
        playerStop = GameObject.Find("PlayerStop");
        levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
        scoreManager = GameObject.Find("ScoreSystem").GetComponent<ScoreManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Player")
        {
            List<Transform> missingTones = operationController.CheckIfTonesCompleted();
            print(missingTones.Count);

            if (missingTones.Count == 0)
            {
                LogManager.EndTime = Time.time;
                LogManager.TotalPoints = scoreManager.CurrentScore;
                LogManager.PrintToTxt();

                playerStop.SetActive(true);
                print("Completed!");
                levelLoader.LoadScene();
                AkSoundEngine.PostEvent("Stop_AllEvents", null);
            }
            else
            {
                // GetComponent<BoxCollider>().isTrigger = false;
                foreach (var item in missingTones)
                {
                    print("Missing tone: " + item.GetComponent<ListItemIdentity>().ToneName);
                }

                if (QuickTest)
                {

                    scoreManager.CurrentScore = 38;

                    LogManager.EndTime = Time.time;
                    LogManager.TotalPoints = scoreManager.CurrentScore;
                    LogManager.PrintToTxt();

                    playerStop.SetActive(true);
                    print("Completed!");
                    levelLoader.LoadScene();
                    AkSoundEngine.PostEvent("Stop_AllEvents", null);
                }
            }
        }
    }
}
