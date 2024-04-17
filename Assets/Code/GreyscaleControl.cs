using UnityEngine;

[ExecuteInEditMode]
public class GreyscaleControl : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float GreyscaleAmount; // The value you want to set in the Inspector.

    [SerializeField]
    private MaterialPropertyBlock m_propBlock;
    [SerializeField]
    private Renderer m_renderer;

    void OnValidate()
    {
        ApplyGreyscale();
    }

    void Start()
    {
        m_propBlock = new MaterialPropertyBlock();
        m_renderer = GetComponent<Renderer>();
        ApplyGreyscale();
    }

    void ApplyGreyscale()
    {
        if (m_renderer == null)
            m_renderer = GetComponent<Renderer>();

        if (m_propBlock == null)
            m_propBlock = new MaterialPropertyBlock();

        // Get the current value of the material properties
        m_renderer.GetPropertyBlock(m_propBlock);
        // Set the greyscale amount
        m_propBlock.SetFloat("_Greyscale", GreyscaleAmount);
        // Apply the modified property block to the renderer
        m_renderer.SetPropertyBlock(m_propBlock);
    }
}
