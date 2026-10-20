using UnityEngine;
using System.Collections;

public class SpeedUpCharacter : MonoBehaviour
{
    [SerializeField] private float respawnDelay = 5f;
    [SerializeField] private float speedMultiplier = 1.5f;
    [SerializeField] private float speedDuration = 5f;

    private MeshRenderer meshRenderer;
    private Collider myCollider;
    private bool isCollected = false;
    private static float defaultMoveSpeed = 0f;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        myCollider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            var playerController = other.GetComponentInParent<StarterAssets.FirstPersonController>();
            Timer timer = FindAnyObjectByType<Timer>();
            
            if (!timer.isWinner)
            {
                if (defaultMoveSpeed == 0f)
                {
                    defaultMoveSpeed = playerController.MoveSpeed;
                }

                if (playerController.MoveSpeed > defaultMoveSpeed)
                {
                    return; 
                }

                isCollected = true;
                StartCoroutine(ApplySpeedBoost(playerController));
                StartCoroutine(DisappearSequence());
            }
        }
    }

    private IEnumerator ApplySpeedBoost(StarterAssets.FirstPersonController player)
    {
        float originalSpeed = player.MoveSpeed;
        float originalSprintSpeed = player.SprintSpeed;
        player.MoveSpeed *= speedMultiplier;
        player.SprintSpeed *= speedMultiplier;
        print("x2 Speed for 5 seconds");

        yield return new WaitForSeconds(speedDuration);

        player.MoveSpeed = originalSpeed;
        player.SprintSpeed = originalSprintSpeed;
    }

    private IEnumerator DisappearSequence()
    {
        if (meshRenderer != null) 
        {
            meshRenderer.enabled = false;
        }
        if (myCollider != null) 
        {
            myCollider.enabled = false;
        }

        yield return new WaitForSeconds(respawnDelay);

        
        if (meshRenderer != null) 
        {
            meshRenderer.enabled = true;
        }
        if (myCollider != null) 
        {
            myCollider.enabled = true;
        }
        isCollected = false;
    }
}
