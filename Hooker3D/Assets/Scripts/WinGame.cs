using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    [SerializeField] Canvas _canvas;
    GameObject _spaceship;
    bool _moveIntoHole;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            var spaceship = collision.GetComponent<Spaceship>();
            _spaceship = spaceship.gameObject;
            spaceship.enabled = false;
            _moveIntoHole = true;
            _spaceship.GetComponent<Rigidbody2D>().gravityScale = 0;
            StartCoroutine(Wait2Seconds());
        }
    }
    IEnumerator Wait2Seconds()
    {
        yield return new WaitForSeconds(2);
        RollCredits();
    }
    private void FixedUpdate()
    {
        if (_moveIntoHole)
        {
            Vector2 forcedirection = new Vector2(this.transform.position.x, this.transform.position.y) - new Vector2(_spaceship.transform.position.x, _spaceship.transform.position.y);
            _spaceship.GetComponent<Rigidbody2D>().AddForce(forcedirection*20);
        }
    }
    private void RollCredits()
    {
        _canvas.gameObject.SetActive(true);
    }
}
