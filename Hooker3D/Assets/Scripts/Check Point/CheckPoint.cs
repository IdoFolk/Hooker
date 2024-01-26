using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    static CheckPoint _lastcheckPoint;
    public Transform SpawnPoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            _lastcheckPoint = this;
            Debug.Log("Spawn Point is set to" + this.transform.parent.name);
        }
    }
    public static void SpawnAtLastSpawnPoint(GameObject ball)
    {
        ball.transform.position = _lastcheckPoint.SpawnPoint.position;
    }
}