using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceToTarget : MonoBehaviour
{
    static TargetController currentLoopObjectShortestDistance = null;

    private void Update()
    {
        DetermineCurrentTarget();
    }

    private void DetermineCurrentTarget()
    {
        // Get the Instances from Target Controller
        var targets = TargetController.Instances;

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
                    currentLoopObjectShortestDistance = target;

                };
            }
        }
    }

    public static TargetController ReturnClosestTarget()
    {
        return currentLoopObjectShortestDistance;
    }
}