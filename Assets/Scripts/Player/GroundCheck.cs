using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    public RaycastHit ground;
    public LayerMask layerMask;
    public Transform origin;
    public bool hit = false;
    private string currentHitObjectTag;

    private void Start()
    {
        LogManager.InitializeWriter();
        StartCoroutine(TrackCoordinates());
    }

    IEnumerator TrackCoordinates()
    {
        yield return new WaitForSeconds(5f);
        LogManager.PrintCoordinates(transform.position.x, transform.position.z);
        StartCoroutine(TrackCoordinates());
    }

    void Update()
    {
        if (Physics.Raycast(origin.position, Vector3.down, out ground, 3f, layerMask))

            if (ground.transform != null)
            {
                if (ground.transform.tag != currentHitObjectTag)
                {
                    hit = false;
                }

                if (!hit)
                {
                    hit = true;
                    currentHitObjectTag = ground.transform.tag;
                    AkSoundEngine.SetSwitch("Materials", ground.transform.tag, gameObject);
                }
            }
    }
}
