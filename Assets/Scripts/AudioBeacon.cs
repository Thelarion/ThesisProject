using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBeacon : MonoBehaviour
{
    public GameObject targetBeacon;
    public AK.Wwise.Event BeaconLR;
    public AK.Wwise.Event BeaconLock;
    public AK.Wwise.Event[] distanceCounts;
    private float _panning;
    private bool _beaconActive = false;
    private bool _beaconLock_hasPlayed = false;

    private void Start()
    {
        // AngleDir();
    }

    private void Update()
    {
        if (_beaconActive)
        {
            AngleDir();
            DistanceToBeacon();
        }
    }
    float semaphoreDistance;
    private void DistanceToBeacon()
    {
        float distanceToBeacon = Vector3.Distance(targetBeacon.transform.position, transform.position);

        float roundDistance = Mathf.Round(distanceToBeacon);
        bool tenthFound = ((roundDistance / 10) % 1) == 0;

        // if (roundDistance != semaphoreDistance)
        // {
        //     semaphoreDistance = Mathf.Infinity;
        // }

        if (tenthFound)
        {
            if (roundDistance != semaphoreDistance)
            {
                semaphoreDistance = roundDistance;
                print(roundDistance);

                int countIndex = Mathf.RoundToInt(roundDistance / 10);
                print(countIndex);
                if (countIndex > 0 && countIndex < 10)
                    distanceCounts[countIndex - 1].Post(gameObject);
            }
        }
    }

    public void ToggleBeaconOn()
    {
        BeaconLR.Post(transform.gameObject);
        _beaconActive = true;
    }

    public void ToggleBeaconOff()
    {
        BeaconLR.Stop(transform.gameObject);
        _beaconActive = false;
        _beaconLock_hasPlayed = false;
    }

    private bool myApproximation(float a, float b, float tolerance)
    {
        return (Mathf.Abs(a - b) < tolerance);
    }
    public void AngleDir()
    {
        var directionToEnemy = targetBeacon.transform.position - transform.position;
        var projectionOnRight = Vector3.Dot(directionToEnemy, transform.right);

        float directionFace = Vector3.Dot(transform.forward, (targetBeacon.transform.position - transform.position).normalized);


        // if (myApproximation(projectionOnRight, 0f, 0.5f))
        // {
        // }
        if (directionFace > 0.99f)
        {
            // Debug.Log("FRONT");
            AkSoundEngine.SetRTPCValue("beacon_lr_volume", 0);

            if (!_beaconLock_hasPlayed && _beaconActive)
            {
                BeaconLock.Post(gameObject);
                _beaconLock_hasPlayed = true;
            }
        }
        else if (projectionOnRight < 0)
        {
            // Debug.Log("LEFT");
            ResetLRAndLock();
            SetRTPCAndVolume(-1f);
        }
        else if (projectionOnRight > 0)
        {
            // Debug.Log("RIGHT");
            ResetLRAndLock();
            SetRTPCAndVolume(1f);
        }
    }

    private void SetRTPCAndVolume(float LR)
    {
        _panning = LR;
        AkSoundEngine.SetRTPCValue("left_right", _panning);
        AkSoundEngine.SetRTPCValue("beacon_lr_volume", 1);
    }

    private void ResetLRAndLock()
    {
        // Reset the front locking sound
        // Also make sure beacon plays from the beginning when looking away
        if (_beaconLock_hasPlayed)
        {
            BeaconLR.Stop(transform.gameObject);
            BeaconLR.Post(transform.gameObject);
            _beaconLock_hasPlayed = false;
        }
    }
}
