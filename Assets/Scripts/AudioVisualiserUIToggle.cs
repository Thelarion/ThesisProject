using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualiserUIToggle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (StartMenuManager.ColourState)
        {
            transform.GetChild(3).transform.gameObject.SetActive(true);
        }
    }

}
