using UnityEngine;

// Details: AimDirector
// Activation, Toggling and Management of: Rise and fall of pitch
// Mode: Inclusion Mode
// Support: Target finding

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
        // Only activate with inclusion mode
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

    // Set the current target to focus on 
    private void SetToneGameObject()
    {
        // If a target exists
        if (DistanceToTarget.CurrentTargetIdentity != null)
        {
            // Get target with shortest distance
            GameObject currentTarget = DistanceToTarget.CurrentTargetIdentity.gameObject;

            // Change the focus once a target changes
            if (currentTarget != checkNewTarget)
            {
                checkNewTarget = currentTarget;
                targetTone = checkNewTarget;
            }
        }
        // If no targets are available anymore, set the volume to 0
        else
        {
            AkSoundEngine.SetRTPCValue("RTPC_aim_volume", 0);
        }
    }

    // Check for obstacles between the player and note
    // If object, stop the indication state
    // If vision clear, start the indicatoin state
    private void CheckForObstacles()
    {
        // Cast the line from the player to the target tone
        if (Physics.Linecast(transform.position, targetTone.transform.position, out hitInfo, layerMask))
        {
            obstacleState = true;
        }
        else
        {
            obstacleState = false;
        }
    }

    // Turn the indicator ON and OFF
    // Depending on the distance
    // Player should be closer as 25 units before the indicator sets off 
    private void ToggleAimDirection()
    {
        // Distance measuring
        if (Vector3.Distance(targetTone.transform.position, gameObject.transform.position) <= 25)
        {
            // ON
            if (!aimDirectionActiveState)
            {
                ToggleAimDirectionOn();
            }
        }
        else
        {
            // OFF
            if (aimDirectionActiveState)
            {
                ToggleAimDirectionOff();
            }
        }
    }

    // Toggle ON
    public void ToggleAimDirectionOn()
    {
        aimDirectionActiveState = true;
        AkSoundEngine.PostEvent("Play_AimSynthOne", gameObject);
        _beaconActive = true;
    }

    // Toggle OFF
    public void ToggleAimDirectionOff()
    {
        aimDirectionActiveState = false;
        AkSoundEngine.PostEvent("Stop_AimSynthOne", gameObject);
        _beaconActive = false;
    }

    // Calculate the Angle direction
    public void AngleDir()
    {
        // Get the face direction to the target tone
        var directionToEnemy = targetTone.transform.position - transform.position;
        var projectionOnRight = Vector3.Dot(directionToEnemy, transform.right);
        float directionFace = Vector3.Dot(transform.forward, (targetTone.transform.position - transform.position).normalized);

        if (!obstacleState)
        {
            // Calculate intervals in which the indicator plays
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
