using UnityEngine;

// Details: AimManager
// Check if the aim is aligned to the target

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

    // Put focus on target with shortest distance
    private void DetermineCurrentTarget()
    {
        if (DistanceToTarget.CurrentTargetIdentity != null)
        {
            currentTarget = DistanceToTarget.CurrentTargetIdentity.gameObject;
        }
    }

    // Check if the aim is aligned with the target
    // If yes, play and indication Wwise event
    private void CheckIfAimOnTarget()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitOnTargetCheck, 25f, layerMask))
        {
            // Collision with the target
            if (hitOnTargetCheck.collider.tag == "Target")
            {
                AkSoundEngine.SetSwitch("OnTarget", "Pitch" + hitOnTargetCheck.collider.name[0], gameObject);

                // Aim Target status ON
                setAimOnTargetStatus();
            }
            // If ray hits objects that are not targets
            else setAimOffTargetStatus();
        }
        // If ray hits nothing at all
        else setAimOffTargetStatus();
    }

    // Aim Target ON
    private void setAimOnTargetStatus()
    {
        if (!isAimOnTarget)
        {
            isAimOnTarget = true;
            Play_OnTarget.Post(gameObject);
        }
    }

    // Aim Target OFF
    private void setAimOffTargetStatus()
    {
        if (isAimOnTarget)
        {
            isAimOnTarget = false;
            Play_OnTarget.Stop(gameObject);
        }
    }
}
