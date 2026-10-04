using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    public GameObject HealthBonus;
    public GameObject SpeedBonus;

    [SerializeField] private int bonusAmount = 3;
    private bool hasSpawned = false;
    public void SpawnBonuses()
    {   
        if (hasSpawned) return;

        for (int i = 0; i < bonusAmount; i++)
        {
            Vector3 randomPos = GetRandomSpawnPosition();
            Instantiate(HealthBonus, randomPos, transform.rotation);
        }

        for (int i = 0; i < bonusAmount; i++)
        {
            Vector3 randomPos = GetRandomSpawnPosition();
            Instantiate(SpeedBonus, randomPos, transform.rotation);
        }
        hasSpawned = true;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float spawnPointX = Random.Range(-3.5f, 4.3f);
        float spawnPointZ = Random.Range(-60f, 26f);

        return new Vector3(spawnPointX, transform.position.y, spawnPointZ);
    }

}
