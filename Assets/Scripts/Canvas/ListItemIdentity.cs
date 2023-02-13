using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListItemIdentity : MonoBehaviour
{
    [SerializeField] private bool lockState; // field
    [SerializeField] private string toneName; // field
    [SerializeField] private string octave; // field
    [SerializeField] private string accidental; // field

    [HideInInspector]
    public bool LockState   // property
    {
        get { return lockState; }
        set { lockState = value; }
    }

    [HideInInspector]
    public string ToneName   // property
    {
        get { return toneName; }
        set { toneName = value; }
    }

    [HideInInspector]
    public string Octave   // property
    {
        get { return octave; }
        set { octave = value; }
    }

    [HideInInspector]
    public string Accidental   // property
    {
        get { return accidental; }
        set { accidental = value; }
    }
}
