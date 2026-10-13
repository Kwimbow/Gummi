using UnityEngine;

public class SideToSideKinematic : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("How far left and right the object moves from its start position.")]
    public float distance = 3.0f;

    [Tooltip("How fast the object moves back and forth.")]
    public float speed = 2.0f;

    private Vector3 startPosition;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = rb.position;
    }

    void FixedUpdate()
    {
        float offset = Mathf.Sin(Time.time * speed) * distance;
        Vector3 targetPos = startPosition + transform.TransformDirection(new Vector3(offset, 0f, 0f));
        rb.MovePosition(targetPos);
    }
}