using UnityEngine;

public class AudioVisualiserUIToggle : MonoBehaviour
{
    void Start()
    {
        if (StartMenuManager.ColourState)
        {
            transform.GetChild(3).transform.gameObject.SetActive(true);
        }
    }

}
