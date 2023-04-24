using UnityEngine;

// Details: CheckOcclusion
// High cut the note sound when an obstacle is in between the player and the note

public class CheckOcclusion : MonoBehaviour
{
    public GameObject player;
    private RaycastHit playerOcclusionCheck;
    public LayerMask layerMask;

    // Update is called once per frame
    void Update()
    {
        CheckIfPlayerBehindObstacle();
    }

    // Shoot a straight line, check with a layerMask
    private void CheckIfPlayerBehindObstacle()
    {
        if (Physics.Linecast(transform.position, player.transform.position, out playerOcclusionCheck, layerMask))
        {
            // Fade in the high cut
            AkSoundEngine.SetRTPCValue("RTPC_Obstruction", 1, gameObject);
        }
        else
        {
            // Fade out the high cut
            AkSoundEngine.SetRTPCValue("RTPC_Obstruction", 0, gameObject);
        }
    }
}
