using TMPro;
using UnityEngine;

public class HoverUnitNameController : MonoBehaviour
{
    [SerializeField] private MouseResolver m_mouseResolver;
    [SerializeField] private GameObject m_hoverUnitPanel;
    [SerializeField] private GameObject m_parentUnit;
    [SerializeField] private TMP_Text m_unitNameText;

    private void OnEnable()
    {
        m_mouseResolver.OnHoverEnter += ShowPanel;
        m_mouseResolver.OnHoverExit += HidePanel;
    }

    private void Awake()
    {
        m_unitNameText.text = m_parentUnit.name;
    }

    private void ShowPanel(ISelectable selectable)
    {        
        m_hoverUnitPanel.SetActive(true);
    }

    private void HidePanel(ISelectable selectable)
    {
        m_hoverUnitPanel.SetActive(false);
    }
}
