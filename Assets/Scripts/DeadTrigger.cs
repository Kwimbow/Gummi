using UnityEngine;

public class DeadTrigger : MonoBehaviour
{
    [SerializeField] 
    Vector3 respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CharacterController cc = other.GetComponent<CharacterController>();
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (cc != null) cc.enabled = false;
            if (rb != null) rb.linearVelocity = Vector3.zero;

            other.transform.position = respawnPoint;

            if (cc != null) cc.enabled = true;
        }
    }
}