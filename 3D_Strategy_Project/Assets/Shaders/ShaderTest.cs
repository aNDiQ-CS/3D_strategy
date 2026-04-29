using UnityEngine;

public class ShaderTest : MonoBehaviour
{
    [SerializeField] private float m_frequency = 1f;
    [SerializeField] private float m_phaseOffset = 0f;
    [SerializeField] private string m_alpha = "_Alpha";

    private Renderer m_rend;
    private MaterialPropertyBlock m_propBlock;
    private int m_propId;

    void Start()
    {
        m_rend = GetComponent<Renderer>();
        m_propBlock = new MaterialPropertyBlock();
        m_propId = Shader.PropertyToID(m_alpha);
    }

    void Update()
    {
        float alpha = (Mathf.Sin(Time.time * m_frequency + m_phaseOffset) * 0.5f + 0.5f);
        // float currentAlpha = m_propBlock.GetFloat(m_propId);
        m_propBlock.SetFloat(m_propId, alpha);
        m_rend.SetPropertyBlock(m_propBlock);
    }
}
