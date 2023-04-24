using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

// Details: Inclusion
// Sets up all important details regarding the inclusion mode

public class Inclusion : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlayerFollowCamera;
    public GameObject PlayerFollowCameraNoiseOff;
    public GameObject PlayerCameraRoot;
    public GameObject TargetParent;
    public bool InclusionQuickStart = false;
    public bool ColourQuickStart = false;

    // Check for the chosen mode or state
    private void Awake()
    {
        if (InclusionQuickStart)
        {
            StartMenuManager.InclusionState = true;
        }
        if (ColourQuickStart)
        {
            StartMenuManager.ColourState = true;
        }
    }

    private void Start()
    {
        // If inclusion state, set up the game in inclusion mode
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
        FPC.RotationSpeed = 0.6f;   // Slow down rotation speed for arrow keys mostly

        // Switch the current action map from "Player" to "PlayerInclusion"
        PlayerInput PI = Player.transform.GetComponent<PlayerInput>();
        PI.SwitchCurrentActionMap("PlayerInclusion");

        // Fix camera rotation without skip
        PlayerCameraRoot.transform.localRotation = Quaternion.Euler(-5.0f, 0.0f, 0.0f);

        // Deactivate the breathing effect on the CinemachineVirtualCam
        PlayerFollowCamera.SetActive(false);
        PlayerFollowCameraNoiseOff.SetActive(true);

        // Adjust behaviour of the targets
        TargetController TP = TargetParent.transform.GetComponent<TargetController>();
        TP.ToggleInclusionTargets();    // Set the inclusion mode flag in each single target
        // TP.ResetNotePositionToSpawnPoint();
        // TP.DeactivateMovement();
    }
}
