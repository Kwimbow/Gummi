using UnityEngine;

public class CCTVDetector : MonoBehaviour
{
    [Header("Game Starters")]
    public Timer timer;
    public BonusSpawner bonusSpawner;

    [Header("Detection Settings")]
    public LayerMask playerLayer;
    public LayerMask obstacleLayer;
    public Transform player;

    private Light spotLight;
    private bool bonusesSpawned = false;
    private bool wasPlayerCaught;

    void Start()
    {
        spotLight = GetComponent<Light>();
        if (spotLight != null)
        {
            spotLight.color = Color.red;
        }
        if (player == null)
        {
            GameObject pObj = GameObject.FindGameObjectWithTag("Player");
            if (pObj != null) player = pObj.transform;
        }

        if (timer == null) timer = FindAnyObjectByType<Timer>();
        if (bonusSpawner == null) bonusSpawner = FindAnyObjectByType<BonusSpawner>();
    }

    void Update()
    {
        CheckPlayerVisibility();
    }

    void CheckPlayerVisibility()
    {
        if (player == null || spotLight == null) return;

        bool isPlayerCaught = false;

        Collider[] hits = Physics.OverlapSphere(transform.position, spotLight.range, playerLayer);

        foreach (var hit in hits)
        {
            if (hit.transform == player)
            {
                Vector3 directionToPlayer = (player.position - transform.position).normalized;
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);

                if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleLayer, QueryTriggerInteraction.Ignore))
                {
                    float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
                    if (angleToPlayer <= spotLight.spotAngle / 2f)
                    {
                        isPlayerCaught = true;
                        break;
                    }
                }
            }
        }

        if (isPlayerCaught)
        {
            TriggerAlarmAndStartGame();

        }

        if (isPlayerCaught != wasPlayerCaught)
        {
            if (LaserSpawner.Instance != null)
            {
                if (isPlayerCaught)
                {
                    LaserSpawner.Instance.StartSpawning(player);
                }
                else
                {
                    LaserSpawner.Instance.StopSpawning();
                }
            }

            wasPlayerCaught = isPlayerCaught;
        }
    }

    void OnDisable()
    {
        if (wasPlayerCaught && LaserSpawner.Instance != null)
        {
            LaserSpawner.Instance.StopSpawning();
            wasPlayerCaught = false;
        }
    }

    void TriggerAlarmAndStartGame()
    {
        if (timer != null && !timer.isWinner)
        {
            timer.StartTimer();
        }

        // Spawn bonuses once
        if (bonusSpawner != null && !bonusesSpawned && (timer == null || !timer.isWinner))
        {
            bonusSpawner.SpawnBonuses();
            bonusesSpawned = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        Light lightComp = GetComponent<Light>();
        if (lightComp == null) return;

        float range = lightComp.range;
        float spotAngle = lightComp.spotAngle;
        float halfAngle = spotAngle / 2f;

        Gizmos.color = Color.red;
        Vector3 pos = transform.position;
        Gizmos.DrawWireSphere(pos, range);

        Vector3 forward = transform.forward;
        Vector3 up = transform.up;
        Vector3 right = transform.right;

        Vector3 leftDir = Quaternion.AngleAxis(-halfAngle, up) * forward;
        Vector3 rightDir = Quaternion.AngleAxis(halfAngle, up) * forward;
        Vector3 upDir = Quaternion.AngleAxis(-halfAngle, right) * forward;
        Vector3 downDir = Quaternion.AngleAxis(halfAngle, right) * forward;

        Gizmos.DrawLine(pos, pos + leftDir * range);
        Gizmos.DrawLine(pos, pos + rightDir * range);
        Gizmos.DrawLine(pos, pos + upDir * range);
        Gizmos.DrawLine(pos, pos + downDir * range);
    }
}