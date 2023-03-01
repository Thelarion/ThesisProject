using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class AimManager : MonoBehaviour
{

    // public Text rightRayToObject;
    // public Text leftCameraToObject;
    // public Text topLeftDifference;
    public GameObject currentTarget = null;
    private RaycastHit hitTargetCheck;
    private RaycastHit hitWallCheck;
    private RaycastHit hitOnTargetCheck;
    public AK.Wwise.Event frequencyDistance;
    public AK.Wwise.Event onTargetWhiteNoise;
    private bool stopSFXBehindWall = false;
    private bool isAimOnTarget = false;
    public LayerMask layerMask;
    public Material outline;
    // public bool AssitanceStateOff = true;

    void FixedUpdate()
    {
        if (CheckIfTargetInRange())
        {
            // CheckIfTargetBehindWall();
            CheckIfAimOnTarget();

            if (currentTarget == null)
            {
                DetermineCurrentTarget();
            }
            else
            {
                // Create Ray for Camera (Aim Point) to Vector3.forward
                Ray rayCameraForward = new Ray(transform.position, transform.TransformDirection(Vector3.forward));

                // Measure whole distance from camera to target object
                float distanceCameraToObject = Vector3.Distance(transform.position, currentTarget.transform.position);

                // Calculate a dynamic length to select a manual ray endpoint
                float lengthCameraToGetPoint = distanceCameraToObject / 1.5f;
                // Measure the left distance from ray endpoint to target object
                float distanceRayEndpointToObject = Vector3.Distance(rayCameraForward.GetPoint(lengthCameraToGetPoint), currentTarget.transform.position);

                // Calculate the angle between the aimed position vector and the vector of the object
                // to determine left or right
                float angleHorizontal = Vector3.SignedAngle(transform.TransformDirection(Vector3.forward), currentTarget.transform.position - transform.position, Vector3.up);
                // Debug.Log(angleHorizontal);
                // DisplayHorizontalCommmand(angleHorizontal);

                float angleVertical = Vector3.SignedAngle(transform.TransformDirection(Vector3.forward), currentTarget.transform.position - transform.position, Vector3.right);
                // Debug.Log(angleVertical);
                // DisplayVerticalCommand(angleVertical);

                float lengthRayForwardPlusDistanceFromRayEndpoint = distanceRayEndpointToObject + lengthCameraToGetPoint;
                // Root is for increasing the small digits
                float distanceFreq = (Mathf.Sqrt(lengthRayForwardPlusDistanceFromRayEndpoint - distanceCameraToObject));

                // AkSoundEngine.SetRTPCValue("RTPC_distance_freq", distanceFreq);
            }
        }
    }

    private bool CheckIfTargetInRange()
    {
        GameObject nextTarget = DistanceToTarget.CurrentLoopObjectShortestDistance.gameObject;


        float distanceToTarget = Vector3.Distance(transform.position, nextTarget.transform.position);

        bool value = distanceToTarget <= 20f ? true : false;

        return value;
    }

    private void DetermineCurrentTarget()
    {
        currentTarget = DistanceToTarget.CurrentLoopObjectShortestDistance.gameObject;
        // frequencyDistance.Post(gameObject);
    }

    public void TurnAimAssitanceOff()
    {
        StopAllSounds();
    }

    public void TurnAimAssitanceOn()
    {
        // Reset target to start initial process from scratch
        currentTarget = null;
    }

    private void StopAllSounds()
    {
        onTargetWhiteNoise.Stop(gameObject);
        frequencyDistance.Stop(gameObject);
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
            onTargetWhiteNoise.Post(gameObject);
        }
    }

    private void setAimOffTargetStatus()
    {
        if (isAimOnTarget)
        {
            isAimOnTarget = false;
            onTargetWhiteNoise.Stop(gameObject);
        }
    }

    private void CheckIfTargetBehindWall()
    {
        if (currentTarget != null)
        {
            if (Physics.Linecast(transform.position, currentTarget.transform.position, out hitWallCheck, layerMask))
            {
                if (hitWallCheck.transform.gameObject.tag == "Wall")
                {
                    stopSFX();
                }
                else startSFX();
            }
        }
    }

    private void startSFX()
    {
        if (stopSFXBehindWall)
        {
            stopSFXBehindWall = false;
            frequencyDistance.Post(gameObject);
        }
    }

    private void stopSFX()
    {
        if (!stopSFXBehindWall)
        {
            stopSFXBehindWall = true;
            frequencyDistance.Stop(gameObject);
        }
    }

    private void DisplayHorizontalCommmand(float angleHorizontal)
    {
        if (angleHorizontal > 0)
        {
            // Aim to the left
            AkSoundEngine.SetRTPCValue("RTPC_frequency_depth", 0f);
        }
        else if (angleHorizontal < 0)
        {
            // Aim to the reft
            AkSoundEngine.SetRTPCValue("RTPC_frequency_depth", 100f);
        }
    }

    private void DisplayVerticalCommand(float angleVertical)
    {
        if (angleVertical > 0)
        {
            // Aim down
            AkSoundEngine.SetRTPCValue("RTPC_synth_pwm_up_down", 50f);
        }
        else if (angleVertical < 0)
        {
            // Aim up
            AkSoundEngine.SetRTPCValue("RTPC_synth_pwm_up_down", 0f);
        }
    }
}
