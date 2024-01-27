using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    private static Spaceship _spaceship;

    private void Start()
    {
        AssignSpaceship(PlayerSpawner.Instance.ActiveSpaceship);
    }

    public static void AssignSpaceship(Spaceship spaceship)
    {
        _spaceship = spaceship;
    }

    public void OnGrapple(InputAction.CallbackContext context)
    {
        if (context.performed) _spaceship.OnGrapple(playerInput.playerIndex, true);
        else if (context.canceled) _spaceship.OnGrapple(playerInput.playerIndex, false);
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed) _spaceship.OnShoot(playerInput.playerIndex, true);
        else if (context.canceled) _spaceship.OnShoot(playerInput.playerIndex, false);
    }
}