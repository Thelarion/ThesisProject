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
    public GameObject CinemachineCameraTarget;
    public GameObject TargetParent;
    public bool IO = false;

    private void Update()
    {
        if (IO) { InclusionIO(); }
    }

    public void InclusionIO()
    {
        FirstPersonController FPC = Player.transform.GetComponent<FirstPersonController>();
        FPC.TopClamp = -5f;         // Lock the camera horizontally
        FPC.BottomClamp = -5f;      // Lock the camera horizontally
        FPC.JumpHeight = 0f;        // Deactivate Jump
        FPC.RotationSpeed = 0.6f;   // Slow down rotation speed for arrow keys mostly

        // Switch the current action map from "Player" to "PlayerInclusion"
        PlayerInput PI = Player.transform.GetComponent<PlayerInput>();
        PI.SwitchCurrentActionMap("PlayerInclusion");
        print(PI.currentActionMap);

        // Fix camera rotation without skip
        CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(-5.0f, 0.0f, 0.0f);

        // Deactivate the breathing effect on the CinemachineVirtualCam
        PlayerFollowCamera.SetActive(false);
        PlayerFollowCameraNoiseOff.SetActive(true);

        TargetParent TP = TargetParent.transform.GetComponent<TargetParent>();
        TP.ToggleInclusionTargets();
        TP.StopMovement();
        TP.ResetNotePositionToSpawnPoint();
        TP.InitializeMovement();

        IO = false;
    }

    // 
    // set targets on fixed height
    // 
    // script toggle on/off
}
