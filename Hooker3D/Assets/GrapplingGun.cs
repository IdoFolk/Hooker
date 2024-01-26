using System;
using Unity.VisualScripting;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    [SerializeField] private Rigidbody hookRigidbody;
    [SerializeField] private Rigidbody ballRigidbody;
    [SerializeField] private SpringJoint springJoint;
    [SerializeField] private Transform gunPoint;
    [SerializeField] private LineRenderer lineRenderer;


    private bool isGrappling;

    private void Start()
    {
        springJoint.connectedBody = null;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootGrapple();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            springJoint.connectedBody = null;
            isGrappling = false;
        }

        if (isGrappling)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0,springJoint.connectedAnchor);
            lineRenderer.SetPosition(1,springJoint.anchor);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    private void ShootGrapple()
    {
        if (Physics.Raycast(gunPoint.position,gunPoint.right,out var hit))
        {
            hookRigidbody.position = hit.point;
            springJoint.anchor = hit.point;
            springJoint.connectedBody = ballRigidbody;
            springJoint.connectedAnchor = Vector3.zero;
            isGrappling = true;
            // springJoint.minDistance = (gunPoint.position - hit.point).magnitude / 2;
            // springJoint.maxDistance = (gunPoint.position - hit.point).magnitude ;
        }
    }
}
