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
    private void Start()
    {
        operationController = GameObject.Find("List").GetComponent<OperationController>();
        playerStop = GameObject.Find("PlayerStop");
        levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Player")
        {
            List<Transform> missingTones = operationController.CheckIfTonesCompleted();
            print(missingTones.Count);

            if (missingTones.Count == 0)
            {
                playerStop.SetActive(true);
                print("Completed!");
                levelLoader.LoadScene();
            }
            else
            {
                GetComponent<BoxCollider>().isTrigger = false;
                foreach (var item in missingTones)
                {
                    print("Missing tone: " + item.GetComponent<ListItemIdentity>().ToneName);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }
}
