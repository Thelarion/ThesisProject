using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosedCaptions : MonoBehaviour
{
    private GameObject captions;
    private GameObject ccBackground;

    private void Start()
    {
        captions = transform.GetChild(1).gameObject;
        ccBackground = transform.GetChild(0).gameObject;
    }

    public void DisplayCaptions(string text)
    {
        StartCoroutine(DisplayCC(text));
    }

    IEnumerator DisplayCC(string text)
    {
        captions.SetActive(true);
        ccBackground.SetActive(true);

        Text cc = captions.GetComponent<Text>();
        cc.text = text;

        yield return new WaitForSeconds(2f);

        captions.SetActive(false);
        ccBackground.SetActive(false);
    }
}
