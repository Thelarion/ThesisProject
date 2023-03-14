using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    private static bool volumeInitialized = false;
    public AK.Wwise.Event Play_VolumeCheck;
    public AK.Wwise.Event Stop_VolumeCheck;
    public AK.Wwise.RTPC RTPC_MusicVolumeSettings;

    private static float musicRTPCValue;
    private static float voiceRTPCValue;
    private static float effectsRTPCValue;

    public static float MusicRTPCValue { get => musicRTPCValue; set => musicRTPCValue = value; }
    public static float VoiceRTPCValue { get => voiceRTPCValue; set => voiceRTPCValue = value; }
    public static float EffectsRTPCValue { get => effectsRTPCValue; set => effectsRTPCValue = value; }
    public static bool VolumeInitialized { get => volumeInitialized; set => volumeInitialized = value; }

    public Slider MusicSliderStartSettings;
    public Slider EffectsSliderStartSettings;
    public Slider VoiceSliderStartSettings;

    public Slider MusicSliderInGameSettings;
    public Slider EffectsSliderInGameSettings;
    public Slider VoiceSliderInGameSettings;

    float initializeValue = 5f;
    private bool ignoreSliderChange = false;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (!VolumeInitialized)
        {
            RTPC_MusicVolumeSettings.SetGlobalValue(initializeValue);
            MusicRTPCValue = initializeValue;

            AkSoundEngine.SetRTPCValue("RTPC_VoiceVolumeSettings", initializeValue);
            VoiceRTPCValue = initializeValue;

            AkSoundEngine.SetRTPCValue("RTPC_EffectsVolumeSettings", initializeValue);
            EffectsRTPCValue = initializeValue;

            VolumeInitialized = true;
        }
        else
        {
            ignoreSliderChange = true;

            MusicSliderStartSettings.value = MusicRTPCValue;
            VoiceSliderStartSettings.value = VoiceRTPCValue;
            EffectsSliderStartSettings.value = EffectsRTPCValue;

            MusicSliderInGameSettings.value = MusicRTPCValue;
            VoiceSliderInGameSettings.value = VoiceRTPCValue;
            EffectsSliderInGameSettings.value = EffectsRTPCValue;

            ignoreSliderChange = false;
        }
    }

    private void PostEvents()
    {
        Stop_VolumeCheck.Post(gameObject);
        Play_VolumeCheck.Post(gameObject);
        // AkSoundEngine.UnregisterGameObj(gameObject);
    }
    public void OnVoiceSliderChanged(float value)
    {
        if (!ignoreSliderChange)
        {
            VoiceRTPCValue = value;
            AkSoundEngine.SetSwitch("VolumeCheck", "Voice", gameObject);
            AkSoundEngine.SetRTPCValue("RTPC_VoiceVolumeSettings", VoiceRTPCValue);
            LogManager.VoiceVolume = value;
            PostEvents();
        }
    }


    public void OnMusicVolumeSliderChanged(float value)
    {
        if (!ignoreSliderChange)
        {
            MusicRTPCValue = value;
            AkSoundEngine.SetSwitch("VolumeCheck", "Music", gameObject);
            RTPC_MusicVolumeSettings.SetGlobalValue(MusicRTPCValue);
            LogManager.MusicVolume = value;
            PostEvents();
        }
    }

    public void OnEffectsVolumeSliderChanged(float value)
    {
        if (!ignoreSliderChange)
        {
            EffectsRTPCValue = value;
            AkSoundEngine.SetSwitch("VolumeCheck", "Effects", gameObject);
            AkSoundEngine.SetRTPCValue("RTPC_EffectsVolumeSettings", EffectsRTPCValue);
            LogManager.EffectsVolume = value;
            PostEvents();
        }
    }
}



