using UnityEngine;

public class UnitHighlight : MonoBehaviour
{
    [Header("Outline settings")]
    [SerializeField] private Outline m_outline;

    [SerializeField][Range(0f, 10f)] private float m_hoverOutlineWidth = 5f;
    [SerializeField] private Color m_hoverColor;

    [SerializeField][Range(0f, 10f)] private float m_selectOutlineWidth = 10f;
    [SerializeField] private Color m_selectColor;

    [SerializeField] private LayerMask m_mask;

    private void OnValidate()
    {
        m_outline = GetComponent<Outline>();
    }

    private void Awake()
    {
        if (m_outline is null) m_outline = GetComponent<Outline>();
    }

    private void Start()
    {
        m_outline.OutlineWidth = 0f;
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, float.MaxValue, m_mask))
        {
            if (Input.GetMouseButton(0))
            {
                SelectOutline();
            }
            else
            {
                HoverOutline();
            }
        }
    }

    private void HoverOutline()
    {
        m_outline.OutlineWidth = m_hoverOutlineWidth;
        m_outline.OutlineColor = m_hoverColor;        
    }

    private void SelectOutline()
    {
        m_outline.OutlineWidth = m_selectOutlineWidth;
        m_outline.OutlineColor = m_selectColor;
    }
}
