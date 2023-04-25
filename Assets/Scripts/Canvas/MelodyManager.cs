using UnityEngine;

// Details: MelodyManager
// Interface for administrator the set up melodies for levels

public class MelodyManager : MonoBehaviour
{
    public AK.Wwise.Event Melody;
    bool isSoundPlaying;

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
