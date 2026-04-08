using TMPro;
using UnityEngine;

public class SelectedUnitPanelController : MonoBehaviour
{
    [SerializeField] private MouseResolver m_mouseResolver;
    [SerializeField] private GameObject m_selectedUnitPanel;
    [SerializeField] private TMP_Text m_unitNameText;

    private void OnEnable()
    {
        m_mouseResolver.OnSelect += ShowPanel;
        m_mouseResolver.OnDeselect += HidePanel;
    }

    private void ShowPanel(ISelectable selectable)
    {
        MonoBehaviour selectableMono = selectable as MonoBehaviour;
        m_unitNameText.text = selectableMono.gameObject.name;
        m_selectedUnitPanel.SetActive(true);
    }

    private void HidePanel(ISelectable selectable)
    {
        m_unitNameText.text = "";
        m_selectedUnitPanel.SetActive(false);
    }
}
