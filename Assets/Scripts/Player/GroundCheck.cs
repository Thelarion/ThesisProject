using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Details: DistanceToTarget
// Determine the ground material and play corresponding Wwise event for footsteps

public class GroundCheck : MonoBehaviour
{

    public RaycastHit ground;
    public LayerMask layerMask;
    public Transform origin;
    public bool hit = false;
    private string currentHitObjectTag;

    private void Start()
    {
        // Start logging if not in practice mode 
        if (SceneManager.GetActiveScene().name != "PracticeMode")
        {
            LogManager.InitializeWriter();
            // Track the path of the player
            StartCoroutine(TrackCoordinates());
        }
    }

    // Track the path of the player
    IEnumerator TrackCoordinates()
    {
        // Every 2.5 seconds
        yield return new WaitForSeconds(2.5f);
        // Log
        LogManager.PrintCoordinates(transform.position.x, transform.position.z);
        // Repeat
        StartCoroutine(TrackCoordinates());
    }

    void Update()
    {
        // Raycast on the ground
        if (Physics.Raycast(origin.position, Vector3.down, out ground, 3f, layerMask))

            // Null check
            if (ground.transform != null)
            {
                // Determine if changed
                if (ground.transform.tag != currentHitObjectTag)
                {
                    hit = false;
                }

                // Set new ground material and set Wwise switch
                if (!hit)
                {
                    hit = true;
                    currentHitObjectTag = ground.transform.tag;
                    AkSoundEngine.SetSwitch("Materials", ground.transform.tag, gameObject);
                }
            }
    }
}
