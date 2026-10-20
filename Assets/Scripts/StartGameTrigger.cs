using UnityEngine;

public class StartGameTrigger : MonoBehaviour
{
    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
        }
    }
}
