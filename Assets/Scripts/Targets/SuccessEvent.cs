using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        nearbyTreesIndicator.DelayIndicatorOnSucess();
        lookManager.DelayAudioCompassOnSucess();
        StartCoroutine(SonifyTreeEnvironment(nextTransform));
    }

    IEnumerator SonifyTreeEnvironment(Transform nextTransform)
    {
        Transform currentTargetTone = DistanceToTarget.CurrentTargetIdentity.transform;

        Transform treeWithShortestDistance = null;

        float shortestDistanceTreeToTarget = Mathf.Infinity;

        Collider[] hitColliders = Physics.OverlapSphere(nextTransform.position, 50f, layerMask);
        foreach (var hitCollider in hitColliders)
        {

            GameObject tree = hitCollider.gameObject;
            float compareDistance = Vector3.Distance(tree.transform.position, currentTargetTone.transform.position);

            if (compareDistance < shortestDistanceTreeToTarget)
            {
                shortestDistanceTreeToTarget = compareDistance;
                treeWithShortestDistance = tree.transform;
            };

        }

        if (!initalBirdWaitState)
        {
            initalBirdWaitState = true;
            yield return new WaitForSeconds(12f);
        }



        if (treeWithShortestDistance != null)
        {
            // print(shortestDistanceTreeToTarget);
            // print(treeWithShortestDistance.name);
            Play_M1_Birds.Post(treeWithShortestDistance.gameObject);
        }


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
