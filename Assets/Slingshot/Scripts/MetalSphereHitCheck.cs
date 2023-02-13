using UnityEngine;

public class MetalSphereHitCheck : MonoBehaviour
{
    public AK.Wwise.Event PlingSuccess;
    public AK.Wwise.Event PlingFail;

    [HideInInspector] public TargetSpawnPoints targetSpawnPoints;

    private void Start()
    {
        targetSpawnPoints = GameObject.Find("SpawnPoints").GetComponent<TargetSpawnPoints>();
    }

    private void OnCollisionEnter(Collision other)
    {
        // Check the target state: does it play the correct note?
        // bool tonePlaysCorrectNote = other.transform.GetComponent<RunInterval>().TapState;

        // Case when the tone plays the correct note
        if (other.transform.tag == "Target" && other.transform.GetComponent<RunInterval>().TapState)
        {
            Destroy(other.transform.gameObject);
            PlingSuccess.Post(gameObject);
        }

        // Case when the tone plays the wrong note
        if (other.transform.tag == "Target" && !other.transform.GetComponent<RunInterval>().TapState)
        {
            other.transform.GetComponent<TargetMove>().StopMovementWhenMissOrInclusion();

            int indexInSequence = other.transform.GetComponent<TargetIdentity>().getIndexInSequence();
            other.transform.position = targetSpawnPoints.ReturnRandomSpawnTransform(indexInSequence).position;
            PlingFail.Post(gameObject);

            other.transform.GetComponent<TargetMove>().InitializeMovementAfterMissOrInclusion();
        }

        // Destroy the sphere if it hits a target
        if (other.transform.tag == "Target")
        {
            Destroy(transform.gameObject);
            // Destroy(other.transform.gameObject);
        }
    }
}
