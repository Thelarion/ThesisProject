using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class AimManager : MonoBehaviour
{
    public GameObject currentTarget = null;
    private RaycastHit hitOnTargetCheck;
    public AK.Wwise.Event Play_OnTarget;
    private bool isAimOnTarget = false;
    public LayerMask layerMask;

    void FixedUpdate()
    {
        if (StartMenuManager.InclusionState)
        {
            if (currentTarget == null)
            {
                DetermineCurrentTarget();
            }
            CheckIfAimOnTarget();
        }
    }


    private void DetermineCurrentTarget()
    {
        if (DistanceToTarget.CurrentTargetIdentity != null)
        {
            currentTarget = DistanceToTarget.CurrentTargetIdentity.gameObject;
        }
    }


    private void CheckIfAimOnTarget()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitOnTargetCheck, 35f, layerMask))
        {
            if (hitOnTargetCheck.collider.tag == "Target")
            {

                AkSoundEngine.SetSwitch("OnTarget", "Pitch" + hitOnTargetCheck.collider.name[0], gameObject);

                // print("Pitch" + hitOnTargetCheck.collider.name[0]);

                setAimOnTargetStatus();
            }
            // If ray hits objects that are not targets
            else setAimOffTargetStatus();
        }
        // If ray hits nothing at all
        else setAimOffTargetStatus();

    }
    private void setAimOnTargetStatus()
    {
        // print("onTarget");
        if (!isAimOnTarget)
        {
            isAimOnTarget = true;
            Play_OnTarget.Post(gameObject);
        }
    }

    private void setAimOffTargetStatus()
    {
        if (isAimOnTarget)
        {
            isAimOnTarget = false;
            Play_OnTarget.Stop(gameObject);
        }
    }


}
