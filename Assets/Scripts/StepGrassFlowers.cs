using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepGrassFlowers : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        AkSoundEngine.PostEvent("Play_Step_GrassFlowers", gameObject);
    }
}
