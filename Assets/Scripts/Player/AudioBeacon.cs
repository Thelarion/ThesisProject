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
    public AK.Wwise.Event destinationReached;
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

        // Artificially increase the distance by some units
        // distanceToBeacon -= 5f;

        float roundedDistance = Mathf.Round(distanceToBeacon);
        print(roundedDistance);
        bool tenthFound = ((roundedDistance / 10) % 1) == 0;

        int roundedDistanceDiv10 = (Mathf.RoundToInt(roundedDistance / 10));    // Divide the distance by 10
        print(roundedDistanceDiv10);
        if (tenthFound)
        {
            if (roundedDistance != semaphoreDistance)
            {
                semaphoreDistance = roundedDistance;                // Make sure not playing when moving back and forth within a tenth

                int Div10Index = roundedDistanceDiv10 - 1;          // lowered by 1, as (10 / 10) = 1, but need element 0

                // print(roundedDistanceDiv10);                        // Distance as index

                if (roundedDistanceDiv10 > 0 && roundedDistanceDiv10 < 10) // Safety, as for now only 0 - 9 exists
                {
                    distanceCounts[Div10Index].Post(gameObject);
                }
            }
        }

        if (roundedDistance < 4 && _beaconActive)
        {
            destinationReached.Post(gameObject);
            _beaconActive = false;
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

        if (directionFace > 0.99f)
        {
            // Debug.Log("FRONT");
            AkSoundEngine.SetRTPCValue("RTPC_beacon_lr_volume", 0);

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
        AkSoundEngine.SetRTPCValue("RTPC_left_right", _panning);
        AkSoundEngine.SetRTPCValue("RTPC_beacon_lr_volume", 1);
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
