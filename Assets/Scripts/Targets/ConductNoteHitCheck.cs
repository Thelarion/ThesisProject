using System;
using UnityEngine;

public class ConductNoteHitCheck : MonoBehaviour
{
    private ScoreManager scoreManager;
    public ParticleSystem hitParticles;


    [HideInInspector] public TargetSpawnPoints targetSpawnPoints;

    private void Start()
    {
        targetSpawnPoints = GameObject.Find("SpawnPoints").GetComponent<TargetSpawnPoints>();
        scoreManager = GameObject.Find("ScoreSystem").GetComponent<ScoreManager>();
    }

    private void OnCollisionEnter(Collision other)
    {
        // CASE CORRECT NOTE
        if (other.transform.tag == "Target" && other.transform.GetComponent<RunInterval>().TapState)
        {
            if (!StartMenuManager.InclusionState)
            {
                AkSoundEngine.PostEvent("Play_PlingSuccess", GameObject.Find("Player"));
            }

            scoreManager.CalculatePoints(other.transform.GetComponent<TargetIdentity>().MissedTaps);
            Destroy(other.transform.gameObject);
        }

        // CASE WRONG NOTE
        if (other.transform.tag == "Target" && !other.transform.GetComponent<RunInterval>().TapState)
        {

            other.transform.GetComponent<TargetIdentity>().MissedTaps++;
            TargetMove targetMove = other.transform.GetComponent<TargetMove>();

            targetMove.StopMovementWhenMissOrInclusion();

            int indexInSequence = other.transform.GetComponent<TargetIdentity>().getIndexInSequence();

            other.transform.position = targetSpawnPoints.ReturnRandomSpawnTransform(indexInSequence).position;

            AkSoundEngine.PostEvent("Play_Wrong_Note", GameObject.Find("Player"));

            targetMove.InitializeMovementAfterMissOrInclusion();

        }

        // // DESTROY SPHERE CASE TARGET
        // if (other.transform.tag == "Target") // Destroy the sphere if it hits a target
        // {
        Instantiate(hitParticles, transform.position, Quaternion.identity);
        Destroy(transform.gameObject); // Destroy(other.transform.gameObject);
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Environment")
        {
            Instantiate(hitParticles, transform.position, Quaternion.identity);
            Destroy(transform.gameObject);
        }
    }
}
