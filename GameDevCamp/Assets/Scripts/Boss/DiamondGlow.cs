using UnityEngine;

public class DiamondGlow : MonoBehaviour
{
    private Renderer renderer;
    private Material material;
    private bool hasEmissionMap = false;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        material = renderer.material;
        hasEmissionMap = material.HasProperty("_EmissionMap");

        // Disable emission map at the start
        /*if (hasEmissionMap)
        {
            material.DisableKeyword("_EMISSION");
        }*/
    }

    public void EnableGlow()
    {
        // Enable emission map
        if (hasEmissionMap)
        {
            material.EnableKeyword("_EMISSION");
        }
    }

    public void DisableGlow()
    {
        // Disable emission map
        if (hasEmissionMap)
        {
            material.DisableKeyword("_EMISSION");
        }
    }
}