using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] 
    private float speed;

    [SerializeField] 
    private float minY;
    [SerializeField] 
    private float maxY;

    void Update()
    {
        float t = Mathf.PingPong(Time.time * speed, 1f);
        float currentY = Mathf.Lerp(minY, maxY, t);

        transform.position = new Vector3(transform.position.x, currentY, transform.position.z);
    }
}