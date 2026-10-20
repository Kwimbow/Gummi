using UnityEngine;

public class MovingWallTargetZ : MonoBehaviour
{
    [Header("Target Positions (Z Axis)")]
    [Tooltip("The Z coordinate where the wall starts.")]
    [SerializeField] private float startZ = 2.64f;

    [Tooltip("The Z coordinate where the wall travels to.")]
    [SerializeField] private float endZ = 23.51f;

    [Header("Movement Settings")]
    [Tooltip("How fast the wall moves between points.")]
    [SerializeField] private float speed = 3f;

    void Update()
    {
        float time = Mathf.PingPong(Time.time * speed, 1f);
        float currentZ = Mathf.Lerp(startZ, endZ, time);
        transform.position = new Vector3(transform.position.x, transform.position.y, currentZ);
    }
}