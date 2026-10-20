using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour
{

    [SerializeField] private GameObject FlashlightLight;
    private bool FlashlightActive = false;
    void Start()
    {
        FlashlightLight.gameObject.SetActive(false);
    }

    void Update()
    {
        if(Keyboard.current.eKey.wasPressedThisFrame)
        {
            if(FlashlightActive == false)
            {
                FlashlightLight.gameObject.SetActive(true);
                FlashlightActive = true;
                print("Flashlight On");
            }
            else
            {
                FlashlightLight.gameObject.SetActive(false);
                FlashlightActive = false;
                print("Flashlight Off");
            }
        }
    }
}
