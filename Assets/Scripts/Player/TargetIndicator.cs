using System.Collections;
using UnityEngine;

// Details: Target Indicator
// Red Arrow in screen space to accompany Closed Captions
// Child of Player GameObject

public class TargetIndicator : MonoBehaviour
{
    private Transform _target;
    private float rotationSpeed = 50f;
    private bool indicateState = false;
    private GameObject indicatorGO;
    private bool startMelodyDone = false;
    private bool semaphore = false;
    private bool stopIndicateTargetState = false;

    // IndicateTarget() is called on action Closed Caption when a noise appears
    public Transform Target { get => _target; set { _target = value; IndicateTarget(); } }

    // Delay the Indicator and call DelayFirstCC()
    public bool StartMelodyDone
    {
        get => startMelodyDone; set { Invoke("DelayFirstCC", 5f); }
    }

    // Get Indicator
    private void Start()
    {
        indicatorGO = transform.GetChild(0).gameObject;
    }

    // Delay for the Indicator to let the melody run through before showing anything
    public void DelayFirstCC()
    {
        startMelodyDone = true;
    }

    // Target Indicator is shown in a intervall defined in the Coroutine
    private void IndicateTarget()
    {
        // if start melody is completed, no sempahore or Target State block the application:
        // -> Start the intervall again
        if (!semaphore && startMelodyDone && !stopIndicateTargetState)
        {
            StartCoroutine(IndicateInterval());
        }
    }

    // The Indicator is shown for 5 seconds
    // It also has a cooldown of 10 seconds
    IEnumerator IndicateInterval()
    {
        semaphore = true;

        // Set the GameObject active
        indicatorGO.SetActive(true);
        // Set State true
        indicateState = true;
        yield return new WaitForSeconds(5f);
        // Deactivate the GameObject
        indicatorGO.SetActive(false);
        // Set State false
        indicateState = false;

        // Cooldown
        yield return new WaitForSeconds(10f);
        semaphore = false;

    }

    // Update to rotate alwaus to the target position
    // The closest target is passed in the method calling the Indicator
    private void Update()
    {
        // Check if the Indicator is shoen
        if (indicateState == true)
        {
            // Transform the rotation with a Slerp
            transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(_target.position - transform.position),
                rotationSpeed * Time.deltaTime);
        }
    }
}

