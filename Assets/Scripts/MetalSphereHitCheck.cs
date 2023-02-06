using UnityEngine;

public class MetalSphereHitCheck : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {

        if (other.transform.tag == "Target" && other.transform.GetComponent<RunInterval>().TapState)
        {
            Destroy(other.transform.gameObject);
        }

        if (other.transform.tag == "Target")
        {
            Destroy(transform.gameObject);
        }
    }
}
