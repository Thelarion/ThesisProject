using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearbyTrees : MonoBehaviour
{

    public AK.Wwise.Event LeavesRustling;
    public float radius = 30f;
    private void Start()
    {
        Invoke("DelayedStart", 20f);
    }

    private void DelayedStart()
    {
        StartCoroutine(SonifyTrees());
    }

    // void OnDrawGizmosSelected()
    // {
    //     // Draw a yellow sphere at the transform's position
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawSphere(transform.position, 30);
    // }

    IEnumerator SonifyTrees()
    {
        Transform currentTargetTone = DistanceToTarget.CurrentLoopObjectShortestDistance.transform;

        Transform treeWithShortestDistance = null;

        float shortestDistanceTreeToTarget = Mathf.Infinity;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Tree")
            {
                // print("tree");
                GameObject colliderGO = hitCollider.gameObject;
                float compareDistance = Vector3.Distance(colliderGO.transform.position, currentTargetTone.transform.position);

                // print(compareDistance);
                // Compare the most recent value with the saved value 
                if (compareDistance < shortestDistanceTreeToTarget)
                {
                    shortestDistanceTreeToTarget = compareDistance;
                    treeWithShortestDistance = colliderGO.transform;
                };


            }
        }

        if (shortestDistanceTreeToTarget >= 20f)
        {
            // print(treeWithShortestDistance);
            if (treeWithShortestDistance != null)
            {
                LeavesRustling.Post(treeWithShortestDistance.gameObject);
            }
        }
        yield return new WaitForSeconds(20f);

        StartCoroutine(SonifyTrees());
    }
}
