using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrangleIdentication : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        StartCoroutine(PlayWrangleIdentication());
    }

    IEnumerator PlayWrangleIdentication()
    {
        float randInterval = Random.Range(30f, 50f);
        yield return new WaitForSeconds(randInterval);

        float distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.position);
        print(distanceToPlayer);
        if (distanceToPlayer >= 20f)
        {
            AkSoundEngine.PostEvent("Play_NoteWrangle", gameObject);
        }
        StartCoroutine(PlayWrangleIdentication());
    }
}
