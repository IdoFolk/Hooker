using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private DistanceJoint2D distanceJoint2D;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float grappleDistance;
    [SerializeField] private Player player;
    [SerializeField, Range(0,1)] private float grapplePullDistanceRatio;
    [SerializeField] private float angularForce;
    


    private void Start()
    {
        distanceJoint2D.enabled = false;
    }

    private void Update()
    {
        int inputKey = 0;
        switch (player)
        {
            case Player.Player1:
                inputKey = 0;
                break;
            case Player.Player2:
                inputKey = 1;
                break;
        }
        if (Input.GetMouseButtonDown(inputKey))
        {
            Grapple();
        }
        else if (Input.GetMouseButtonUp(inputKey))
        {
            distanceJoint2D.enabled = false;
            lineRenderer.enabled = false;
        }

        if (distanceJoint2D.enabled)
        {
            lineRenderer.SetPosition(1,transform.position);
        }

       
    }

    private void Grapple()
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right,grappleDistance);
        if (hit.collider != null)
        {
            lineRenderer.SetPosition(0, hit.point);
            lineRenderer.SetPosition(1, transform.position);
            distanceJoint2D.connectedAnchor = hit.point;
            distanceJoint2D.distance = Vector3.Distance(hit.point, transform.position) * grapplePullDistanceRatio;
            rigidBody.angularDrag = angularForce;
            distanceJoint2D.enabled = true;
            lineRenderer.enabled = true;
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

public enum Player
{
    Player1,
    Player2
}
