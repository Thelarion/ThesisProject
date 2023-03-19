using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosedCaptions : MonoBehaviour
{
    private GameObject captions;
    private GameObject ccBackground;
    private bool semaphore = false;
    private bool semaphoreTop = false;
    private bool startMelodyDone = false;

    public bool StartMelodyDone
    {
        get => startMelodyDone; set { Invoke("DelayFirstCC", 5f); }
    }

    private void Start()
    {
        captions = transform.GetChild(1).gameObject;
        ccBackground = transform.GetChild(0).gameObject;
    }

    public void DelayFirstCC()
    {
        startMelodyDone = true;
    }


    public void DisplayCaptions(string text)
    {
        if (!semaphore && !semaphoreTop && startMelodyDone)
        {
            StartCoroutine(DisplayCC(text));
        }
    }

    IEnumerator DisplayCC(string text)
    {
        semaphore = true;
        captions.SetActive(true);
        ccBackground.SetActive(true);

        Text cc = captions.GetComponent<Text>();
        cc.text = text;

        yield return new WaitForSeconds(5f);

        if (!semaphoreTop)
        {
            captions.SetActive(false);
            ccBackground.SetActive(false);
        }


        yield return new WaitForSeconds(10f);
        semaphore = false;
    }

    public void DisplayCaptionsTop(string text)
    {
        if (!semaphoreTop)
        {
            StartCoroutine(DisplayCCTop(text));
        }
    }

    IEnumerator DisplayCCTop(string text)
    {
        semaphoreTop = true;
        captions.SetActive(true);
        ccBackground.SetActive(true);

        Text cc = captions.GetComponent<Text>();
        cc.text = text;

        yield return new WaitForSeconds(6f);

        captions.SetActive(false);
        ccBackground.SetActive(false);

        semaphoreTop = false;
    }

    public void DisplayCaptionsPracticeEnd(string text)
    {
        captions.SetActive(true);
        ccBackground.SetActive(true);

        Text cc = captions.GetComponent<Text>();
        cc.text = text;
    }
}
