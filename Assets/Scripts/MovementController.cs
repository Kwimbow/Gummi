
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 moveInput;

    [SerializeField]
    public float jumpspeed = 5f;
    [SerializeField] 
    private float moveSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 targetVelocity = new Vector3(moveInput.x * moveSpeed, rb.linearVelocity.y, moveInput.y * moveSpeed);
        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && Mathf.Abs(rb.linearVelocity.y) < 0.001f)
        {
            rb.AddForce(Vector3.up * jumpspeed, ForceMode.Impulse);
        }
    }
}
