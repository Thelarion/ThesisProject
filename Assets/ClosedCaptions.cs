using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosedCaptions : MonoBehaviour
{
    private GameObject captions;
    private GameObject ccBackground;
    private bool semaphore = false;
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
        if (!semaphore && startMelodyDone)
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

        yield return new WaitForSeconds(3f);

        captions.SetActive(false);
        ccBackground.SetActive(false);

        yield return new WaitForSeconds(10f);
        semaphore = false;
    }
}
