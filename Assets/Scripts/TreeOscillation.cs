using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeOscillation : MonoBehaviour
{
    public Vector3 from;
    public Vector3 to;
    public float speed = 0.1f;
    public Vector3 originTransform;

    private void Start()
    {
        originTransform = transform.eulerAngles;
    }

    void OnEnable()
    {
        from = transform.eulerAngles;
        to = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 1f, transform.eulerAngles.z + 1f);
    }

    private void OnDisable()
    {
        transform.eulerAngles = new Vector3(originTransform.x, originTransform.y, originTransform.z);
    }

    void Update()
    {
        // 'speed' will be the number of full oscillations per second.
        float t = (Mathf.Sin(Time.time * speed * Mathf.PI * 2.0f) + 1.0f) / 2.0f;
        transform.eulerAngles = Vector3.Lerp(from, to, t);
    }
}
