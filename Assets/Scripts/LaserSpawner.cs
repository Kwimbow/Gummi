using UnityEngine;
using System.Collections;

public class LaserSpawner : MonoBehaviour
{
    public static LaserSpawner Instance;

    public GameObject laser;
    private int detectorsActiveCount;
    private Transform playerTarget;

    [Header("Spawn Interval Settings")]
    [SerializeField] private float minSpawnTime = 0.5f;
    [SerializeField] private float maxSpawnTime = 2f;
    private Coroutine spawnCoroutine;

    void Awake()
    {
        Instance = this;
    }

    public void StartSpawning(Transform target)
    {
        playerTarget = target;
        detectorsActiveCount++;

        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnRoutine());
        }
    }

    public void StopSpawning()
    {
        detectorsActiveCount = Mathf.Max(0, detectorsActiveCount - 1);

        if (detectorsActiveCount == 0 && spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (detectorsActiveCount > 0)
        {
            float randomDelay = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(randomDelay);
            if (detectorsActiveCount > 0)
            {
                SpawnLaser();
            }
        }
        spawnCoroutine = null;
    }

    public void SpawnLaser()
    {
        if (laser == null) return;
        if (playerTarget == null)
        {
            Debug.LogWarning("Cannot spawn a laser because no player target is set.", this);
            return;
        }

        float spawnPointY = Random.Range(0.6f, 4f);
        float spawnPointZ = 36f;
        Vector3 spawnPosition = new Vector3(transform.position.x, spawnPointY, spawnPointZ);

        GameObject newLaser = Instantiate(laser, spawnPosition, transform.rotation);
        MoveToStart moveScript = newLaser.GetComponent<MoveToStart>();

        if (moveScript != null)
        {
            moveScript.StartLaser(playerTarget);
        }
        else
        {
            Debug.LogWarning("The assigned laser prefab has no MoveToStart component.", laser);
        }
    }
}