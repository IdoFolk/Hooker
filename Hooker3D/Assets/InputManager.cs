using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInputManager playerInputManager;
    private List<PlayerInput> _playerInputs = new List<PlayerInput>();

    private void Start()
    {
        playerInputManager.onPlayerJoined += OnDeviceChange;
    }
    
    private void OnDeviceChange(PlayerInput playerInput)
    {
        _playerInputs.Add(playerInput);
        
        // var gamepad = playerInput.GetDevice<Gamepad>();
        // var keyboard = playerInput.GetDevice<Keyboard>();
        // var mouse = playerInput.GetDevice<Mouse>();
        // if (gamepad is not null)
        // {
        //     playerInput.SwitchCurrentControlScheme("XboxControls");
        // }
        // else if (keyboard is not null)
        // {
        //     playerInput.SwitchCurrentControlScheme("Keyboard");
        // }
        // else if (mouse is not null)
        // {
        //     playerInput.SwitchCurrentControlScheme("Mouse");
        // }
    }
}
