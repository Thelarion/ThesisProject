using UnityEngine;

// StepGrassFlowers Details:
// Trigger Detection when the character moves through flowers

public class StepGrassFlowers : MonoBehaviour
{
    // Triffer Detection
    private void OnTriggerEnter(Collider other)
    {
        // Play Wwise Event
        AkSoundEngine.PostEvent("Play_Step_GrassFlowers", gameObject);
    }
}
