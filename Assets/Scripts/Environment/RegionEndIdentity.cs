using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionEndIdentity : MonoBehaviour
{
    Material shaderMaterial;

    // Start is called before the first frame update
    void Start()
    {
        shaderMaterial = GetComponent<Renderer>().material;
    }

    public void TransitionAlertOver()
    {
        // targetValue = 0f;
        // if (value > targetValue)
        // {
        //     value -= 0.1f * Time.deltaTime;
        //     shaderMaterial.SetFloat("Vector1_8ec0b323bcf242c6b66b93c8c9a3b7bc", value);
        // }
        shaderMaterial.SetFloat("Vector1_8ec0b323bcf242c6b66b93c8c9a3b7bc", 0);
    }

    public void TransitionToAlert()
    {
        shaderMaterial.SetFloat("Vector1_8ec0b323bcf242c6b66b93c8c9a3b7bc", 1);
        // targetValue = 1f;
        // if (value < targetValue)
        // {
        //     value += 0.1f * (Time.deltaTime * 1.5f);
        //     shaderMaterial.SetFloat("Vector1_8ec0b323bcf242c6b66b93c8c9a3b7bc", value);
        // }
    }
}
