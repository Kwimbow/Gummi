using UnityEngine;

public class StartTrigger : MonoBehaviour
{
    [SerializeField] 
    private XToX platformScript;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            platformScript.StartPlatform();
        }
    }
}
