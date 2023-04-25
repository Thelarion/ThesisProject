using System.Collections;
using UnityEngine;

// Details: NearbyTrees
// Play audio cues on nearby trees

public class NearbyTrees : MonoBehaviour
{

    public AK.Wwise.Event LeavesRustling;
    private float radius = 50f;
    private bool stopIndicator = false;
    private bool stopIndicatorOnSucess = false;
    private TargetIndicator targetIndicator;
    private ClosedCaptions closedCaptions;

    // Delay the audio cues, so that the player has time to get used to the environment first
    private void Start()
    {
        Invoke("DelayedStart", 20f);
        targetIndicator = GameObject.Find("TargetIndicator").GetComponent<TargetIndicator>();
        closedCaptions = GameObject.Find("ClosedCaptions").GetComponent<ClosedCaptions>();
    }

    // Delay 20 seconds
    private void DelayedStart()
    {
        StartCoroutine(SonifyTrees());
    }

    // Delay 30 seconds
    public void DelayIndicatorOnSuccess()
    {
        StartCoroutine(ToggleIndicator());
    }

    // When successful hit, stop the audio cues for 30 seconds to let the player search first
    IEnumerator ToggleIndicator()
    {
        stopIndicatorOnSucess = true;
        yield return new WaitForSeconds(30f);
        stopIndicatorOnSucess = false;
    }

    // SonifyTrees: 
    // Get the distance to the target
    // Determine the trees around the player in a specific radius
    // Determine the tree with the closest distance to the target
    // Play the audio cue on exact that object
    Transform currentTargetTone;
    IEnumerator SonifyTrees()
    {
        // Get the distance to the target
        if (DistanceToTarget.CurrentTargetIdentity != null)
        {
            currentTargetTone = DistanceToTarget.CurrentTargetIdentity.transform;
        }

        Transform treeWithShortestDistance = null;

        float shortestDistanceTreeToTarget = Mathf.Infinity;

        // Determine the trees around the player in a specific radius
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
        // Play the audio cue on exact that object
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

        yield return new WaitForSeconds(5f);

        StartCoroutine(SonifyTrees());
    }
}
