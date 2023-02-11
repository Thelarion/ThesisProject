using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [HideInInspector] public OperationController operationController;
    private void Start()
    {
        operationController = GameObject.Find("List").GetComponent<OperationController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Player")
        {
            List<Transform> missingTones = operationController.CheckIfTonesCompleted();
            print(missingTones.Count);

            if (missingTones.Count == 0)
            {
                print("Completed!");
                SceneManager.LoadSceneAsync("Goal");
            }
            else
            {
                foreach (var item in missingTones)
                {
                    print("Missing tone: " + item.GetComponent<ListItemIdentity>().ToneName);
                }
            }
        }

    }
}
