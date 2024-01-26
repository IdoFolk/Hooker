using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRotation : MonoBehaviour
{
    [SerializeField] List<DistanceJoint2D> _joints;
    [SerializeField] bool _isGrappleActive;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float _minAngularVelocity;
    [SerializeField] bool _isRightDirection;
    [SerializeField] float _rotationMultiplier;
    [SerializeField] float _positionOffset;
    Vector3 _lastPosition;
    // Update is called once per frame
    private bool CheckAngularVelocity(float a,float b)
    {
        if (a < b || a >-b)
        {
            return true;
        }
        return false;
    }
    private void RotateBall()
    {
        if (_isRightDirection)
        {
            _rb.AddTorque(-1 * _rotationMultiplier);
        }
        else
        {
            _rb.AddTorque(+1 * _rotationMultiplier);
        }
    }
    private void CheckDirection()
    {
        if (transform.localPosition.x - _lastPosition.x < 0)
        {
            _isRightDirection = false;
        }
        else
        {
            _isRightDirection = true;
        }
        _lastPosition = transform.position;
    }
    private bool _areGrapplesActive()
    {
        foreach (var joint in _joints)
        {
            if (joint.enabled)
                return true;
        }
        return false;
    }
    void FixedUpdate()
    {
        _isGrappleActive = _areGrapplesActive();
        if (!_isGrappleActive && CheckAngularVelocity(_rb.angularVelocity, _minAngularVelocity))
        {
            CheckDirection();
            RotateBall();
        }
        else
        {
            _rb.angularVelocity = 0;
        }
    }
}
