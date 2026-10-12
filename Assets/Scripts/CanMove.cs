using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CanPhysics : MonoBehaviour
{
    [Header("Hit Reaction Settings")]
    public float extraImpulseForce = 5f;
    public float minImpactSpeed = 2f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.ResetCenterOfMass();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (collision.relativeVelocity.magnitude >= minImpactSpeed)
            {
                Vector3 hitPoint = collision.contacts[0].point;
                Vector3 forceDirection = collision.relativeVelocity.normalized;

                rb.AddForceAtPosition(forceDirection * extraImpulseForce, hitPoint, ForceMode.Impulse);
            }
        }
    }
}