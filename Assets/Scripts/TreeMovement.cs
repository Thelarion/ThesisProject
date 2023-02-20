using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMovement : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (other.transform.tag == "Tree")
        {
            other.transform.GetComponent<TreeOscillation>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Tree")
        {
            other.transform.GetComponent<TreeOscillation>().enabled = false;
        }
    }
}