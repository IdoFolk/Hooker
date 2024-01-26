using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    static CheckPoint _lastcheckPoint;
    public Transform SpawnPoint;
    [SerializeField] private GameObject _wall;
    [SerializeField] private GameObject _activateParticles;
    [SerializeField] private GameObject _idleParticles;
    private void Start()
    {
        _idleParticles.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            _lastcheckPoint = this;
            Debug.Log("Spawn Point is set to" + this.transform.parent.name);
            _activateParticles.SetActive(true);
            _idleParticles.SetActive(false);
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
    }
}
