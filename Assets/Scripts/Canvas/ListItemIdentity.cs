using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListItemIdentity : MonoBehaviour
{
    private bool lockState; // field
    private string toneName; // field
    [SerializeField] private string octave; // field

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
}
