using UnityEngine;

public class bonusMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] 
    private float speed = 1.5f;

    [SerializeField] 
    private float minY = 1.5f;
    [SerializeField] 
    private float maxY = 1.7f;

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.PingPong(Time.time * speed, 1f);
        float currentY = Mathf.Lerp(minY, maxY, t);

        transform.position = new Vector3(transform.position.x, currentY, transform.position.z);
    }
}
