using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearbyTrees : MonoBehaviour
{

    public AK.Wwise.Event LeavesRustling;
    private float radius = 50f;
    private bool stopIndicator = false;
    private bool stopIndicatorOnSucess = false;
    private TargetIndicator targetIndicator;
    private ClosedCaptions closedCaptions;
    private void Start()
    {
        Invoke("DelayedStart", 20f);
        targetIndicator = GameObject.Find("TargetIndicator").GetComponent<TargetIndicator>();
        closedCaptions = GameObject.Find("ClosedCaptions").GetComponent<ClosedCaptions>();
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
    Transform currentTargetTone;
    IEnumerator SonifyTrees()
    {
        if (DistanceToTarget.CurrentTargetIdentity != null)
        {
            currentTargetTone = DistanceToTarget.CurrentTargetIdentity.transform;
        }

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
        // print("Should NOT play" + shortestDistanceTreeToTarget);
        if (!stopIndicator && treeWithShortestDistance != null && !stopIndicatorOnSucess)
        {
            // print("Should play" + shortestDistanceTreeToTarget);
            LeavesRustling.Post(treeWithShortestDistance.gameObject);
            if (StartMenuManager.ColourState)
            {
                targetIndicator.Target = treeWithShortestDistance.gameObject.transform;
                closedCaptions.DisplayCaptions("Leaves are rustling in this direction!");
            }

        }

        yield return new WaitForSeconds(9f);

        StartCoroutine(SonifyTrees());
    }
}
