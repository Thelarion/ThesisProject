using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    private GameObject player;
    private void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        SidechainMusicWhenTargetClose();
    }

    private void SidechainMusicWhenTargetClose()
    {
        if (DistanceToTarget.CurrentTargetIdentity != null)
        {
            Transform closestTarget = DistanceToTarget.CurrentTargetIdentity.transform;
            float currentDistance = Vector3.Distance(player.transform.position, closestTarget.transform.position);
            // print(currentDistance);
            AkSoundEngine.SetRTPCValue("RTPC_MusicBusFade", currentDistance);
        }
        else
        {
            AkSoundEngine.SetRTPCValue("RTPC_MusicBusFade", 20);
        }
    }
}
