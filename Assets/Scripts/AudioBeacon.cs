using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBeacon : MonoBehaviour
{
    public GameObject targetBeacon;
    public AK.Wwise.Event BeaconLR;
    public AK.Wwise.Event BeaconLock;
    private float _pan;
    public bool StartBeacon = false;
    private bool _beaconActive = false;
    private bool _beaconLock_hasPlayed = false;

    private void Start()
    {
        AngleDir();
    }

    private void Update()
    {
        ToggleBeaconIO();

        if (_beaconActive)
        {
            AngleDir();
        }
    }

    private void ToggleBeaconIO()
    {
        if (StartBeacon)
        {
            if (!_beaconActive)
            {
                BeaconLR.Post(transform.gameObject);
                _beaconActive = true;
            }
            else
            {
                BeaconLR.Stop(transform.gameObject);
                _beaconActive = false;
            }
            StartBeacon = false;
        }
    }

    private bool myApproximation(float a, float b, float tolerance)
    {
        return (Mathf.Abs(a - b) < tolerance);
    }
    public void AngleDir()
    {
        var directionToEnemy = targetBeacon.transform.position - transform.position;
        var projectionOnRight = Vector3.Dot(directionToEnemy, transform.right);

        if (myApproximation(projectionOnRight, 0f, 0.5f))
        {
            Debug.Log("FRONT");
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

            // Reset the front locking sound
            // Also make sure beacon plays from the beginning when looking away
            if (_beaconLock_hasPlayed)
            {
                BeaconLR.Stop(transform.gameObject);
                BeaconLR.Post(transform.gameObject);
                _beaconLock_hasPlayed = false;
            }

            _pan = -1f;
            AkSoundEngine.SetRTPCValue("left_right", _pan);
            AkSoundEngine.SetRTPCValue("beacon_lr_volume", 1);
        }
        else if (projectionOnRight > 0)
        {
            // Debug.Log("RIGHT");

            // Reset the front locking sound
            // Also make sure beacon plays from the beginning when looking away
            if (_beaconLock_hasPlayed)
            {
                BeaconLR.Stop(transform.gameObject);
                BeaconLR.Post(transform.gameObject);
                _beaconLock_hasPlayed = false;
            }
            _pan = 1f;
            AkSoundEngine.SetRTPCValue("left_right", _pan);
            AkSoundEngine.SetRTPCValue("beacon_lr_volume", 1);

        }
    }
}
