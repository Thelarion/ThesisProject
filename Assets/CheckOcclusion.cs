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
        CheckIfPlayerBehindWall();
    }

    private void CheckIfPlayerBehindWall()
    {
        // layermask 9 = ignore Ground (applied to reverb zones, slingshot and ground)
        if (Physics.Linecast(transform.position, player.transform.position, out playerOcclusionCheck, layerMask))
        {
            if (playerOcclusionCheck.transform.gameObject.tag != "Player")
            {
                AkSoundEngine.SetRTPCValue("wall_occlusion", 1);
            }
            else AkSoundEngine.SetRTPCValue("wall_occlusion", 0);
        }
    }
}
