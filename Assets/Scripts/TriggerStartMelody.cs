using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStartMelody : MonoBehaviour
{
    public Animator FirstMelodyTilt;
    public Animator SecondMelodyTilt;
    public Animator ThirdMelodyTilt;
    public Animator FourthMelodyTilt;

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
        SecondMelodyTilt.SetTrigger("FirstDaffodil");
        Melody_versions.Post(SecondMelodyTilt.transform.gameObject);
        yield return new WaitForSeconds(5f);
        // StartCoroutine(ThirdTrigger());
    }

    // IEnumerator ThirdTrigger()
    // {
    //     print("Third");
    //     ThirdMelodyTilt.SetTrigger("ThirdBush");
    //     Melody_versions.Post(ThirdMelodyTilt.transform.gameObject);
    //     yield return new WaitForSeconds(5f);
    //     StartCoroutine(FourthTrigger());
    // }

    // IEnumerator FourthTrigger()
    // {
    //     print("Fourth");
    //     FourthMelodyTilt.SetTrigger("FourthBush");
    //     Melody_versions.Post(FourthMelodyTilt.transform.gameObject);
    //     yield return new WaitForSeconds(0f);
    // }
}
