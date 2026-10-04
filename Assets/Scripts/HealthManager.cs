using UnityEngine;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 10f;

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 10f;
        if (healthAmount <= 0f)
        {
            print("You died! Respawning player...");
            healthAmount = 10f;
            healthBar.fillAmount = healthAmount / 10f;
            Timer timer = FindAnyObjectByType<Timer>();
            timer.isTimerRunning = false;
            timer.RespawnPlayer();
        }
    }

    public void Heal(float healAmount)
    {
        healthAmount += healAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0f, 10f);
        healthBar.fillAmount = healthAmount / 10f;
    }
}
