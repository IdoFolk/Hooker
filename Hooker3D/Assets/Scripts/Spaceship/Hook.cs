using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private AudioSource audioSource;
    [Header("VFX")] 
    [SerializeField] private ParticleSystem grappleVFX;
    [Header("SFX")]
    [SerializeField] private AudioClip grappleHitSfx;

    [Header("Hook")]
    [SerializeField] private float hookSpeed;
    private Vector3 _grappleTargetPoint;
    private Vector3 _grappleSelfPoint;
    private bool _hookLaunched;
    private bool _isGrappled;
    private float _animationTime;

    private Grappler _grappler;
    public bool IsGrappled => _isGrappled;

    public Vector3 GrappleTargetPoint => _grappleTargetPoint;

    public bool HookLaunched => _hookLaunched;

    private void Start()
    {
        lineRenderer.enabled = false;
        _grappler ??= GetComponentInParent<Grappler>();
    }

    private void OnValidate()
    {
        audioSource ??= GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_hookLaunched)
        {
            _animationTime += Time.deltaTime * hookSpeed;
            float xPos = Mathf.Lerp(_grappleSelfPoint.x, _grappleTargetPoint.x, animationCurve.Evaluate(_animationTime));
            float yPos = Mathf.Lerp(_grappleSelfPoint.y, _grappleTargetPoint.y, animationCurve.Evaluate(_animationTime));
            transform.position = new Vector3(xPos, yPos, 0);
            lineRenderer.SetPosition(0, _grappler.FirePoint.position);
            lineRenderer.SetPosition(1, transform.position);
            if (_animationTime >= 1)
            {
                _isGrappled = true;
            }
        }
        else
        {
            _animationTime = 0;
        }
    }

    public void LaunchHook(Vector3 grapplePoint)
    {
        _grappleTargetPoint = grapplePoint;
        _grappleSelfPoint = transform.position;
        _hookLaunched = true;
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, _grappler.FirePoint.position);
        lineRenderer.SetPosition(1, transform.position);
    }

    public void ReturnHook(Vector3 returnPoint)
    {
        _hookLaunched = false;
        _isGrappled = false;
        transform.position = returnPoint;
        lineRenderer.enabled = false;
    }

    public void GrappleVFX(bool state)
    {
        grappleVFX.gameObject.SetActive(state);
        if (state) audioSource.PlayOneShot(grappleHitSfx);
    }
}