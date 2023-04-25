using UnityEngine;

// Details: ConductorBaton
// Animate, trigger and fire the note

public class ConductorBaton : MonoBehaviour
{
    Animator m_Animator;
    public GameObject ConductSpherePrefab;
    private GameObject conductSphere;
    public Transform ConductSphereSpawn;
    private Rigidbody rb;
    public float launchVelocity = 1200f;
    private string currentTarget;
    private void Start()
    {
        // Get animation
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Set up the animation to shoot the note
    public void TriggerConductor()
    {
        // Reset animation
        m_Animator.ResetTrigger("OnConduct");
        // Start animation
        m_Animator.SetTrigger("OnConduct");
    }

    private void Update()
    {
        if (DistanceToTarget.CurrentTargetIdentity != null)
        {
            currentTarget = DistanceToTarget.CurrentTargetIdentity.gameObject.name;
        }
    }

    public void FireConductor()
    {
        AkSoundEngine.PostEvent(currentTarget, gameObject);

        // Instatiate
        conductSphere = Instantiate(ConductSpherePrefab, ConductSphereSpawn.position, ConductSphereSpawn.rotation);

        // Shoot straight
        conductSphere.transform.localRotation = Quaternion.identity;

        // Add force and launch
        conductSphere.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, launchVelocity * 3, 0));

        // Destroy after 10 sec
        Destroy(conductSphere, 10f);
    }
}
