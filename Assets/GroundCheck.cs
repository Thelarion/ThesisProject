using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    public RaycastHit ground;
    public LayerMask layerMask;
    public Transform origin;
    public bool hit = false;
    private string currentHitObject;

    void Update()
    {
        if (Physics.Raycast(origin.position, Vector3.down, out ground, 3f, layerMask))

            if (ground.transform != null)
            {
                if (ground.transform.name != currentHitObject)
                {
                    hit = false;
                }

                if (!hit)
                {
                    hit = true;
                    currentHitObject = ground.transform.name;
                    print(ground.transform.name);
                    AkSoundEngine.SetSwitch("Materials", ground.transform.tag, gameObject);
                }
            }



    }
}
