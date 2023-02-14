using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderController : MonoBehaviour
{
    public AK.Wwise.Event Play_VolumeCheck;

    public void OnMusicVolumeSliderChanged(float value)
    {
        print(value.ToString());
        AkSoundEngine.SetRTPCValue("RTPC_MusicVolumeSettings", value);
        Play_VolumeCheck.Post(gameObject);
    }
}
