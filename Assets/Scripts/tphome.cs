using UnityEngine;

public class tphome : MonoBehaviour
{
    [SerializeField] private GameObject player;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        CharacterController cc = player.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false;
                player.transform.position = new Vector3(-0.08f,0f,-11.03f);
                cc.enabled = true;
            }
            else
            {
                player.transform.position = new Vector3(-0.08f,0f,-11.03f);
            }
        }
    }
}
