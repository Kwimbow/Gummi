using UnityEngine;

public class DeadTrigger : MonoBehaviour
{
    [SerializeField] 
    Vector3 respawnPoint = new Vector3(19, 2,6);

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null) rb.linearVelocity = Vector3.zero;

            other.transform.position = respawnPoint;

        }
    }
}