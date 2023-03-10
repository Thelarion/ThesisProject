using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpDetection : MonoBehaviour
{
    public AK.Wwise.Event StoneBumpEvent;
    public AK.Wwise.Event TreeBumpEvent;
    private bool playerFacesObstacle;

    public bool PlayerFacesObstacle { get => playerFacesObstacle; set => playerFacesObstacle = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Stone")
        {
            StoneBumpEvent.Post(other.transform.gameObject);
        }
        else if (other.transform.tag == "Tree")
        {
            TreeBumpEvent.Post(other.transform.gameObject);
        }
    }
}
