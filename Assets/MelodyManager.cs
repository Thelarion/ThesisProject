using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyManager : MonoBehaviour
{
    public AK.Wwise.Event Melody;

    public void PlayMelody()
    {
        Melody.Post(gameObject);
    }
}
