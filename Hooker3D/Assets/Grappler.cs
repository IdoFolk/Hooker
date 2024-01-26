using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappler : MonoBehaviour
{
    public bool IsGrappled => hook.IsGrappled;
    public Transform FirePoint => firePoint;
    public bool IsCharging => _isCharging;
    
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private DistanceJoint2D distanceJoint2D;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Hook hook;
    [Header("VFX")]
    [SerializeField] private ParticleSystem pulseCannonVFX;
    [SerializeField] private ParticleSystem grappleGunVFX;
    [SerializeField] private MeshRenderer spaceshipMesh;
    [Header("Grapple Gun")]
    [SerializeField] private float grappleDistance;
    [SerializeField] private float grapplePullDistance;
    [SerializeField] private float grappleTime;
    [Header("Pulse Cannon")]
    [SerializeField] private float chargingSpeed;
    [SerializeField] private float pulsePower;
    
    private bool _isCharging;
    private int _id;


    private float _chargePercent;
    private static readonly int BlueIntensity = Shader.PropertyToID("Blue_Intensity");
    private static readonly int OrangeIntensity = Shader.PropertyToID("Orange_Intensity");

    private void Start()
    {
        distanceJoint2D.enabled = false;
    }

    public void Init(int id)
    {
        _id = id;
    }

    private void FixedUpdate()
    {
        if (_isCharging)
        {
            if (_chargePercent >= 1) return;
            _chargePercent += Time.fixedDeltaTime * chargingSpeed;
            if(_id == 0) spaceshipMesh.material.SetFloat(BlueIntensity,_chargePercent);
            else if(_id == 1) spaceshipMesh.material.SetFloat(OrangeIntensity,_chargePercent);
        }
    }

    public void ChargeShot()
    {
        if (hook.HookLaunched) return;
        _isCharging = true;
    }
    public void ReleaseShot()
    {
        if (hook.HookLaunched) return;
        _isCharging = false;
        var direction = (Vector3)rigidBody.position - firePoint.position;
        rigidBody.AddForce(direction*pulsePower*_chargePercent,ForceMode2D.Impulse);
        pulseCannonVFX.Play();
        _chargePercent = 0;
        if(_id == 0) spaceshipMesh.material.SetFloat(BlueIntensity,_chargePercent);
        else if(_id == 1) spaceshipMesh.material.SetFloat(OrangeIntensity,_chargePercent);
    }

    public void TryGrapple()
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right, grappleDistance);
        if (hit.collider != null) StartCoroutine(EnableGrapple(hit));
    }

    public IEnumerator EnableGrapple(RaycastHit2D hit)
    {
        hook.LaunchHook(hit.point);
        grappleGunVFX.Play();
        yield return new WaitUntil(() => hook.IsGrappled);
        distanceJoint2D.connectedAnchor = hit.point;
        var distance = Vector3.Distance(transform.position, hit.point);
        if (distance < grapplePullDistance) distanceJoint2D.distance = distance;
        else distanceJoint2D.distance = grapplePullDistance;
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