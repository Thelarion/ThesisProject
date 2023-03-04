using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectRegionEnd : MonoBehaviour
{
    Vector3 myPosition;
    Vector3 rayDirectionForward;
    Vector3 rayDirectionLeft;
    Vector3 rayDirectionRight;
    Vector3 rayDirectionBack;
    RaycastHit detectionRay;
    int rayLengthMeters = 2;
    [SerializeField] bool regionEndState = false;
    ArrayList triggeredRegionEndIDs = new ArrayList();
    ArrayList triggeredRegionEnds = new ArrayList();
    private bool isSemaphore;

    private void Update()
    {
        CheckForRegionEndCollision();

        if (triggeredRegionEnds.Count > 0 && !isSemaphore)
        {
            StartCoroutine(EndZoneAnnouncement());
        }

    }

    IEnumerator EndZoneAnnouncement()
    {
        isSemaphore = true;
        print("EndZone");
        yield return new WaitForSeconds(5f);
        isSemaphore = false;
    }

    public void CheckForRegionEndCollision()
    {
        myPosition = transform.position;
        rayDirectionForward = transform.TransformDirection(Vector3.forward);
        rayDirectionLeft = transform.TransformDirection(Vector3.left);
        rayDirectionRight = transform.TransformDirection(Vector3.right);
        rayDirectionBack = transform.TransformDirection(Vector3.back);

        if (
        Physics.Raycast(myPosition, rayDirectionForward, out detectionRay, rayLengthMeters) ||
        Physics.Raycast(myPosition, rayDirectionLeft, out detectionRay, rayLengthMeters) ||
        Physics.Raycast(myPosition, rayDirectionRight, out detectionRay, rayLengthMeters) ||
        Physics.Raycast(myPosition, rayDirectionBack, out detectionRay, rayLengthMeters)
        )
        {
            if (detectionRay.collider.gameObject.name == "RegionEnd")
            {
                regionEndState = true;
                GameObject triggeredGO = detectionRay.collider.gameObject.GetComponentInParent<Transform>().gameObject;

                if (triggeredRegionEndIDs.IndexOf(triggeredGO.GetInstanceID()) == -1)
                {
                    triggeredRegionEndIDs.Add(triggeredGO.GetInstanceID());
                    triggeredRegionEnds.Add(triggeredGO);
                }
                detectionRay.collider.gameObject.GetComponent<RegionEndIdentity>().TransitionToAlert();
            }
        }
        else
        {
            if (regionEndState == true)
            {
                foreach (GameObject regionEnd in triggeredRegionEnds)
                {
                    regionEnd.transform.gameObject.GetComponent<RegionEndIdentity>().TransitionAlertOver();
                }

                triggeredRegionEnds.Clear();
                triggeredRegionEndIDs.Clear();
                regionEndState = false;
            }
        }
    }
}