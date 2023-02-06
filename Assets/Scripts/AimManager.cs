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
    public bool AssitanceStateOff = true;

    void FixedUpdate()
    {
        CheckIfTargetBehindWall();
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
            // cube.transform.position - transform.position => vector from transfrom.position to the obj position
            // https://www.reddit.com/r/Unity3D/comments/p0zfi4/how_do_i_get_an_angle_from_two_vectors/
            float angleHorizontal = Vector3.SignedAngle(transform.TransformDirection(Vector3.forward), currentTarget.transform.position - transform.position, Vector3.up);
            // Debug.Log(angleHorizontal);
            DisplayHorizontalCommmand(angleHorizontal);

            float angleVertical = Vector3.SignedAngle(transform.TransformDirection(Vector3.forward), currentTarget.transform.position - transform.position, Vector3.right);
            // Debug.Log(angleVertical);
            DisplayVerticalCommand(angleVertical);

            float lengthRayForwardPlusDistanceFromRayEndpoint = distanceRayEndpointToObject + lengthCameraToGetPoint;
            // Root is for increasing the small digits
            float distanceFreq = (Mathf.Sqrt(lengthRayForwardPlusDistanceFromRayEndpoint - distanceCameraToObject));

            AkSoundEngine.SetRTPCValue("distance_freq", distanceFreq);

            // Draw ray and lines
            // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * lengthCameraToGetPoint, Color.red);
            // Debug.DrawLine(rayCameraForward.GetPoint(lengthCameraToGetPoint), currentTarget.transform.position, Color.green);
            // Debug.DrawLine(transform.position, currentTarget.transform.position, Color.yellow);

            // Outputs on UI
            // rightRayToObject.text = lengthRayForwardPlusDistanceFromRayEndpoint.ToString();
            // leftCameraToObject.text = distanceCameraToObject.ToString();
            // topLeftDifference.text = (Mathf.Sqrt(lengthRayForwardPlusDistanceFromRayEndpoint - distanceCameraToObject)).ToString("F3");
        }
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
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitOnTargetCheck, Mathf.Infinity, layerMask))
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
            // layermask 9 = ignore Ground (applied to reverb zones, slingshot and ground)
            // layermask 8 = ignore Character 
            if (Physics.Linecast(transform.position, currentTarget.transform.position, out hitWallCheck, layerMask))
            {
                // print(hitWallCheck.transform.gameObject.name);
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

    private void DetermineCurrentTarget()
    {
        // Get the Instances from Target Controller
        var targets = TargetController.Instances;

        // Set the initial distance to infinity high
        float distanceCameraNextTarget = Mathf.Infinity;

        TargetController currentLoopObjectShortestDistance = null;

        if (targets.Count > 0)
        {
            // Loop over all target in order to get the target with the lowest distance to the player
            foreach (var target in targets)
            {
                // Get the distance value of the recent object
                float compareValueTarget = Vector3.Distance(transform.position, target.transform.position);

                // Compare the most recent value with the saved value 
                if (compareValueTarget < distanceCameraNextTarget)
                {
                    distanceCameraNextTarget = compareValueTarget;
                    currentLoopObjectShortestDistance = target;

                };
            }
            SetUpNewTarget(currentLoopObjectShortestDistance);
        }
    }

    private void SetUpNewTarget(TargetController target)
    {
        // Set the new target
        currentTarget = target.gameObject;
        // Activate the outline
        // currentTarget.GetComponent<ChangeOutline>().ActivateOutline(outline);
        // Post the aim frequency
        frequencyDistance.Post(gameObject);
    }

    private void DisplayHorizontalCommmand(float angleHorizontal)
    {
        if (angleHorizontal > 0)
        {
            // Aim to the left
            AkSoundEngine.SetRTPCValue("frequency_depth", 0f);
        }
        else if (angleHorizontal < 0)
        {
            // Aim to the reft
            AkSoundEngine.SetRTPCValue("frequency_depth", 100f);
        }
    }

    private void DisplayVerticalCommand(float angleVertical)
    {
        if (angleVertical > 0)
        {
            // Aim down
            AkSoundEngine.SetRTPCValue("synth_pwm_up_down", 50f);
        }
        else if (angleVertical < 0)
        {
            // Aim up
            AkSoundEngine.SetRTPCValue("synth_pwm_up_down", 0f);
        }
    }

    // public void Fire()
    // {
    //     if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitTargetCheck, Mathf.Infinity, layerMask))
    //     {
    //         if (hitTargetCheck.transform.tag == "Target")
    //         {
    // Deal 25 int damage
    // TargetController currentTargetController = hitTargetCheck.transform.GetComponent<TargetController>();
    // currentTargetController.takeDamage(25);
    // Check if health is above 0
    // if (!currentTargetController.checkHasHealth())
    // {
    // hitOnTargetCheck.transform.GetComponent<AkAmbient>().Stop(0);
    // Destroy(hitTargetCheck.transform.gameObject);
    // audioManager.StopDistanceFrequency(gameObject);
    // currentTarget.GetComponent<ChangeOutline>().DeactivateOutline();
    // currentTarget = null;
    // }
    //         }
    //     }
    // }
}
