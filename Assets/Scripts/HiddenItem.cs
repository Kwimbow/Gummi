using UnityEngine;

public class HiddenItem : MonoBehaviour, IRevealedObject
{
    private MeshRenderer meshRenderer;
    private Collider itemCollider;
    private float lastShinedTime;
    public float fadeDelay = 0.2f;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        itemCollider = GetComponent<Collider>();
        
        meshRenderer.enabled = false;
        if (itemCollider != null) itemCollider.isTrigger = true; 
    }

    public void OnShinedUpon()
    {
        meshRenderer.enabled = true;
        if (itemCollider != null) itemCollider.isTrigger = false;
        lastShinedTime = Time.time;
    }

    void Update()
    {
        if (Time.time - lastShinedTime > fadeDelay)
        {
            meshRenderer.enabled = false;
            if (itemCollider != null) itemCollider.isTrigger = true;
        }
    }
}