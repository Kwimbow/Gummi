using UnityEngine;
using System.Collections;

public class HealCharacter : MonoBehaviour
{
    [SerializeField] private float respawnDelay = 5f;

    private MeshRenderer meshRenderer;
    private Collider myCollider;
    private bool isCollected = false;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        myCollider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            HealthManager health = other.GetComponentInParent<HealthManager>();
            Timer timer = FindAnyObjectByType<Timer>();
            
            if (!timer.isWinner)
            {
                isCollected = true;
                if(health.healthAmount < 10f)
                {
                    health.Heal(1f);
                    print("+1 Health");
                }
                else
                {
                    print("Already Full Health!");
                }
                StartCoroutine(DisappearSequence());
            }
        }
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
