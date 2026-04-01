using UnityEngine;

public class UnitSelection : MonoBehaviour
{    
    [SerializeField] private Outline m_outline;

    [Header("On Hover")]
    [SerializeField][Range(0f, 10f)] private float m_hoverOutlineWidth;
    [SerializeField] private Color m_hoverOutlineColor;

    [Header("On Select")]
    [SerializeField][Range(0f, 10f)] private float m_selectOutlineWidth;
    [SerializeField] private Color m_selectOutlineColor;

    private bool m_isSelected;
    public bool IsSelected => m_isSelected;

    private void Awake()
    {
        m_outline.OutlineWidth = 0f;
    }

    public void OnHoverEnter()
    {
        if (!m_isSelected)
        {
            m_outline.OutlineColor = m_hoverOutlineColor;
            m_outline.OutlineWidth = m_hoverOutlineWidth;
        }        
    }

    public void OnHoverExit()
    {
        if (!m_isSelected)
        {
            m_outline.OutlineWidth = 0f;
        }
    }

    public void OnSelect()
    {
        m_isSelected = true;
        m_outline.OutlineColor = m_selectOutlineColor;
        m_outline.OutlineWidth = m_selectOutlineWidth;
    }    

    public void OnDeselect()
    {
        m_isSelected = false;
        m_outline.OutlineWidth = 0f;
    }
}
