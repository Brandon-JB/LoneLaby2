using UnityEngine;
using System.Collections;

public class HueShiftManager : MonoBehaviour
{
    public Material[] materials; // Assign multiple materials here
    public Material singleMaterial; // Assign a single material here if needed
    public float transitionDuration = 2.0f; // Duration of transition
    private bool isTransitioning = false;

    void Start()
    {
        // Initialize all materials with full color
        SetHue(1.0f);
    }

    private void OnDisable()
    {
        //Set back to full color, as this also affects the editor. fun!
        SetHue(1.0f);
    }

    public void StartHueTransitionSingle()
    {
        if (singleMaterial != null && !isTransitioning)
            StartCoroutine(HueShiftCoroutine(new Material[] { singleMaterial }));
    }

    public void StartHueTransitionMultiple()
    {
        if (materials.Length > 0 && !isTransitioning)
            StartCoroutine(HueShiftCoroutine(materials));
    }

    private IEnumerator HueShiftCoroutine(Material[] targetMaterials)
    {
        isTransitioning = true;
        float elapsed = 0f;

        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float hueShift = Mathf.Lerp(1.0f, 0.0f, elapsed / transitionDuration);

            foreach (Material mat in targetMaterials)
            {
                if (mat != null)
                    mat.SetFloat("_HueShift", hueShift);
            }

            yield return null;
        }

        // Ensure final hue shift is set to 0
        SetHue(0.0f, targetMaterials);
        isTransitioning = false;
    }

    private void SetHue(float hue, Material[] targetMaterials = null)
    {
        Material[] mats = targetMaterials ?? materials; // Default to assigned materials if none provided

        foreach (Material mat in mats)
        {
            if (mat != null)
                mat.SetFloat("_HueShift", hue);
        }

        if (singleMaterial != null && targetMaterials == null)
            singleMaterial.SetFloat("_HueShift", hue);
    }
}
