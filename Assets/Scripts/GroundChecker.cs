using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField]
    private float checkSphereRadius = 5;
    [SerializeField]
    private LayerMask groundLayer;

    public bool isGrounded { get; set; }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Physics.CheckSphere(transform.position, checkSphereRadius, groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, checkSphereRadius);
    }
}
