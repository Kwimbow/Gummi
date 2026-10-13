using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class BallShooter : MonoBehaviour
{
    [Header("Ball Settings")]
    public GameObject ballPrefab;
    public Transform attackPoint;
    public Camera fpsCam;

    [Header("Physics & Spread")]
    public float shootForce = 35f;
    public float upwardForce = 0.5f;
    public float spread;

    [Header("Ammo Settings")]
    public int maxBalls = 10;
    private int ballsRemaining;

    [Header("Timing")]
    public float timeBetweenShooting = 0.25f;
    private bool readyToShoot = true;

    private int shootableLayerMask;

    private void Awake()
    {
        ballsRemaining = maxBalls;
        shootableLayerMask = ~LayerMask.GetMask("Ball", "Player", "Ignore Raycast");
    }

    private void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        bool isShooting = Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;

        if (readyToShoot && isShooting && ballsRemaining > 0)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetPoint = Physics.Raycast(ray, out hit, 100f, shootableLayerMask) ? hit.point : ray.GetPoint(75f);

        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        GameObject currentBall = Instantiate(ballPrefab, attackPoint.position, Quaternion.identity);
        currentBall.transform.forward = directionWithSpread.normalized;

        Collider ballCollider = currentBall.GetComponent<Collider>();
        Collider playerCollider = GetComponentInParent<Collider>();
        if (ballCollider != null && playerCollider != null)
        {
            Physics.IgnoreCollision(ballCollider, playerCollider);
        }

        Rigidbody rb = currentBall.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 finalForceDirection = directionWithSpread.normalized * shootForce + fpsCam.transform.up * upwardForce;
            rb.AddForce(finalForceDirection, ForceMode.Impulse);
        }

        ballsRemaining--;

        Invoke(nameof(ResetShot), timeBetweenShooting);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }
}