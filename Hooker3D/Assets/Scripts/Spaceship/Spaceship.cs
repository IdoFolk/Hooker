using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spaceship : MonoBehaviour
{
    [SerializeField] private Grappler player1GrappleGun;
    [SerializeField] private Grappler player2GrappleGun;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private bool useXboxControllers;
    [SerializeField] private float angularDrag;
    [SerializeField] public ParticleSystem BallCheckpointParticles;
    [SerializeField] public AudioSource audioSource;
    [Header("SFX Clips")] 
    [SerializeField] private AudioClip deathSFX;
    [SerializeField] private AudioClip winSFX;

    private bool _grapplePressedPlayer1;
    private bool _grapplePressedPlayer2;
    private bool _shootPressedPlayer1;
    private bool _shootPressedPlayer2;

    public Grappler Player1GrappleGun => player1GrappleGun;
    public Grappler Player2GrappleGun => player2GrappleGun;

    private void OnValidate()
    {
        _rigidbody2D ??= GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rigidbody2D.angularDrag = angularDrag;
        player1GrappleGun.Init(0);
        player2GrappleGun.Init(1);
    }

    public void OnGrapple(int playerIndex, bool pressed)
    {
        if (pressed)
        {
            if(playerIndex == 0) player1GrappleGun.TryGrapple();
            else if(playerIndex == 1) player2GrappleGun.TryGrapple();
        }
        else
        {
            if(playerIndex == 0) player1GrappleGun.DisableGrapple();
            else if(playerIndex == 1) player2GrappleGun.DisableGrapple();
        }
    }
    public void OnShoot(int playerIndex, bool pressed)
    {
        if (pressed)
        {
            if(playerIndex == 0) player1GrappleGun.ChargeShot();
            else if(playerIndex == 1) player2GrappleGun.ChargeShot();
        }
        else
        {
            if(playerIndex == 0) player1GrappleGun.ReleaseShot();
            else if(playerIndex == 1) player2GrappleGun.ReleaseShot();
        }
    }
    public void WinSFX()
    {
        audioSource.PlayOneShot(winSFX);
    }
    public void DeathSFX()
    {
        audioSource.PlayOneShot(deathSFX);
    }
    private void CannonInput()
    {
        if (!useXboxControllers)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                player1GrappleGun.ChargeShot();
            }
            else if (Input.GetKeyUp(KeyCode.X))
            {
                player1GrappleGun.ReleaseShot();
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                player2GrappleGun.ChargeShot();
            }
            else if (Input.GetKeyUp(KeyCode.Z))
            {
                player2GrappleGun.ReleaseShot();
            }
        }
        else
        {
            if (!player1GrappleGun.IsCharging && !player1GrappleGun.IsGrappled && !_shootPressedPlayer1)
            {
                    if (Input.GetAxis("Player1Shoot") == 1)
                    {
                        _shootPressedPlayer1 = true;
                        player1GrappleGun.ChargeShot();
                    } 
            }
            else if (_shootPressedPlayer1)
            {
                if (Input.GetAxis("Player1Shoot") == 0)
                {
                    _shootPressedPlayer1 = false;
                    player1GrappleGun.ReleaseShot();
                }
            }
            
            if (!player2GrappleGun.IsCharging && !player2GrappleGun.IsGrappled && !_shootPressedPlayer2)
            {
                    if (Input.GetAxis("Player2Shoot") == 1)
                    {
                        _shootPressedPlayer2 = true;
                        player2GrappleGun.ChargeShot();
                    }
            }
            else if (_shootPressedPlayer2)
            {
                if (Input.GetAxis("Player2Shoot") == 0)
                {
                    _shootPressedPlayer2 = false;
                    player2GrappleGun.ReleaseShot();
                }
            }
        }
    }

    private void GrappleInput()
    {
        if (!useXboxControllers)
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
        else
        {
            if (!player1GrappleGun.IsGrappled && !_grapplePressedPlayer1)
            {
                if (Input.GetAxis("Player1Grapple") == 1)
                {
                    _grapplePressedPlayer1 = true;
                    player1GrappleGun.TryGrapple();
                }
            }
            else if(_grapplePressedPlayer1)
            {
                if (Input.GetAxis("Player1Grapple") == 0)
                {
                    _grapplePressedPlayer1 = false;
                    player1GrappleGun.DisableGrapple();
                }
            }

            if (!player2GrappleGun.IsGrappled && !_grapplePressedPlayer2)
            {
                if (Input.GetAxis("Player2Grapple") == 1)
                {
                    _grapplePressedPlayer2 = true;
                    player2GrappleGun.TryGrapple();
                }
            }
            else if(_grapplePressedPlayer2)
            {
                if (Input.GetAxis("Player2Grapple") == 0)
                {
                    _grapplePressedPlayer2 = false;
                    player2GrappleGun.DisableGrapple();
                }
            }
        }
    }
}