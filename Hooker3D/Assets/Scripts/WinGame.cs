using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    [SerializeField] Canvas _canvas;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            RollCredits();
            var spaceship = collision.GetComponent<Spaceship>();
            spaceship.enabled = false;
        }
    }
    private void RollCredits()
    {
        _canvas.gameObject.SetActive(true);
    }
}
