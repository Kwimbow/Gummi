using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    [Tooltip("Timer")]
    public Timer timer;

    [Tooltip("Player")]
    public string playerTag = "Player";
    private LaserSpawner laserSpawner;

    private void Start()
    {
        laserSpawner = FindAnyObjectByType<LaserSpawner>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            timer.isTimerRunning = false;
            timer.isWinner = true;
            laserSpawner.StopSpawning();
            print("You win!");

            GameObject[] activeLasers = GameObject.FindGameObjectsWithTag("Laser");
            foreach (GameObject laser in activeLasers)
            {
                Destroy(laser);
            }
        }
    }
}
