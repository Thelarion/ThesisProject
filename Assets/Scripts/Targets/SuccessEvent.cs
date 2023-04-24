using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Details: SuccessEvent
// Define what should happen if the player hits the correct note 
// = bird whistling the path to the next note

public class SuccessEvent : MonoBehaviour
{
    public AK.Wwise.Event Play_M1_Birds;
    public LayerMask layerMask;
    int i = 0;
    private NearbyTrees nearbyTreesIndicator;
    private LookManager lookManager;
    bool initalBirdWaitState = false;

    private void Start()
    {
        nearbyTreesIndicator = GameObject.Find("DirectionIndicationOnTree").GetComponent<NearbyTrees>();
        lookManager = GameObject.Find("MainCamera").GetComponent<LookManager>();
    }


    public void PlaySuccessEvent(Transform nextTransform)
    {
        i = 0;
        // Audio Cues sounds should not interupt the birds
        nearbyTreesIndicator.DelayIndicatorOnSuccess();
        // Audio Compass should not interupt the birds
        lookManager.DelayAudioCompassOnSuccess();
        // Start the sonification of the trees/birds
        StartCoroutine(SonifyTreeEnvironment(nextTransform));
    }

    IEnumerator SonifyTreeEnvironment(Transform nextTransform)
    {
        // Get the tree with the shortest distance to the target
        Transform currentTargetTone = DistanceToTarget.CurrentTargetIdentity.transform;
        Transform treeWithShortestDistance = null;

        // Set shortest distance infinite
        float shortestDistanceTreeToTarget = Mathf.Infinity;

        // Get surrounding trees in radius of 50 units
        Collider[] hitColliders = Physics.OverlapSphere(nextTransform.position, 50f, layerMask);

        // Iteratre over colliders and select the those with the shortest distance
        foreach (var hitCollider in hitColliders)
        {
            // compare the collider distance
            GameObject tree = hitCollider.gameObject;
            float compareDistance = Vector3.Distance(tree.transform.position, currentTargetTone.transform.position);

            if (compareDistance < shortestDistanceTreeToTarget)
            {
                shortestDistanceTreeToTarget = compareDistance;
                treeWithShortestDistance = tree.transform;
            };

        }

        // Wait for 12 seconds before the birds start
        // to give room for the voice over first
        if (!initalBirdWaitState)
        {
            initalBirdWaitState = true;
            yield return new WaitForSeconds(12f);
        }


        // If a tree is found with the shortest distance, play the Wwise event
        if (treeWithShortestDistance != null)
        {
            Play_M1_Birds.Post(treeWithShortestDistance.gameObject);
        }


        // Cooldown of 4 seconds before the birds whistle again
        yield return new WaitForSeconds(4f);

        if (i <= 1)
        {
            StartCoroutine(SonifyTreeEnvironment(treeWithShortestDistance));
        }
        else
        {
            initalBirdWaitState = false;
        }
        i++;
    }
}
