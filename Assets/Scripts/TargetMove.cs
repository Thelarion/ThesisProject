using System;
using System.Collections;
using UnityEngine;

public class TargetMove : MonoBehaviour
{
    float movementFactor;
    [SerializeField] float period = 2f;
    // [SerializeField] 
    public Vector3 movementVector;
    private Vector3 startingPosition;
    private bool positionIsInitialized = false;
    private bool interpolate = false;
    Vector3 offset;
    float randomXPrev = 0f;
    float randomYPrev = 0f;

    private void Start()
    {
        StartCoroutine(WaitForPositionInitialization());
    }


    IEnumerator WaitForPositionInitialization()
    {
        yield return new WaitForSeconds(0.5f);
        startingPosition = transform.position;
        positionIsInitialized = true;
    }

    void Update()
    {
        if (positionIsInitialized)
        {
            OscillateMovement();
        }
    }

    private void OscillateMovement()
    {
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);
        float interpolateX = 0f;
        float interpolateY = 0f;

        if (!interpolate)
        {
            StartCoroutine(InterpolateRandomMovementX(rawSinWave, interpolateX, interpolateY));
        }
        movementFactor = (rawSinWave + 1f) / 2f;
        offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }

    IEnumerator InterpolateRandomMovementX(float rawSinWave, float interpolateX, float interpolateY)
    {
        interpolate = true;

        float randomXNew = UnityEngine.Random.Range(-3f, 4f);
        float randomYNew = UnityEngine.Random.Range(0.2f, 1f);

        // randomXNew = CheckUnwantedAccelerationX(randomXNew);

        float t = 0f;
        float duration = 3f;
        while (t < duration)
        {
            interpolateX = Mathf.Lerp(randomXPrev, randomXNew, t / duration);
            interpolateY = Mathf.Lerp(randomYPrev, randomYNew, t / duration);
            t += Time.deltaTime;
            movementVector = new Vector3(interpolateX, interpolateY, 0f);
            yield return null;
        }

        randomXPrev = randomXNew;
        randomYPrev = randomYNew;
        interpolate = false;
    }

    private float CheckUnwantedAccelerationX(float randomXNew)
    {
        if (randomXNew > 0 && (randomXNew - randomXPrev) > 1)
        {
            randomXNew = randomXNew - 1;
        }
        if (randomXNew < 0 && (randomXPrev - randomXNew) > 1)
        {
            randomXNew = randomXNew + 1;
        }

        return randomXNew;
    }
}
