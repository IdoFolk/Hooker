using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grappler : MonoBehaviour
{
    public bool IsGrappled => hook.IsGrappled;
    public Transform FirePoint => firePoint;
    public bool IsCharging => _isCharging;
    
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private DistanceJoint2D distanceJoint2D;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Hook hook;
    [SerializeField] private AudioSource audioSource;
    [Header("VFX")]
    [SerializeField] private ParticleSystem pulseCannonVFX;
    [SerializeField] private ParticleSystem grappleGunVFX;
    [SerializeField] private ParticleSystem grappleGunConsistentVFX;
    [SerializeField] private MeshRenderer spaceshipMesh;
    [Header("SFX")] 
    [SerializeField] private AudioClip pulseCannonBurstSfx;
    [SerializeField] private AudioClip grappleGunFireSfx;
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
    private static readonly int BlueIntensity = Shader.PropertyToID("_Blue_Intensity");
    private static readonly int OrangeIntensity = Shader.PropertyToID("_Orange_Intensity");
    private static int GrappableLayerMask;

    private void Start()
    {
        distanceJoint2D.enabled = false;
        GrappableLayerMask = LayerMask.GetMask("Grappable");
    }

    private void OnValidate()
    {
        audioSource ??= GetComponent<AudioSource>();
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
        audioSource.PlayOneShot(pulseCannonBurstSfx);
        _chargePercent = 0;
        if(_id == 0) spaceshipMesh.material.SetFloat(BlueIntensity,_chargePercent);
        else if(_id == 1) spaceshipMesh.material.SetFloat(OrangeIntensity,_chargePercent);
    }

    public void TryGrapple()
    {
        if (_isCharging) return;
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right, grappleDistance,GrappableLayerMask);
        if (hit.collider != null) StartCoroutine(EnableGrapple(hit));
    }

    public IEnumerator EnableGrapple(RaycastHit2D hit)
    {
        hook.LaunchHook(hit.point);
        GrappleVFX(true);
        yield return new WaitUntil(() => hook.IsGrappled);
        HookVFX();
        distanceJoint2D.enabled = true;
        distanceJoint2D.connectedAnchor = hook.GrappleTargetPoint;
        var distance = Vector3.Distance(transform.position, hook.GrappleTargetPoint);
        if (distance < grapplePullDistance) distanceJoint2D.distance = distance;
        else distanceJoint2D.distance = grapplePullDistance;
    }

    public void DisableGrapple()
    {
        hook.ReturnHook(firePoint.position);
        GrappleVFX(false);
        distanceJoint2D.enabled = false;
    }

    private void GrappleVFX(bool state)
    {
        if (state)
        {
            grappleGunVFX.Play();
            audioSource.PlayOneShot(grappleGunFireSfx);
            if(_id == 0) spaceshipMesh.material.SetFloat(BlueIntensity,1);
            else if(_id == 1) spaceshipMesh.material.SetFloat(OrangeIntensity,1);
        }
        else
        {
            hook.GrappleVFX(false);
            grappleGunConsistentVFX.gameObject.SetActive(false);
            if(_id == 0) spaceshipMesh.material.SetFloat(BlueIntensity,0);
            else if(_id == 1) spaceshipMesh.material.SetFloat(OrangeIntensity,0);
        }
    }

    private void HookVFX()
    {
        hook.GrappleVFX(true);
        grappleGunConsistentVFX.gameObject.SetActive(true);
    }
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            Gizmos.DrawWireSphere(transform.position, grappleDistance);
        }
    }
}