using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    bool ignoreFirstState = false;

    // Update is called once per frame
    private void Update()
    {
        if (StartMenuManager.InclusionState)
        {
            CalculatePlayerLookAngle();
        }

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
        // if (_mTempAngle < 0) _mTempAngle += 360f;

        if (_mTempAngle < 0) _mTempAngle *= (-1);

        float newAngle = Mathf.Round(_mTempAngle);

        if (newAngle % 30 == 0 && newAngle != roundedAngle && ignoreFirstState)
        {
            roundedAngle = newAngle;
            Play_LookAngle.Post(gameObject);
        }

        if (!ignoreFirstState)
        {
            roundedAngle = newAngle;
            ignoreFirstState = true;
        }
    }
}