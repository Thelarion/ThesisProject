using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderController : MonoBehaviour
{
    public AK.Wwise.Event Play_VolumeCheck;
    public AK.Wwise.Event Stop_VolumeCheck;

    private void Start()
    {
        AkSoundEngine.SetRTPCValue("RTPC_MusicVolumeSettings", 5);
        AkSoundEngine.SetRTPCValue("RTPC_VoiceVolumeSettings", 5);
        AkSoundEngine.SetRTPCValue("RTPC_EffectsVolumeSettings", 5);
    }

    private void PostEvents()
    {
        Stop_VolumeCheck.Post(gameObject);
        Play_VolumeCheck.Post(gameObject);
    }
    public void OnVoiceSliderChanged(float value)
    {
        AkSoundEngine.SetSwitch("VolumeCheck", "Voice", gameObject);
        AkSoundEngine.SetRTPCValue("RTPC_VoiceVolumeSettings", value);
        PostEvents();
    }


    public void OnMusicVolumeSliderChanged(float value)
    {
        AkSoundEngine.SetSwitch("VolumeCheck", "Music", gameObject);
        AkSoundEngine.SetRTPCValue("RTPC_MusicVolumeSettings", value);
        PostEvents();
    }

    public void OnEffectsVolumeSliderChanged(float value)
    {
        AkSoundEngine.SetSwitch("VolumeCheck", "Effects", gameObject);
        AkSoundEngine.SetRTPCValue("RTPC_EffectsVolumeSettings", value);
        PostEvents();
    }
}



