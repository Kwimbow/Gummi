using UnityEngine;

public class VisibleItem : MonoBehaviour, IRevealedObject
{
    private MeshRenderer meshRenderer;
    private Collider itemCollider;
    private float lastShinedTime;
    public float fadeDelay = 0.2f;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        itemCollider = GetComponent<Collider>();
        
        meshRenderer.enabled = true;
        if (itemCollider != null) itemCollider.isTrigger = false;
    }

    public void OnShinedUpon()
    {
        meshRenderer.enabled = false;
        if (itemCollider != null) itemCollider.isTrigger = true;
        lastShinedTime = Time.time;
    }

    void Update()
    {
        if (Time.time - lastShinedTime > fadeDelay)
        {
            meshRenderer.enabled = true;
            if (itemCollider != null) itemCollider.isTrigger = false;
        }
    }
}