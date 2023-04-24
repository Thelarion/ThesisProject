using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Details: MuteAudio
// Provides the functionallity in the Unity Editor to mute in gamemode

public class MuteAudio : MonoBehaviour
{
#if UNITY_EDITOR
    AkAudioListener _akAudioListener;

    void Awake()
    {
        _akAudioListener = Object.FindObjectOfType<AkAudioListener>();
    }

    void Update()
    {
        _akAudioListener.enabled = !EditorUtility.audioMasterMute;
    }
#endif
}
