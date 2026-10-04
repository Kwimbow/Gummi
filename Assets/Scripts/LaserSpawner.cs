using UnityEngine;
using System.Collections;

public class LaserSpawner : MonoBehaviour
{
    public GameObject laser;
    public bool isSpawning = false;

    [Header("Spawn Interval Settings")]
    [SerializeField] private float minSpawnTime = 0.5f;
    [SerializeField] private float maxSpawnTime = 2f;
    private Coroutine spawnCoroutine;

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            spawnCoroutine = StartCoroutine(SpawnRoutine());
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (isSpawning)
        {
            float randomDelay = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(randomDelay);

            SpawnLaser();
        }
    }

    public void SpawnLaser()
    {
        float spawnPointY = Random.Range(0.6f, 4f);
        float spawnPointZ = 36f;
        Vector3 spawnPosition = new Vector3(transform.position.x, spawnPointY, spawnPointZ);

        GameObject newLaser = Instantiate(laser, spawnPosition, transform.rotation);

        MoveToStart moveScript = newLaser.GetComponent<MoveToStart>();

        if (moveScript != null)
        {
            moveScript.StartLaser();
        }
    }
}
