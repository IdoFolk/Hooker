using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float hookSpeed;
    private Vector3 _grappleTargetPoint;
    private Vector3 _grappleSelfPoint;
    private bool _hookLaunched;
    private bool _isGrappled;
    private float _animationTime;

    private Grappler _grappler;
    public bool IsGrappled => _isGrappled;

    private void Start()
    {
        lineRenderer.enabled = false;
        _grappler ??= GetComponentInParent<Grappler>();
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
    }

    public void ReturnHook(Vector3 returnPoint)
    {
        _hookLaunched = false;
        _isGrappled = false;
        transform.position = returnPoint;
        lineRenderer.enabled = false;
    }
}