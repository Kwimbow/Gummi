using UnityEngine;

public class MoveToStart : MonoBehaviour
{
    [SerializeField] private GameObject startWall;
    [SerializeField] private float speed = 5f;
    public bool isLaserMoving = false;

    void Start()
    {
        RandomizeZRotation();
    }
    void Update()
    {
        if (isLaserMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, startWall.transform.position, speed * Time.deltaTime);
        }
    }

    public void StartLaser(Transform targetTransform)
    {
        RandomizeZRotation();
        isLaserMoving = true;}

    public void RandomizeZRotation()
    {
        float currentX = transform.eulerAngles.x;
        float currentY = transform.eulerAngles.y;

        float randomZ = Random.Range(0f, 360f);

        transform.eulerAngles = new Vector3(currentX, currentY, randomZ);
    }
}