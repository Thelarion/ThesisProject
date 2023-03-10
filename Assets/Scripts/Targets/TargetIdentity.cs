using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Put this component on your enemy prefabs / objects
public class TargetIdentity : MonoBehaviour
{
    private static int targetCount;
    private int _indexInSequence;
    private bool _tapState;
    [SerializeField] private int missedTaps = 0;
    private static OperationController operationController;
    [HideInInspector] public TargetSpawnPoints targetSpawnPoints;

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
        gameObject.transform.position = targetSpawnPoints.ReturnRandomSpawnTransform(_indexInSequence).position;
        AdjustSizeAndVoice();
        // GameObject.Find("TargetIndicator").GetComponent<TargetIndicator>().Target = gameObject.transform;
    }

    private void AdjustSizeAndVoice()
    {
        if (StartMenuManager.InclusionState == true)
        {
            transform.localScale = new Vector3(70f, 70f, 70f);
            AkSoundEngine.SetRTPCValue("RTPC_MelodyVoice", 1f);
        }
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

        if (_tapState && operationController != null)
        {
            operationController.ActivateFrameSuccess(_indexInSequence, transform.name);
            operationController.DelayDistanceFrameCoroutine(4.5f);
        }


        if (transform.GetComponentInParent<TargetController>().TargetCount > 1)
        {
            AkSoundEngine.PostEvent("Play_M1_Success_Announce", GameObject.Find("Player"));
            Transform nextTone = transform.GetComponentInParent<TargetController>().ActivateNextTone();
            GetComponentInParent<SuccessEvent>().PlaySuccessEvent(this.transform);
            DistanceToTarget.CurrentTargetIdentity = nextTone.transform.GetComponent<TargetIdentity>();
        }



        _instances.Remove(this);
    }
}