using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    public Button Continue;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            AkSoundEngine.PostEvent("Play_Button_OnClick", gameObject);
            Continue.onClick.Invoke();
        }
    }
}
