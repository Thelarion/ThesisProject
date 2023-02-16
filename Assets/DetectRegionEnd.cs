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
    float targetValue = 1f;
    float value = 0f;
    bool regionEndState = false;

    Material memoryMaterial;

    private void Update()
    {
        CheckForRegionEndCollision();
        CheckForRegionOK();
    }

    private void CheckForRegionOK()
    {
        if (memoryMaterial != null && !regionEndState)
        {
            TransitionAlertOver();
        }
    }

    private void TransitionAlertOver()
    {
        targetValue = 0f;
        if (value > targetValue)
        {
            value -= 0.1f * Time.deltaTime;
            memoryMaterial.SetFloat("Vector1_8ec0b323bcf242c6b66b93c8c9a3b7bc", value);
        }
        if (value <= 0)
        {
            memoryMaterial = null;
        }
    }

    int rayLengthMeters = 2;

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
            regionEndState = true;

            if (detectionRay.collider.gameObject.name == "RegionEnd")
            {
                TransitionToAlert();
            }
        }
        else
        {
            regionEndState = false;
        }
    }

    private void TransitionToAlert()
    {
        targetValue = 1f;
        if (value < targetValue)
        {
            value += 0.1f * (Time.deltaTime * 1.5f);
            print(value);
            memoryMaterial = detectionRay.collider.gameObject.GetComponent<Renderer>().material;
            memoryMaterial.SetFloat("Vector1_8ec0b323bcf242c6b66b93c8c9a3b7bc", value);
        }
    }
}
