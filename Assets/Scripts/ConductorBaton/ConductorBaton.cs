using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        m_Animator = gameObject.GetComponent<Animator>();
    }
    public void TriggerConductor()
    {
        m_Animator.ResetTrigger("OnConduct");
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

        conductSphere = Instantiate(ConductSpherePrefab, ConductSphereSpawn.position, ConductSphereSpawn.rotation);

        conductSphere.transform.localRotation = Quaternion.identity;

        conductSphere.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, launchVelocity * 3, 0));

        Destroy(conductSphere, 10f);
    }
}
