using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PusherObject : MonoBehaviour
{
    [SerializeField] bool _isPushed;
    Rigidbody2D _ballRB;
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    Vector2 _direction;
    [SerializeField] float _directionMultiplier;
    private void OnTriggerEnter2D(Collider2D collision)
    {
            Debug.Log("Ball?");
        if (collision != null)
        {
            _ballRB = collision.gameObject.GetComponent<Rigidbody2D>();
            Debug.Log("Ball");
            _isPushed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
            Debug.Log("NoBall?");
        if (collision != null)
        {
            Debug.Log("NoBall");
            _isPushed = false;
        }
    }
    private void Start()
    {
        var vectorA = new Vector2(pointA.position.x, pointA.position.y);
        var vectorB = new Vector2(pointB.position.x, pointB.position.y);
        _direction = vectorB - vectorA;
        _direction.Normalize();
    }
    // Update is called once per frame
    void Update()
    {
        if (_isPushed)
        {
            _ballRB.AddForce(_direction * _directionMultiplier,ForceMode2D.Impulse);
        }
    }
}
