using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOutline : MonoBehaviour
{
    private new Renderer renderer;
    private Material[] materials;

    void Awake()
    {
        renderer = GetComponent<Renderer>();
        materials = renderer.materials;
    }

    public void ActivateOutline(Material outline)
    {
        materials[1] = outline;
        renderer.materials = materials;
    }

    public void DeactivateOutline()
    {
        materials[1] = null;
        renderer.materials = materials;
    }
}
