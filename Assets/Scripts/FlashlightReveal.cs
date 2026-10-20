using UnityEngine;

public class FlashlightReveal : MonoBehaviour
{
    public float lightRange = 15f;
    [Range(0, 90)] public float spotAngle = 30f; 
    public LayerMask hiddenObjectMask;
    public LayerMask occlusionMask;

    void Update()
    {
        RevealHiddenObjects();
    }

    void RevealHiddenObjects()
    {
        Collider[] hiddenColliders = Physics.OverlapSphere(transform.position, lightRange, hiddenObjectMask);

        foreach (var col in hiddenColliders)
        {
            IRevealedObject revealable = col.GetComponent<IRevealedObject>();
            if (revealable == null) continue;

            Vector3 dirToTarget = (col.transform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToTarget) < spotAngle)
            {
                float distToTarget = Vector3.Distance(transform.position, col.transform.position);

                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, occlusionMask))
                {
                    revealable.OnShinedUpon();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawRay(transform.position, transform.forward * lightRange);

        Vector3 leftEdge = Quaternion.AngleAxis(-spotAngle, transform.up) * transform.forward;
        Vector3 rightEdge = Quaternion.AngleAxis(spotAngle, transform.up) * transform.forward;
        Gizmos.DrawRay(transform.position, leftEdge * lightRange);
        Gizmos.DrawRay(transform.position, rightEdge * lightRange);

        Vector3 topEdge = Quaternion.AngleAxis(-spotAngle, transform.right) * transform.forward;
        Vector3 bottomEdge = Quaternion.AngleAxis(spotAngle, transform.right) * transform.forward;
        Gizmos.DrawRay(transform.position, topEdge * lightRange);
        Gizmos.DrawRay(transform.position, bottomEdge * lightRange);
    }
}