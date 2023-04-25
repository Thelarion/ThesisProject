using UnityEngine;

// Details: DistanceToTarget
// Controls the distance to put focus on shortest target

public class DistanceToTarget : MonoBehaviour
{
    private static TargetIdentity currentTargetIdentity = null;

    public static TargetIdentity CurrentTargetIdentity
    {
        set => currentTargetIdentity = value;
        get => currentTargetIdentity;
    }

    public TargetController TargetController;

    private void Start()
    {
        CurrentTargetIdentity = TargetController.transform.GetChild(0).GetComponent<TargetIdentity>();

        // Old Method, when all targets on the field
        // DetermineCurrentTarget();
    }

    private void Update()
    {

        // Old Method, when all targets on the field
        // DetermineCurrentTarget();
    }

    private void DetermineCurrentTarget()
    {
        // Get the Instances from Target Controller
        var targets = TargetIdentity.Instances;

        // Set the initial distance to infinity high
        float distanceCameraNextTarget = Mathf.Infinity;

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
                    CurrentTargetIdentity = target;
                };
            }
        }
    }
}
