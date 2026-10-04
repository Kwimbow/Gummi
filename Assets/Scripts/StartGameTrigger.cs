using UnityEngine;

public class StartGameTrigger : MonoBehaviour
{
    [Tooltip("Timer")]
    public Timer timer;

    [Tooltip("Laser Spawner")]
    public LaserSpawner laserSpawner;

    [Tooltip("Bonus Spawner")]
    public BonusSpawner bonusSpawner;

    [Tooltip("Player")]
    public string playerTag = "Player";

    private void Start()
    {
        if (laserSpawner == null)
        {
            laserSpawner = FindAnyObjectByType<LaserSpawner>();
        }

        if (bonusSpawner == null)
        {
            bonusSpawner = FindAnyObjectByType<BonusSpawner>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (timer != null && !timer.isWinner)
            {
                timer.StartTimer();
            }

            if (laserSpawner != null && !timer.isWinner)
            {
                laserSpawner.StartSpawning();
            }

            if (bonusSpawner != null && !timer.isWinner)
            {
                bonusSpawner.SpawnBonuses();
            }
        }

        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
        }
    }
}
