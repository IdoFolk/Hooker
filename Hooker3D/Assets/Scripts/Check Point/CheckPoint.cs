using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    static CheckPoint _lastcheckPoint;
    public Transform SpawnPoint;
    bool _hasBeenActivated = false;
    [SerializeField] private GameObject _wall;
    [SerializeField] private List<ParticleSystem> _activateParticles;
    [SerializeField] private List<ParticleSystem> _idleParticles;
    private void Start()
    {
        foreach (var particle in _idleParticles)
        {
            particle.Play();
        }
        foreach (var particle in _activateParticles)
        {
            particle.Stop();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            _lastcheckPoint = this;
            if (!_hasBeenActivated)
            {
                var spaceship = collision.gameObject.GetComponent<Spaceship>();
                if (spaceship)
                {
                    var spaceshipParticles = spaceship.BallCheckpointParticles;
                    if (spaceshipParticles)
                    {
                        spaceshipParticles.Play();
                        _hasBeenActivated = true;
                    }
                }
            }

            //Debug.Log("Spawn Point is set to" + this.transform.parent.name);
            foreach (var particle in _activateParticles)
            {
                particle.Play();
            }
            foreach (var particle in _idleParticles)
            {
                particle.Stop();
            }
            _wall.SetActive(true);
        }
    }
    public void OverridePlayerCheckpoint(CheckPoint checkPoint)
    {
        _lastcheckPoint = checkPoint;
    }
    public static void SpawnAtLastSpawnPoint(GameObject ball)
    {
        ball.transform.position = _lastcheckPoint.SpawnPoint.position;
        _lastcheckPoint._hasBeenActivated = false;
    }
}
