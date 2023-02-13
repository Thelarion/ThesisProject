using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using Cinemachine;
using UnityEngine.InputSystem;

public class Inclusion : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlayerFollowCamera;
    public GameObject PlayerFollowCameraNoiseOff;
    public GameObject PlayerCameraRoot;
    public GameObject TargetParent;

    private void Start()
    {
        if (StartMenuManager.InclusionState == true)
        {
            InclusionIO();
        }
    }

    private void InclusionIO()
    {
        // Lock Camera, deactivate jump and adjust rotation speed
        FirstPersonController FPC = Player.transform.GetComponent<FirstPersonController>();
        FPC.TopClamp = -5f;         // Lock the camera horizontally
        FPC.BottomClamp = -5f;      // Lock the camera horizontally
        FPC.JumpHeight = 0f;        // Deactivate Jump
        FPC.RotationSpeed = 0.4f;   // Slow down rotation speed for arrow keys mostly

        // Switch the current action map from "Player" to "PlayerInclusion"
        PlayerInput PI = Player.transform.GetComponent<PlayerInput>();
        PI.SwitchCurrentActionMap("PlayerInclusion");
        print(PI.currentActionMap);

        // Fix camera rotation without skip
        PlayerCameraRoot.transform.localRotation = Quaternion.Euler(-5.0f, 0.0f, 0.0f);

        // Deactivate the breathing effect on the CinemachineVirtualCam
        PlayerFollowCamera.SetActive(false);
        PlayerFollowCameraNoiseOff.SetActive(true);

        // Adjust behaviour of the targets
        TargetController TP = TargetParent.transform.GetComponent<TargetController>();
        TP.ToggleInclusionTargets();
        TP.ResetNotePositionToSpawnPoint();
    }
}
