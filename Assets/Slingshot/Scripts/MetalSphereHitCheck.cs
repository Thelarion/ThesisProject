using System;
using UnityEngine;

public class MetalSphereHitCheck : MonoBehaviour
{
    public AK.Wwise.Event PlingSuccess;
    public AK.Wwise.Event PlingFail;
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
            PlingSuccess.Post(gameObject);
            scoreManager.CalculatePoints(other.transform.GetComponent<TargetIdentity>().MissedTaps);
            // Destroy(other.transform.gameObject);
        }

        // CASE WRONG NOTE
        if (other.transform.tag == "Target" && !other.transform.GetComponent<RunInterval>().TapState)
        {

            other.transform.GetComponent<TargetIdentity>().MissedTaps++;
            TargetMove targetMove = other.transform.GetComponent<TargetMove>();

            targetMove.StopMovementWhenMissOrInclusion();

            int indexInSequence = other.transform.GetComponent<TargetIdentity>().getIndexInSequence();

            other.transform.position = targetSpawnPoints.ReturnRandomSpawnTransform(indexInSequence).position;

            PlingFail.Post(gameObject);

            targetMove.InitializeMovementAfterMissOrInclusion();

        }

        // // DESTROY SPHERE CASE TARGET
        // if (other.transform.tag == "Target") // Destroy the sphere if it hits a target
        // {
        Destroy(transform.gameObject); // Destroy(other.transform.gameObject);
        hitParticles.Play();
        // }
    }
}
