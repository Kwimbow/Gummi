using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class RibbonHelixSwirl : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Material capsuleMaterial;
    private Color originalBaseColor;
    private Color originalEmissionColor;

    private GameObject sparkleObj;
    private ParticleSystem sparkleParticles;

    public Transform targetCylinder; 
    private Vector3 originalCapsulePosition;
    
    [Header("Ribbon Settings")]
    public float height = 2.0f;
    public float radius = 0.6f;
    public float turns = 20f;
    public int resolution = 400;
    public float ribbonDuration = 5.0f;      

    [Header("Transformation & Glow Flow")]
    public float bottomOffset = 0.0f; 
    public float floatHeight = 2.0f;        // How high the capsule floats up during transformation
    public float shrinkDuration = 0.4f;     
    public float glowPeakDuration = 0.2f;   
    public float fadeDuration = 1.0f;       
    public float sparkleDelay = 0.5f;       
    public float sparkleTotalLifetime = 2.5f; 
    public float pauseBeforeRestart = 0.5f; // Duration used to lower the capsule back down

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;

        if (transform.childCount > 0)
        {
            sparkleObj = transform.GetChild(0).gameObject;
            sparkleParticles = sparkleObj.GetComponent<ParticleSystem>();
        }

        if (sparkleObj != null) sparkleObj.SetActive(false);

        if (targetCylinder != null)
        {
            originalCapsulePosition = targetCylinder.position;

            Renderer capRenderer = targetCylinder.GetComponent<Renderer>();
            if (capRenderer != null)
            {
                capsuleMaterial = capRenderer.material;
                if (capsuleMaterial.HasProperty("_BaseColor"))
                    originalBaseColor = capsuleMaterial.GetColor("_BaseColor");
                if (capsuleMaterial.HasProperty("_EmissionColor"))
                    originalEmissionColor = capsuleMaterial.GetColor("_EmissionColor");
            }
        }

        StartCoroutine(TransformationLoopRoutine());
    }

    IEnumerator TransformationLoopRoutine()
    {
        while (true)
        {
            lineRenderer.positionCount = 0;
            ResetCapsuleAppearance();

            if (sparkleObj != null) sparkleObj.SetActive(false);

            if (targetCylinder != null)
                targetCylinder.position = originalCapsulePosition;

            // --- STEP 1: Ribbon Down to Up & Capsule Floats Up ---
            float elapsed = 0f;
            while (elapsed < ribbonDuration)
            {
                if (!targetCylinder) yield break;

                elapsed += Time.deltaTime;
                float progress = Mathf.Clamp01(elapsed / ribbonDuration);
                int activePoints = Mathf.FloorToInt(Mathf.Lerp(2, resolution, progress));
                lineRenderer.positionCount = activePoints;

                // Smoothly float the capsule up to its target height while the ribbon builds
                targetCylinder.position = Vector3.Lerp(originalCapsulePosition, originalCapsulePosition + Vector3.up * floatHeight, progress);

                Vector3 basePos = targetCylinder.position - new Vector3(0, targetCylinder.localScale.y + bottomOffset, 0);

                for (int i = 0; i < activePoints; i++)
                {
                    float t = (float)i / (resolution - 1);
                    if (t > progress) t = progress;

                    float currentHeight = t * height;
                    float angle = t * turns * 2f * Mathf.PI;

                    float x = Mathf.Cos(angle) * radius;
                    float z = Mathf.Sin(angle) * radius;

                    Vector3 pos = basePos + new Vector3(x, currentHeight, z);
                    lineRenderer.SetPosition(i, pos);
                }

                yield return null;
            }

            // --- STEP 2: Ribbon Shrinks Inward into the Capsule ---
            float shrinkElapsed = 0f;
            float startRadius = radius;
            
            while (shrinkElapsed < shrinkDuration)
            {
                shrinkElapsed += Time.deltaTime;
                float shrinkT = Mathf.Clamp01(shrinkElapsed / shrinkDuration);
                
                float currentRadius = Mathf.Lerp(startRadius, 0f, shrinkT);
                Vector3 basePos = targetCylinder.position - new Vector3(0, targetCylinder.localScale.y + bottomOffset, 0);

                for (int i = 0; i < resolution; i++)
                {
                    float t = (float)i / (resolution - 1);
                    float currentHeight = t * height;
                    float angle = t * turns * 2f * Mathf.PI;

                    float x = Mathf.Cos(angle) * currentRadius;
                    float z = Mathf.Sin(angle) * currentRadius;

                    Vector3 pos = basePos + new Vector3(x, currentHeight, z);
                    lineRenderer.SetPosition(i, pos);
                }

                if (capsuleMaterial != null)
                {
                    Color targetGlow = Color.white * 6f;
                    if (capsuleMaterial.HasProperty("_EmissionColor"))
                        capsuleMaterial.SetColor("_EmissionColor", Color.Lerp(originalEmissionColor, targetGlow, shrinkT));
                }

                yield return null;
            }

            lineRenderer.positionCount = 0;
            yield return new WaitForSeconds(glowPeakDuration);

            // --- STEP 3: Independent Timelines for Fade and Sparkles (Capsule stays floating up) ---
            float actionElapsed = 0f;
            bool sparklesTriggered = false;

            float maxRoutineTime = Mathf.Max(fadeDuration, sparkleDelay + sparkleTotalLifetime);

            while (actionElapsed < maxRoutineTime)
            {
                actionElapsed += Time.deltaTime;

                // 1. Handle Capsule Emission Fade
                if (actionElapsed <= fadeDuration)
                {
                    float fadeT = Mathf.Clamp01(actionElapsed / fadeDuration);
                    if (capsuleMaterial != null && capsuleMaterial.HasProperty("_EmissionColor"))
                    {
                        capsuleMaterial.SetColor("_EmissionColor", Color.Lerp(Color.white * 6f, originalEmissionColor, fadeT));
                    }
                }
                else
                {
                    ResetCapsuleAppearance();
                }

                // 2. Handle Sparkles Burst & Independent Lifetime
                if (!sparklesTriggered && actionElapsed >= sparkleDelay)
                {
                    sparklesTriggered = true;
                    if (sparkleObj != null)
                    {
                        sparkleObj.SetActive(true);
                        if (sparkleParticles != null)
                        {
                            sparkleParticles.Clear();
                            sparkleParticles.Play();
                        }
                    }
                }

                yield return null;
            }

            ResetCapsuleAppearance();

            if (sparkleObj != null) sparkleObj.SetActive(false);

            // --- STEP 4: Lower Capsule Back Down During Pause Before Restart ---
            float descendElapsed = 0f;
            Vector3 elevatedPosition = targetCylinder != null ? targetCylinder.position : originalCapsulePosition;

            while (descendElapsed < pauseBeforeRestart)
            {
                descendElapsed += Time.deltaTime;
                float descendT = Mathf.Clamp01(descendElapsed / pauseBeforeRestart);

                if (targetCylinder != null)
                {
                    targetCylinder.position = Vector3.Lerp(elevatedPosition, originalCapsulePosition, descendT);
                }

                yield return null;
            }

            if (targetCylinder != null)
                targetCylinder.position = originalCapsulePosition;
        }
    }

    void ResetCapsuleAppearance()
    {
        if (capsuleMaterial != null)
        {
            if (capsuleMaterial.HasProperty("_EmissionColor"))
                capsuleMaterial.SetColor("_EmissionColor", originalEmissionColor);
        }
    }
}