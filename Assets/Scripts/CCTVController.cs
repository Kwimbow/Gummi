using UnityEngine;

public class CCTVMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Speed of the side-to-side sweep.")]
    public float rotationSpeed = 20f;
    [Tooltip("Maximum angle it can turn to the left and right from the center.")]
    public float maxAngle = 45f;

    void Update()
    {
        float angle = Mathf.PingPong(Time.time * rotationSpeed, maxAngle * 2) - maxAngle;
        transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, angle, transform.localEulerAngles.z);
    }
}