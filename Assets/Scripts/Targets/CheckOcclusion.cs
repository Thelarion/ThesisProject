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
        Debug.DrawLine(transform.position, player.transform.position, Color.red);
        // layermask 9 = ignore Ground (applied to reverb zones, slingshot and ground)
        if (Physics.Linecast(transform.position, player.transform.position, out playerOcclusionCheck, layerMask))
        {
            if (playerOcclusionCheck.transform.gameObject.tag != "Player")
            {
                // print(playerOcclusionCheck.transform.gameObject.name);
                AkSoundEngine.SetRTPCValue("RTPC_wall_occlusion", 1);
                // print("1");
            }
            else
            {
                AkSoundEngine.SetRTPCValue("RTPC_wall_occlusion", 0);
                // print("0");
            }
        }
    }
}
