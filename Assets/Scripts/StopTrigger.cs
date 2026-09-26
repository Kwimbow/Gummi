using UnityEngine;

public class StopTrigger : MonoBehaviour
{
    [SerializeField] 
    private XToX platformScript;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            platformScript.StopPlatform();
        }
    }
}
