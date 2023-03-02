using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearbyTrees : MonoBehaviour
{

    public AK.Wwise.Event LeavesRustling;
    private float radius = 50f;
    private bool stopIndicator = false;
    private bool stopIndicatorOnSucess = false;
    private void Start()
    {
        Invoke("DelayedStart", 20f);
    }

    private void DelayedStart()
    {
        StartCoroutine(SonifyTrees());
    }

    public void DelayIndicatorOnSucess()
    {
        StartCoroutine(ToggleIndicator());
    }

    IEnumerator ToggleIndicator()
    {
        stopIndicatorOnSucess = true;
        yield return new WaitForSeconds(30f);
        stopIndicatorOnSucess = false;
    }

    IEnumerator SonifyTrees()
    {
        Transform currentTargetTone = DistanceToTarget.CurrentTargetIdentity.transform;

        Transform treeWithShortestDistance = null;

        float shortestDistanceTreeToTarget = Mathf.Infinity;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Tree")
            {
                GameObject colliderGO = hitCollider.gameObject;
                float compareDistance = Vector3.Distance(colliderGO.transform.position, currentTargetTone.transform.position);

                // Compare the most recent value with the saved value 
                if (compareDistance < shortestDistanceTreeToTarget)
                {
                    shortestDistanceTreeToTarget = compareDistance;
                    treeWithShortestDistance = colliderGO.transform;
                };


            }
        }

        if (shortestDistanceTreeToTarget <= 40f)
        {
            stopIndicator = true;
        }
        else
        {
            stopIndicator = false;
        }
        print("Should NOT play" + shortestDistanceTreeToTarget);
        if (!stopIndicator && treeWithShortestDistance != null && !stopIndicatorOnSucess)
        {
            print("Should play" + shortestDistanceTreeToTarget);
            LeavesRustling.Post(treeWithShortestDistance.gameObject);
        }

        yield return new WaitForSeconds(15f);

        StartCoroutine(SonifyTrees());
    }
}
