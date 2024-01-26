using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Grappler player1GrappleGun;
    [SerializeField] private Grappler player2GrappleGun;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            player1GrappleGun.TryGrapple();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            player1GrappleGun.DisableGrapple();
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            player2GrappleGun.TryGrapple();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            player2GrappleGun.DisableGrapple();
        }
    }
}
