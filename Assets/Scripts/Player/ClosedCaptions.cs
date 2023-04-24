using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Details: Closed Captions
// Logic for placing text for noises and sounds on screen

public class ClosedCaptions : MonoBehaviour
{
    private GameObject captions;
    private GameObject ccBackground;
    private bool semaphore = false;
    private bool semaphoreTop = false;
    private bool startMelodyDone = false;

    // Get the gameObjects of the text element and the transparent background
    private void Start()
    {
        captions = transform.GetChild(1).gameObject;
        ccBackground = transform.GetChild(0).gameObject;
    }


    // Delay the CC together with the Direction Indicator for about 5 seconds
    public bool StartMelodyDone
    {
        get => startMelodyDone; set { Invoke("DelayFirstCC", 5f); }
    }
    public void DelayFirstCC()
    {
        startMelodyDone = true;
    }

    // Display the caption when ready
    public void DisplayCaptions(string text)
    {
        // if start melody is completed, no sempahore or and not other captions block:
        // -> Start the intervall again
        if (!semaphore && !semaphoreTop && startMelodyDone)
        {
            StartCoroutine(DisplayCC(text));
        }
    }

    // Show CC when DisplayCCTop is not proritized
    // DisplayCCTop has higher importance and is preferred
    // Hence, it is made sure that DisplayCC does not turn of the CC 
    // while DisplayCCTop still displays information
    IEnumerator DisplayCC(string text)
    {
        semaphore = true;
        // Activate the gameObject to show CC
        captions.SetActive(true);
        ccBackground.SetActive(true);

        // Set text accordingly
        Text cc = captions.GetComponent<Text>();
        cc.text = text;

        yield return new WaitForSeconds(5f);

        // Do not turn off if DisplayCCTop is still active
        if (!semaphoreTop)
        {
            captions.SetActive(false);
            ccBackground.SetActive(false);
        }

        // Cooldown of 10 seconds
        yield return new WaitForSeconds(10f);
        semaphore = false;
    }

    // Logic for DisplayCCTop
    public void DisplayCaptionsTop(string text)
    {
        if (!semaphoreTop)
        {
            StartCoroutine(DisplayCCTop(text));
        }
    }

    // Same as DisplayCC but with higher priority
    IEnumerator DisplayCCTop(string text)
    {
        semaphoreTop = true;
        captions.SetActive(true);
        ccBackground.SetActive(true);

        Text cc = captions.GetComponent<Text>();
        cc.text = text;

        yield return new WaitForSeconds(6f);

        // Set elements false
        captions.SetActive(false);
        ccBackground.SetActive(false);

        semaphoreTop = false;
    }

    // Only applicable in practice mode
    // Additional information that it ended
    public void DisplayCaptionsPracticeEnd(string text)
    {
        captions.SetActive(true);
        ccBackground.SetActive(true);

        Text cc = captions.GetComponent<Text>();
        cc.text = text;
    }

    public void DeactivateCC()
    {
        captions.SetActive(false);
        ccBackground.SetActive(false);
    }
}
