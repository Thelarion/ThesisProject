using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOcclusion : MonoBehaviour
{
    public GameObject player;
    private RaycastHit playerOcclusionCheck;
    public LayerMask layerMask;

    // Update is called once per frame
    void Update()
    {
        CheckIfPlayerBehindObstacle();
    }

    private void CheckIfPlayerBehindObstacle()
    {
        if (Physics.Linecast(transform.position, player.transform.position, out playerOcclusionCheck, layerMask))
        {
            AkSoundEngine.SetRTPCValue("RTPC_Obstruction", 1, gameObject);
            // print("1");
        }
        else
        {
            AkSoundEngine.SetRTPCValue("RTPC_Obstruction", 0, gameObject);
            // print("0");
        }
    }
}
