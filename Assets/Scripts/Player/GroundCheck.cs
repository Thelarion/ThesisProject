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
                    print(ground.transform.tag);
                    AkSoundEngine.SetSwitch("Materials", ground.transform.tag, gameObject);
                }
            }
    }
}
