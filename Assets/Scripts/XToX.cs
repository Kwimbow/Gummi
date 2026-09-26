using UnityEngine;

public class XToX : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float distance = 4f;
    
    [Header("Direction Settings")]
    [SerializeField] private Vector3 moveDirection; 

    [Header("State")]
    [SerializeField] private bool isMoving = true;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (isMoving)
        {
            float offset = Mathf.PingPong(Time.time * speed, distance);
            transform.position = startPosition + (moveDirection.normalized * offset);
        }
    }

    public void StartPlatform() => isMoving = true;
    public void StopPlatform() => isMoving = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}

