using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyManager : MonoBehaviour
{
    public AK.Wwise.Event Melody;
    bool isSoundPlaying;

    private void Update()
    {
        // print(isSoundPlaying);
    }

    public void PlayMelody()
    {
        if (!isSoundPlaying)
        {
            Melody.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, CallBackFunction);
            isSoundPlaying = true;
        }
    }

    void CallBackFunction(object in_cookie, AkCallbackType callType, object in_info)
    {
        if (callType == AkCallbackType.AK_EndOfEvent)
        {
            isSoundPlaying = false;
        }
    }
}
