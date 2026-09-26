using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] 
    private float speed = 0.2f;

    [SerializeField] 
    private float minY = 0.7246015f;
    [SerializeField] 
    private float maxY = 9f;

    void Update()
    {
        float t = Mathf.PingPong(Time.time * speed, 1f);
        float currentY = Mathf.Lerp(minY, maxY, t);

        transform.position = new Vector3(transform.position.x, currentY, transform.position.z);
    }
}