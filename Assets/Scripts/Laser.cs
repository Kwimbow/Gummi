using UnityEngine;

public class Laser : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealthManager health = other.GetComponentInParent<HealthManager>();
            Timer timer = FindAnyObjectByType<Timer>();
            
            if (!timer.isWinner)
            {
                health.TakeDamage(2f);
                Destroy(gameObject);
            }
        }
    }
}