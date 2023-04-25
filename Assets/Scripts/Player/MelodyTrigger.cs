using System.Collections;
using UnityEngine;

// Details: MelodyTrigger
// Supports the melody to illuminate the obstacles around the player

public class MelodyTrigger : MonoBehaviour
{
    public LayerMask layerMask;
    private float[] objectDistances;
    public AK.Wwise.Event Play_Melody_Seq;
    public AK.Wwise.Event Reset_Melody_Seq;

    // Determine the radius
    // Determine all objects around the player
    // Get the 5 closest obstacles
    // Play the event on those obstacles
    public void PlayMelodyRadius()
    {
        float radius = 30f;

        // Determine all objects around the player within the radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, layerMask);
        objectDistances = new float[hitColliders.Length];

        // If obstacles at all available
        if (hitColliders.Length != 0)
        {
            int x = 0;
            // Iterate through the array
            foreach (var hitCollider in hitColliders)
            {
                GameObject colliderGO = hitCollider.gameObject;
                float compareDistance = Vector3.Distance(transform.position, colliderGO.transform.position);
                objectDistances[x++] = compareDistance;
            }

            GameObject[] sortedGOs = new GameObject[hitColliders.Length];
            print(sortedGOs.Length);

            // Sort the obstacles according to their shortest distance
            for (int j = 0; j < objectDistances.Length; j++)
            {
                var min = Mathf.Infinity;
                var index = 0;

                for (int i = 0; i < objectDistances.Length; i++)
                {
                    if (objectDistances[i] < min)
                    {
                        min = objectDistances[i];
                        index = i;
                    }
                }

                sortedGOs[j] = hitColliders[index].gameObject;
                objectDistances[index] = Mathf.Infinity;
            }
            // Start playing the sequence
            StartCoroutine(PlayMelSeq(sortedGOs));
        }
        else
        {
            print("Play_Event: No object around");
        }



    }

    float waitSeconds;
    IEnumerator PlayMelSeq(GameObject[] sortedGOs)
    {
        int max_value;

        // Limit the sequence to the amount of notes in the melody (here 5)
        max_value = sortedGOs.Length > 5 ? 5 : sortedGOs.Length;

        for (int i = 0; i < max_value; i++)
        {
            // Play_Melody_Seq.Post(sortedGOs[i]);

            if (sortedGOs[i].tag == "Tree")
            {
                AkSoundEngine.PostEvent("Play_TreeIndication", sortedGOs[i]);
            }
            if (sortedGOs[i].tag == "Stone")
            {
                AkSoundEngine.PostEvent("Play_StoneIndication", sortedGOs[i]);
            }

            // Each intervall is according to their tonal interaval within the melody
            // Improvement: do it dynamically based on the input given by a user
            switch (i)
            {
                case 0:
                    waitSeconds = 0.563f;
                    break;
                case 1:
                    waitSeconds = 0.188f;
                    break;
                case 2:
                    waitSeconds = 0.375f;
                    break;
                case 3:
                    waitSeconds = 0.375f;
                    break;
                default:
                    break;
            }

            if (StartMenuManager.InclusionState)
            {
                waitSeconds = 1f;
            }


            yield return new WaitForSeconds(waitSeconds);
        }
        LogManager.MelodyCount++;
        Reset_Melody_Seq.Post(gameObject);
    }
}