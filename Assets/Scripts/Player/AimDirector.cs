using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimDirector : MonoBehaviour
{
    public GameObject targetTone;
    private GameObject checkNewTarget;
    private float _panning;
    private bool _beaconActive = true;
    private bool aimDirectionActiveState = false;
    public LayerMask layerMask;
    public RaycastHit hitInfo;
    private bool obstacleState;

    private void Update()
    {
        if (StartMenuManager.InclusionState)
        {
            SetToneGameObject();
            ToggleAimDirection();
            CheckForObstacles();
            if (_beaconActive)
            {
                AngleDir();
            }
        }
    }

    private void CheckForObstacles()
    {
        if (Physics.Linecast(transform.position, targetTone.transform.position, out hitInfo, layerMask))
        {
            obstacleState = true;
        }
        else
        {
            obstacleState = false;
        }
    }

    private void ToggleAimDirection()
    {
        if (Vector3.Distance(targetTone.transform.position, gameObject.transform.position) <= 25)
        {
            if (!aimDirectionActiveState)
            {
                ToggleAimDirectionOn();
            }
        }
        else
        {
            if (aimDirectionActiveState)
            {
                ToggleAimDirectionOff();
            }
        }
    }

    private void SetToneGameObject()
    {
        if (DistanceToTarget.CurrentTargetIdentity != null)
        {
            GameObject currentTarget = DistanceToTarget.CurrentTargetIdentity.gameObject;

            if (currentTarget != checkNewTarget)
            {
                checkNewTarget = currentTarget;
                targetTone = checkNewTarget;
            }
        }
        else
        {
            AkSoundEngine.SetRTPCValue("RTPC_aim_volume", 0);
        }
    }

    public void ToggleAimDirectionOn()
    {
        aimDirectionActiveState = true;
        AkSoundEngine.PostEvent("Play_AimSynthOne", gameObject);
        _beaconActive = true;
    }

    public void ToggleAimDirectionOff()
    {
        aimDirectionActiveState = false;
        AkSoundEngine.PostEvent("Stop_AimSynthOne", gameObject);
        _beaconActive = false;
    }

    public void AngleDir()
    {
        var directionToEnemy = targetTone.transform.position - transform.position;
        var projectionOnRight = Vector3.Dot(directionToEnemy, transform.right);

        float directionFace = Vector3.Dot(transform.forward, (targetTone.transform.position - transform.position).normalized);

        if (!obstacleState)
        {

            // print(directionFace);

            if (directionFace >= 0.4)
            {
                if (projectionOnRight > -1.5 && projectionOnRight < 1.5)
                {
                    AkSoundEngine.SetRTPCValue("RTPC_aim_volume", 0);
                }
                else if (projectionOnRight > 30 || projectionOnRight < -30)
                {
                    AkSoundEngine.SetRTPCValue("RTPC_aim_volume", 0);
                }
                else if (projectionOnRight < -3)
                {
                    AkSoundEngine.SetRTPCValue("RTPC_aim_volume", 1);
                    projectionOnRight = projectionOnRight * (-1);
                    AkSoundEngine.SetRTPCValue("RTPC_distance_freq", projectionOnRight);
                }
                else if (projectionOnRight > 3)
                {
                    AkSoundEngine.SetRTPCValue("RTPC_aim_volume", 1);
                    AkSoundEngine.SetRTPCValue("RTPC_distance_freq", projectionOnRight);
                }
            }
            else
            {
                AkSoundEngine.SetRTPCValue("RTPC_aim_volume", 0);
            }
        }
        else
        {
            AkSoundEngine.SetRTPCValue("RTPC_aim_volume", 0);
        }
    }
}
