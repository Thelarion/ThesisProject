using UnityEngine;

// Details: RegionEndIdentity
// Shader transition

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
        shaderMaterial.SetFloat("Vector1_8ec0b323bcf242c6b66b93c8c9a3b7bc", 0);
    }

    public void TransitionToAlert()
    {
        shaderMaterial.SetFloat("Vector1_8ec0b323bcf242c6b66b93c8c9a3b7bc", 1);
    }
}
