using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LookManager : MonoBehaviour
{
    public Transform mPlayerTransform;

    [Tooltip("The direction towards which the compass points. Default for North is (0, 0, 1)")]
    public Vector3 kReferenceVector = new Vector3(0, 0, 1);

    // memalloc
    private Vector3 _mTempVector;
    public float _mTempAngle;
    private float roundedAngle;
    public AK.Wwise.Event Play_LookAngle;
    public AK.Wwise.Event Play_West;
    public AK.Wwise.Event Play_South;
    public AK.Wwise.Event Play_East;
    public AK.Wwise.Event Play_North;
    bool ignoreFirstState = false;
    private bool stopAudioCompassOnSuccess = false;

    // Update is called once per frame
    private void Update()
    {
        if (!stopAudioCompassOnSuccess)
        {
            CalculatePlayerLookAngle();
        }

    }

    public void DelayAudioCompassOnSucess()
    {
        StartCoroutine(ToggleIndicator());
    }

    IEnumerator ToggleIndicator()
    {
        stopAudioCompassOnSuccess = true;
        yield return new WaitForSeconds(10f);
        stopAudioCompassOnSuccess = false;
    }


    private void CalculatePlayerLookAngle()
    {
        // get player transform, set y to 0 and normalize
        _mTempVector = mPlayerTransform.forward;
        _mTempVector.y = 0f;
        _mTempVector = _mTempVector.normalized;

        // get distance to reference, ensure y equals 0 and normalize
        _mTempVector = _mTempVector - kReferenceVector;
        _mTempVector.y = 0;
        _mTempVector = _mTempVector.normalized;

        // if the distance between the two vectors is 0, this causes an issue with angle computation afterwards  
        if (_mTempVector == Vector3.zero)
        {
            _mTempVector = new Vector3(1, 0, 0);
        }

        // compute the rotation angle in radians and adjust it 
        _mTempAngle = Mathf.Atan2(_mTempVector.x, _mTempVector.z);
        _mTempAngle *= Mathf.Rad2Deg;
        _mTempAngle = _mTempAngle - 90f;


        // if (_mTempAngle < 0) _mTempAngle += 360f;

        if (_mTempAngle < 0)
        {
            // _mTempAngle = (_mTempAngle * (-1)) + 360f;
            _mTempAngle = (_mTempAngle) + 360f;

        }
        float newAngle = Mathf.Round(_mTempAngle);

        if (newAngle != 180 && (newAngle % 45 == 0 || newAngle % 22.5f == 0.5f) && newAngle != roundedAngle && ignoreFirstState)
        {
            roundedAngle = newAngle;
            // Play_LookAngle.Post(gameObject);

            switch (roundedAngle)
            {
                case 0:
                    Play_North.Post(gameObject);
                    LogManager.North++;
                    break;
                case 23:
                    Play_LookAngle.Post(gameObject);
                    break;
                case 45:
                    Play_East.Post(gameObject);
                    LogManager.East++;
                    break;
                case 68f:
                    Play_LookAngle.Post(gameObject);
                    break;
                case 90:
                    Play_South.Post(gameObject);
                    LogManager.South++;
                    break;
                case 113f:
                    Play_LookAngle.Post(gameObject);
                    break;
                case 135:
                    Play_West.Post(gameObject);
                    LogManager.West++;
                    break;
                case 158:
                    Play_LookAngle.Post(gameObject);
                    break;
            }
            // print(roundedAngle);
        }

        if (!ignoreFirstState && newAngle != 0f && newAngle != 180)
        {
            roundedAngle = newAngle;
            ignoreFirstState = true;
        }
    }
}