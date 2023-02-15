using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderController : MonoBehaviour
{
    public AK.Wwise.Event Play_VolumeCheck;

    public void OnAudioMenuVolumeSliderChanged(float value)
    {
        AkSoundEngine.SetSwitch("VolumeCheck", "AudioMenu", gameObject);
        AkSoundEngine.SetRTPCValue("RTPC_AudioMenuVolumeSettings", value);
        Play_VolumeCheck.Post(gameObject);
    }
    public void OnMusicVolumeSliderChanged(float value)
    {
        AkSoundEngine.SetSwitch("VolumeCheck", "Music", gameObject);
        AkSoundEngine.SetRTPCValue("RTPC_MusicVolumeSettings", value);
        Play_VolumeCheck.Post(gameObject);
    }

    public void OnEffectsVolumeSliderChanged(float value)
    {
        AkSoundEngine.SetSwitch("VolumeCheck", "Effects", gameObject);
        AkSoundEngine.SetRTPCValue("RTPC_EffectsVolumeSettings", value);
        Play_VolumeCheck.Post(gameObject);
    }
}



