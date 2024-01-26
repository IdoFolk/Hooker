using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private DistanceJoint2D distanceJoint2D;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Hook hook;
    [SerializeField] private float grappleDistance;
    [SerializeField] private float grapplePullDistance;
    [SerializeField] private float angularForce;
    [SerializeField] private float grappleTime;
    public Transform FirePoint => firePoint;


    private void Start()
    {
        distanceJoint2D.enabled = false;
    }

    

    public void TryGrapple()
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right, grappleDistance);
        if (hit.collider != null) StartCoroutine(EnableGrapple(hit));
    }

    public IEnumerator EnableGrapple(RaycastHit2D hit)
    {
        hook.LaunchHook(hit.point);
        yield return new WaitUntil(() => hook.IsGrappled);
        distanceJoint2D.connectedAnchor = hit.point;
        distanceJoint2D.distance = grapplePullDistance;
        rigidBody.angularDrag = angularForce;
        distanceJoint2D.enabled = true;
    }

    public void DisableGrapple()
    {
        distanceJoint2D.enabled = false;
        hook.ReturnHook(firePoint.position);
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            Gizmos.DrawWireSphere(transform.position, grappleDistance);
        }
    }
}