using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStartMelody : MonoBehaviour
{
    public Animator FirstMelodyTilt;
    public Animator SecondMelodyTilt;

    public AK.Wwise.Event Melody_versions;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayMelody());
    }

    IEnumerator DelayMelody()
    {
        yield return new WaitForSeconds(5f);
        StartCoroutine(FirstTrigger());
    }

    IEnumerator FirstTrigger()
    {
        print("First");
        FirstMelodyTilt.SetTrigger("FirstBush");
        Melody_versions.Post(FirstMelodyTilt.transform.gameObject);
        yield return new WaitForSeconds(5f);
        StartCoroutine(SecondTrigger());
    }


    IEnumerator SecondTrigger()
    {
        print("Second");
        SecondMelodyTilt.SetTrigger("SecondBush");
        Melody_versions.Post(SecondMelodyTilt.transform.gameObject);
        yield return new WaitForSeconds(5f);
    }
}
