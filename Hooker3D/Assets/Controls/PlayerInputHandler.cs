using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private Spaceship spaceship;
    [SerializeField] private PlayerInput playerInput;

    private void Start()
    {
        spaceship = FindObjectOfType<Spaceship>();
        
    }

    public void OnGrapple(InputAction.CallbackContext context)
    {
        if (context.performed) spaceship.OnGrapple(playerInput.playerIndex,true);
        else if(context.canceled) spaceship.OnGrapple(playerInput.playerIndex,false);
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed) spaceship.OnShoot(playerInput.playerIndex,true);
        else if(context.canceled) spaceship.OnShoot(playerInput.playerIndex,false);
    }

    
}
