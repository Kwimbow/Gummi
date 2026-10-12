using UnityEngine;

public class CharacterPush : MonoBehaviour
{
    [SerializeField] private float pushPower = 2.0f;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // No Rigidbody, or the Rigidbody is kinematic
        if (body == null || body.isKinematic)
        {
            return;
        }

        // Do not push objects below the player feet
        if (hit.moveDirection.y < -0.3f)
        {
            return;
        }

        // Calculate push direction from move direction (horizontal only)
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // Apply force to the Rigidbody
        body.AddForceAtPosition(pushDir * pushPower, hit.point, ForceMode.Impulse);
    }
}