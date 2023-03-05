using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyTrigger : MonoBehaviour
{
    public LayerMask layerMask;
    private float[] objectDistances;
    public AK.Wwise.Event Play_Melody_Seq;
    public AK.Wwise.Event Reset_Melody_Seq;

    public void PlayMelodyRadius()
    {
        float radius = 30f;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, layerMask);
        objectDistances = new float[hitColliders.Length];

        if (hitColliders.Length != 0)
        {
            int x = 0;
            foreach (var hitCollider in hitColliders)
            {
                GameObject colliderGO = hitCollider.gameObject;
                float compareDistance = Vector3.Distance(transform.position, colliderGO.transform.position);
                objectDistances[x++] = compareDistance;
            }

            GameObject[] sortedGOs = new GameObject[hitColliders.Length];
            print(sortedGOs.Length);

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

        max_value = sortedGOs.Length > 5 ? 5 : sortedGOs.Length;

        for (int i = 0; i < max_value; i++)
        {
            Play_Melody_Seq.Post(sortedGOs[i]);



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

        Reset_Melody_Seq.Post(gameObject);
    }
}