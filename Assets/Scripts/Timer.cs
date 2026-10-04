using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime = 60f;
    public bool isTimerRunning = false;
    public bool isWinner = false;
    private float maxTime;
    [Header("Respawn Settings")]
    [SerializeField] private Vector3 respawnPoint = new Vector3(0.519999981f, 0.448592871f, -66.1600037f);
    [SerializeField] private GameObject player;

    private void Start()
    {
        maxTime = remainingTime;
    }
    void Update()
    {
        if(isTimerRunning)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
            }
            else
            {
                remainingTime = 0;
                isTimerRunning = false;
                if (!isWinner){ //if player hasnt reached the end before timer ends, lose
                    print("Time's up! Respawning player...");
                    RespawnPlayer();
                }
            }
            int seconds = Mathf.FloorToInt(remainingTime);
            timerText.text = string.Format("{00}", seconds);
        }
    }

    public void StartTimer()
    {
        isTimerRunning = true;
    }

    public void RespawnPlayer()
    {
        remainingTime = maxTime;
        isWinner = false;
        int seconds = Mathf.FloorToInt(remainingTime);
        timerText.text = string.Format("{00}", seconds);
        
        if (player != null)
        {
            CharacterController controller = player.GetComponent<CharacterController>();
            if (controller != null)
            {
                controller.enabled = false;
                player.transform.position = respawnPoint;
                player.transform.rotation = Quaternion.identity;
                controller.enabled = true;
            }
            else
            {
                player.transform.position = respawnPoint;
                player.transform.rotation = Quaternion.identity;
            }
        }
    }
}
