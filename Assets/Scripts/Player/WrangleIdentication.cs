using System.Collections;
using UnityEngine;

// Details: WrangleIdentication
// Play audio cue directly on the note in intervals

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
        // Start the coroutine
        StartCoroutine(PlayWrangleIdentication());
    }

    IEnumerator PlayWrangleIdentication()
    {
        // Determine the intervall between 10 and 20 seconds
        float randInterval = Random.Range(10f, 20f);

        yield return new WaitForSeconds(randInterval);

        float distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.position);

        // Play the indicator only if player is more than 60 units away from target
        if (distanceToPlayer >= 60f)
        {
            AkSoundEngine.PostEvent("Play_NoteWrangle", gameObject);
            // CC
            if (StartMenuManager.ColourState)
            {
                targetIndicator.Target = gameObject.transform;
                closedCaptions.DisplayCaptions("A forest inhabitant got into trouble with a Tone!");
            }
        }
        StartCoroutine(PlayWrangleIdentication());
    }
}
