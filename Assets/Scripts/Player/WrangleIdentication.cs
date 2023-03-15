using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrangleIdentication : MonoBehaviour
{
    private Transform player;
    private TargetIndicator targetIndicator;
    private ClosedCaptions closedCaptions;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        targetIndicator = GameObject.Find("TargetIndicator").GetComponent<TargetIndicator>();
        closedCaptions = GameObject.Find("ClosedCaptions").GetComponent<ClosedCaptions>();
        StartCoroutine(PlayWrangleIdentication());
    }

    IEnumerator PlayWrangleIdentication()
    {
        float randInterval = Random.Range(20f, 30f);
        yield return new WaitForSeconds(randInterval);

        float distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.position);

        if (distanceToPlayer >= 60f)
        {
            AkSoundEngine.PostEvent("Play_NoteWrangle", gameObject);
            if (StartMenuManager.ColourState)
            {
                targetIndicator.Target = gameObject.transform;
                closedCaptions.DisplayCaptions("A forest inhabitant got into trouble with a Tone!");
            }
        }
        StartCoroutine(PlayWrangleIdentication());
    }
}
