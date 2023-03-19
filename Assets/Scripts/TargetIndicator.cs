using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TargetIndicator : MonoBehaviour
{
    private Transform _target;
    private Transform _bells;
    private float rotationSpeed = 50f;
    private bool indicateState = false;
    private GameObject indicatorGO;
    private bool startMelodyDone = false;
    private bool semaphore = false;
    private bool stopIndicateTargetState = false;

    public Transform Target { get => _target; set { _target = value; IndicateTarget(); } }
    public Transform Bells { get => _target; set { _target = value; stopIndicateTargetState = true; IndicateBells(); } }


    public bool StartMelodyDone
    {
        get => startMelodyDone; set { Invoke("DelayFirstCC", 5f); }
    }

    private void Start()
    {
        indicatorGO = transform.GetChild(0).gameObject;
    }
    public void DelayFirstCC()
    {
        startMelodyDone = true;
    }


    private void IndicateTarget()
    {
        if (!semaphore && startMelodyDone && !stopIndicateTargetState)
        {
            StartCoroutine(IndicateInterval());
        }
    }

    IEnumerator IndicateInterval()
    {
        semaphore = true;

        indicatorGO.SetActive(true);
        indicateState = true;
        yield return new WaitForSeconds(3f);
        indicatorGO.SetActive(false);
        indicateState = false;

        yield return new WaitForSeconds(10f);
        semaphore = false;

    }

    private void IndicateBells()
    {
        indicateState = true;
        indicatorGO.SetActive(true);
    }

    private void Update()
    {
        if (indicateState == true)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(_target.position - transform.position),
                rotationSpeed * Time.deltaTime);
        }
    }
}

