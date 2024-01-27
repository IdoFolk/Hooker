using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerSpawner : MonoSingleton<PlayerSpawner>
{
    [SerializeField] private Transform spawnPos;
    [SerializeField] Spaceship spaceshipPrefab;

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    // Start is called before the first frame update
    private Spaceship _activeSpaceship;

    public Spaceship ActiveSpaceship => _activeSpaceship;

    void Start()
    {
        
        SpawnPlayer(spawnPos.position);
    }

    public Spaceship SpawnPlayer(Vector3 position)
    {
        if(_activeSpaceship is not null) Destroy( _activeSpaceship.gameObject);
        _activeSpaceship = Instantiate(spaceshipPrefab, position, Quaternion.identity, transform);
        cinemachineVirtualCamera.Follow = _activeSpaceship.gameObject.transform;
        var rigidbody2D = _activeSpaceship.GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(new Vector2(-20f,0),ForceMode2D.Impulse);
        return _activeSpaceship;
    }

    public void DestroyPlayer()
    {
        if(_activeSpaceship is null) return;
        Destroy( _activeSpaceship.gameObject);
    }
}
