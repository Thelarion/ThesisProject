using System.Collections.Generic;
using UnityEngine;

// Details: TargetIdentity
// Separate information class for each target

// Put this component on your enemy prefabs / objects
public class TargetIdentity : MonoBehaviour
{
    private static int targetCount;
    private int _indexInSequence;
    private bool _tapState;
    [SerializeField] private int missedTaps = 0;
    private static OperationController operationController;
    [HideInInspector] public TargetSpawnPoints targetSpawnPoints;
    private ClosedCaptions closedCaptions;

    // every instance registers to and removes itself from here
    private static readonly HashSet<TargetIdentity> _instances = new HashSet<TargetIdentity>();

    // Readonly property, I would return a new HashSet so nobody on the outside can alter the content
    public static HashSet<TargetIdentity> Instances => new HashSet<TargetIdentity>(_instances);

    public int MissedTaps { get => missedTaps; set => missedTaps = value; }

    // Add target to instances
    private void Awake()
    {
        _instances.Add(this);
        operationController = GameObject.Find("List").GetComponent<OperationController>();
        targetSpawnPoints = GameObject.Find("SpawnPoints").GetComponent<TargetSpawnPoints>();
    }

    private void Start()
    {
        closedCaptions = GameObject.Find("ClosedCaptions").GetComponent<ClosedCaptions>();
        gameObject.transform.position = targetSpawnPoints.ReturnRandomSpawnTransform(_indexInSequence).position;
        AdjustSizeAndVoice();
    }

    // Adjust details about the note based on the chosen inclusion mode
    private void AdjustSizeAndVoice()
    {
        // If inclusion mode, increase the 3D size
        // Increase the collider size
        if (StartMenuManager.InclusionState == true)
        {
            transform.localScale = new Vector3(100f, 100f, 100f);
            transform.GetComponent<BoxCollider>().size = new Vector3(0.06f, 0.08f, 0.05f);
            AkSoundEngine.SetRTPCValue("RTPC_MelodyVoice", 1f);
        }
        // If not inclusion mode, set it back to the usual values, which is smaller
        else if (StartMenuManager.InclusionState == false)
        {
            transform.localScale = new Vector3(20f, 20f, 20f);
            AkSoundEngine.SetRTPCValue("RTPC_MelodyVoice", 0f);
        }
    }

    private void Update()
    {
        _tapState = GetComponent<RunInterval>().TapState;
    }

    public void setIndexInSequence(int index)
    {
        _indexInSequence = index;
    }

    public int getIndexInSequence()
    {
        return _indexInSequence;
    }

    // Remove target from instances when destroyed
    private void OnDestroy()
    {
        // Active the success frame
        if (_tapState && operationController != null)
        {
            operationController.ActivateFrameSuccess(_indexInSequence, transform.name);
            operationController.DelayDistanceFrameCoroutine(4.5f);
        }

        // While this is not the last note, play success announcement and continue
        if (transform.GetComponentInParent<TargetController>().TargetCount > 1)
        {
            if (StartMenuManager.InclusionState)
            {
                AkSoundEngine.PostEvent("Play_M1_Success_Announce", GameObject.Find("Player"));
                if (StartMenuManager.ColourState)
                {
                    closedCaptions.DisplayCaptionsTop("Great, that is the correct note! On to the next.");
                }
            }
            // Activate a new note
            Transform nextTone = transform.GetComponentInParent<TargetController>().ActivateNextTone();
            // Play success event
            GetComponentInParent<SuccessEvent>().PlaySuccessEvent(this.transform);
            DistanceToTarget.CurrentTargetIdentity = nextTone.transform.GetComponent<TargetIdentity>();
        }



        _instances.Remove(this);
    }
}