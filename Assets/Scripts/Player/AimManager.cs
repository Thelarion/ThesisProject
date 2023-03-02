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
        if (CheckIfTargetInRange())
        {
            if (currentTarget == null)
            {
                DetermineCurrentTarget();
            }
            CheckIfAimOnTarget();
        }
    }

    private bool CheckIfTargetInRange()
    {

        if (DistanceToTarget.CurrentTargetIdentity.gameObject != null)
        {
            GameObject nextTarget = DistanceToTarget.CurrentTargetIdentity.gameObject;


            float distanceToTarget = Vector3.Distance(transform.position, nextTarget.transform.position);

            bool value = distanceToTarget <= 20f ? true : false;

            return value;
        }
        setAimOffTargetStatus();
        return false;
    }

    private void DetermineCurrentTarget()
    {
        currentTarget = DistanceToTarget.CurrentTargetIdentity.gameObject;
    }


    private void CheckIfAimOnTarget()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitOnTargetCheck, 35f, layerMask))
        {
            if (hitOnTargetCheck.collider.tag == "Target")
            {
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
        print("onTarget");
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
