using System.Collections;
using UnityEngine;

// Details: DetectRegionEnd
// Detects the mountains and stops the player from moving any further

public class DetectRegionEnd : MonoBehaviour
{
    Vector3 myPosition;
    Vector3 rayDirectionForward;
    Vector3 rayDirectionLeft;
    Vector3 rayDirectionRight;
    Vector3 rayDirectionBack;
    RaycastHit detectionRay;
    private ClosedCaptions closedCaptions;
    int rayLengthMeters = 2;
    [SerializeField] bool regionEndState = false;
    ArrayList triggeredRegionEndIDs = new ArrayList();
    ArrayList triggeredRegionEnds = new ArrayList();
    private bool isSemaphore;

    private void Start()
    {
        closedCaptions = GameObject.Find("ClosedCaptions").GetComponent<ClosedCaptions>();
    }

    private void Update()
    {
        CheckForRegionEndCollision();

        // Start the region end announcement when detected colliders greater 0
        if (triggeredRegionEnds.Count > 0 && !isSemaphore)
        {
            StartCoroutine(RegionEndAnnouncement());
        }

    }

    // Announcement to the player that there is not further way
    IEnumerator RegionEndAnnouncement()
    {
        isSemaphore = true;
        // Wwise event
        AkSoundEngine.PostEvent("Play_EndRegion", gameObject);
        // CC
        if (StartMenuManager.ColourState)
        {
            closedCaptions.DisplayCaptions("You cannot climb the mountains!");
        }
        yield return new WaitForSeconds(5f);
        isSemaphore = false;
    }

    public void CheckForRegionEndCollision()
    {
        // Prepare the Rays
        myPosition = transform.position;
        rayDirectionForward = transform.TransformDirection(Vector3.forward);
        rayDirectionLeft = transform.TransformDirection(Vector3.left);
        rayDirectionRight = transform.TransformDirection(Vector3.right);
        rayDirectionBack = transform.TransformDirection(Vector3.back);

        // Raycast in each 4 cardinal directions, to detect the colliders from each side
        if (
        Physics.Raycast(myPosition, rayDirectionForward, out detectionRay, rayLengthMeters) ||
        Physics.Raycast(myPosition, rayDirectionLeft, out detectionRay, rayLengthMeters) ||
        Physics.Raycast(myPosition, rayDirectionRight, out detectionRay, rayLengthMeters) ||
        Physics.Raycast(myPosition, rayDirectionBack, out detectionRay, rayLengthMeters)
        )
        {
            // Collider detected
            if (detectionRay.collider.gameObject.name == "RegionEnd")
            {
                // Indicator true
                regionEndState = true;
                GameObject triggeredGO = detectionRay.collider.gameObject.GetComponentInParent<Transform>().gameObject;

                if (triggeredRegionEndIDs.IndexOf(triggeredGO.GetInstanceID()) == -1)
                {
                    triggeredRegionEndIDs.Add(triggeredGO.GetInstanceID());
                    triggeredRegionEnds.Add(triggeredGO);
                }
                // Activate each shader concerned to show the alert mode
                detectionRay.collider.gameObject.GetComponent<RegionEndIdentity>().TransitionToAlert();
            }
        }
        else
        {
            if (regionEndState == true)
            {
                // deactivate the alert mode for each collider in the array
                foreach (GameObject regionEnd in triggeredRegionEnds)
                {
                    regionEnd.transform.gameObject.GetComponent<RegionEndIdentity>().TransitionAlertOver();
                }

                triggeredRegionEnds.Clear();
                triggeredRegionEndIDs.Clear();
                regionEndState = false;
            }
        }
    }
}