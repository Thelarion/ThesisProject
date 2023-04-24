using UnityEngine;

// Details: ConductNoteHitCheck
// Placed on the Note shot from the conductors baton
// Triggers collision events

public class ConductNoteHitCheck : MonoBehaviour
{
    private ScoreManager scoreManager;
    public ParticleSystem hitParticles;
    private ClosedCaptions closedCaptions;
    [HideInInspector] public TargetSpawnPoints targetSpawnPoints;

    private void Start()
    {
        closedCaptions = GameObject.Find("ClosedCaptions").GetComponent<ClosedCaptions>();
        targetSpawnPoints = GameObject.Find("SpawnPoints").GetComponent<TargetSpawnPoints>();
        scoreManager = GameObject.Find("ScoreSystem").GetComponent<ScoreManager>();
    }

    private void OnCollisionEnter(Collision other)
    {
        // CASE CORRECT NOTE
        if (other.transform.tag == "Target" && other.transform.GetComponent<RunInterval>().TapState)
        {
            // If not in inclusion state, play the success Wwise event
            if (!StartMenuManager.InclusionState)
            {
                AkSoundEngine.PostEvent("Play_PlingSuccess", GameObject.Find("Player"));
            }

            // Add calculated points to the score board
            scoreManager.CalculatePoints(other.transform.GetComponent<TargetIdentity>().MissedTaps);
            // Destroy the object (note)
            Destroy(other.transform.gameObject);
        }

        // CASE WRONG NOTE
        if (other.transform.tag == "Target" && !other.transform.GetComponent<RunInterval>().TapState)
        {
            // Add to the missed taps
            other.transform.GetComponent<TargetIdentity>().MissedTaps++;
            // Logging
            LogManager.MissedTaps++;
            // Get the object to move it next
            TargetMove targetMove = other.transform.GetComponent<TargetMove>();

            // Make sure that the object is not moving while changing the position
            targetMove.StopMovementWhenMissOrInclusion();

            // Get the index of the sequence
            int indexInSequence = other.transform.GetComponent<TargetIdentity>().getIndexInSequence();

            // Get a random spawn point (1 of 3 possible) and save the position
            other.transform.position = targetSpawnPoints.ReturnRandomSpawnTransform(indexInSequence).position;

            // Play wrong note Wwise event
            AkSoundEngine.PostEvent("Play_Wrong_Note", GameObject.Find("Player"));

            // For DHH people, display Subtitles
            if (StartMenuManager.ColourState)
            {
                closedCaptions.DisplayCaptionsTop("That was the wrong note. The note moved but is still nearby!");
            }

            // Initialize the movement
            targetMove.InitializeMovementAfterMissOrInclusion();
        }

        // Instantiate particle effects on position the emitted note hits the ground or an object
        Instantiate(hitParticles, transform.position, Quaternion.identity);
        // If nothing hit, destroy the shot note
        Destroy(transform.gameObject);

    }

    // Destroy the emitted note when an object tagged environment is hit
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Environment")
        {
            Instantiate(hitParticles, transform.position, Quaternion.identity);
            Destroy(transform.gameObject);
        }
    }
}
