using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TargetIndicator : MonoBehaviour
{
    private Transform _target;
    private float rotationSpeed = 50f;
    private bool indicateState = false;
    private GameObject indicatorGO;

    public Transform Target { get => _target; set { _target = value; IndicateTarget(); } }

    private void Start()
    {
        indicatorGO = transform.GetChild(0).gameObject;
    }


    private void IndicateTarget()
    {
        StartCoroutine(IndicateInterval());
    }

    IEnumerator IndicateInterval()
    {
        indicatorGO.SetActive(true);
        indicateState = true;
        yield return new WaitForSeconds(2f);
        indicatorGO.SetActive(false);
        indicateState = false;

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

