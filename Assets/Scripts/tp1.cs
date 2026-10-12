using UnityEngine;

public class tp1 : MonoBehaviour
{
    [SerializeField] private GameObject player;
    void OnTriggerEnter(Collider other)
    {
        CharacterController cc = player.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false;
                player.transform.position = new Vector3(-20.4f, 0f, 36.6f);
                cc.enabled = true;
            }
            else
            {
                player.transform.position = new Vector3(-20.4f, 0f, 36.6f);
            }
    }
}
