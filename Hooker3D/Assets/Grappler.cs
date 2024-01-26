using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float grappleDistance;

    [SerializeField]private SpringJoint2D springJoint2D;
    private bool _isGrappling;

    private void Start()
    {
        springJoint2D.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Grapple();
        }
        if (Input.GetMouseButtonUp(0))
        {
            _isGrappling = false;
        }

        if (_isGrappling)
        {
            springJoint2D.anchor = firePoint.position;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0,springJoint2D.anchor);
            lineRenderer.SetPosition(1,springJoint2D.connectedAnchor);
        }
    }

    private void Grapple()
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, Vector2.right);
        Debug.DrawRay(firePoint.position,Vector2.right);
        if (hit.collider != null)
        {
            springJoint2D.connectedAnchor = hit.point;
            springJoint2D.distance = 0;
            springJoint2D.enabled = true;
            _isGrappling = true;
        }
        
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            Gizmos.DrawWireSphere(transform.position,grappleDistance);
        }
        
    }
}
