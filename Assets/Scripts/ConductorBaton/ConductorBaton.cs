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
    public float launchVelocity = 1000f;

    private void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }
    public void TriggerConductor()
    {
        m_Animator.ResetTrigger("OnConduct");
        m_Animator.SetTrigger("OnConduct");
        Debug.Log("Animation called");
    }

    public void FireConductor()
    {
        conductSphere = Instantiate(ConductSpherePrefab, ConductSphereSpawn.position, ConductSphereSpawn.rotation);

        conductSphere.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, launchVelocity * 2, 0));

        Destroy(conductSphere, 3f);
    }
}
