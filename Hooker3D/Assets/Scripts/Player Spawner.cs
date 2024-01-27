using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] bool _overridePlayerStartingPos = false;
    [SerializeField] CheckPoint _startingCheckPoint;
    [SerializeField] GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        if (_overridePlayerStartingPos)
        {
            _startingCheckPoint.OverridePlayerCheckpoint(_startingCheckPoint);
            CheckPoint.SpawnAtLastSpawnPoint(_player);
            _player.transform.rotation = Quaternion.Euler(0, 0, -45);
        }
    }
}
