using UnityEngine;

public class UnitSelection : MonoBehaviour
{    
    [SerializeField] private Outline m_outline;

    [Header("On Hover")]
    [SerializeField][Range(0f, 10f)] private float m_hoverOutlineWidth;
    [SerializeField] private Color m_hoverOutlineColor;

    private void Awake()
    {
        m_outline.OutlineWidth = 0f;
    }

    public void OnHoverEnter()
    {
        m_outline.OutlineWidth = m_hoverOutlineWidth;
        m_outline.OutlineColor = m_hoverOutlineColor;
    }
}
