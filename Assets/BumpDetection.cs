using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpDetection : MonoBehaviour
{
    public AK.Wwise.Event StoneBumpEvent;
    public AK.Wwise.Event TreeBumpEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Stone")
        {
            print("Stone");
            StoneBumpEvent.Post(other.transform.gameObject);
        }
        else if (other.transform.tag == "Tree")
        {
            TreeBumpEvent.Post(other.transform.gameObject);
        }
    }
}
