using System;
using System.Collections;
using UnityEngine;

public class TargetMove : MonoBehaviour
{
    float movementFactor;
    [SerializeField] float period = 2f;
    public Vector3 movementVector;
    private Vector3 startingPosition;
    private bool positionIsSet = false;
    public bool InclusionIO = false;
    private bool interpolate = false;
    Vector3 offset;
    float randomXPrev = 0f;
    float randomYPrev = 0f;
    float randomZPrev = 0f;

    private void Awake()
    {
        StartCoroutine(WaitForPositionInitialization());
    }

    public void StopMovementWhenMissOrInclusion()
    {
        positionIsSet = false;
    }

    public void InitializeMovementAfterMissOrInclusion()
    {
        startingPosition = transform.position;
        positionIsSet = true;
    }

    IEnumerator WaitForPositionInitialization()
    {
        yield return new WaitForSeconds(0.2f);
        startingPosition = transform.position;
        positionIsSet = true;
    }

    void Update()
    {
        if (positionIsSet) { OscillateMovement(); }
    }

    private void OscillateMovement()
    {
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);
        float interpolateX = 0f;
        float interpolateY = 0f;
        float interpolateZ = 0f;

        if (!interpolate)
        {
            if (!InclusionIO)
            {
                StartCoroutine(InterpolateRandomMovementXYZ(rawSinWave, interpolateX, interpolateY, interpolateZ));
            }
            else
            {
                StartCoroutine(InterpolateRandomMovementXZ(rawSinWave, interpolateX, interpolateZ));
            }
        }
        movementFactor = (rawSinWave + 1f) / 2f;
        offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
    IEnumerator InterpolateRandomMovementXZ(float rawSinWave, float interpolateX, float interpolateZ)
    {
        interpolate = true;

        float randomXNew = UnityEngine.Random.Range(-3f, 4f);
        float randomZNew = UnityEngine.Random.Range(-2f, 3f);

        // randomXNew = CheckUnwantedAccelerationX(randomXNew);

        float t = 0f;
        float duration = 5f;
        while (t < duration)
        {
            interpolateX = Mathf.Lerp(randomXPrev, randomXNew, t / duration);
            interpolateZ = Mathf.Lerp(randomZPrev, randomZNew, t / duration);
            t += Time.deltaTime;
            movementVector = new Vector3(interpolateX, 0.0f, interpolateZ);
            yield return null;
        }

        randomXPrev = randomXNew;
        randomZPrev = randomZNew;

        interpolate = false;
    }
    IEnumerator InterpolateRandomMovementXYZ(float rawSinWave, float interpolateX, float interpolateY, float interpolateZ)
    {
        interpolate = true;

        float randomXNew = UnityEngine.Random.Range(-3f, 4f);
        float randomYNew = UnityEngine.Random.Range(0.2f, 2f);
        float randomZNew = UnityEngine.Random.Range(-2f, 3f);

        // randomXNew = CheckUnwantedAccelerationX(randomXNew);

        float t = 0f;
        float duration = 5f;
        while (t < duration && !InclusionIO)
        {
            interpolateX = Mathf.Lerp(randomXPrev, randomXNew, t / duration);
            interpolateY = Mathf.Lerp(randomYPrev, randomYNew, t / duration);
            interpolateZ = Mathf.Lerp(randomZPrev, randomZNew, t / duration);
            t += Time.deltaTime;
            if (!InclusionIO) { movementVector = new Vector3(interpolateX, interpolateY, interpolateZ); }
            yield return null;
        }

        randomXPrev = randomXNew;
        randomYPrev = randomYNew;
        randomZPrev = randomZNew;

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
