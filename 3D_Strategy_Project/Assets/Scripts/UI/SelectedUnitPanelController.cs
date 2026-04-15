using TMPro;
using UnityEngine;

public class SelectedUnitPanelController : MonoBehaviour
{
    [SerializeField] private MouseResolver m_mouseResolver;
    [SerializeField] private GameObject m_selectedUnitPanel;
    [SerializeField] private TMP_Text m_unitNameText;

    [Header("Slide animation positions")]
    [SerializeField] private Vector2 m_shownPosition;
    [SerializeField] private Vector2 m_hiddenPosition;
    [SerializeField] private float m_animationDuration = 0.5f;

    private RectTransform m_rectTransform;

    private void Awake()
    {
        m_rectTransform = m_selectedUnitPanel.GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        m_mouseResolver.OnSelect += ShowPanel;
        m_mouseResolver.OnDeselect += HidePanel;
    }

    private void ShowPanel(ISelectable selectable)
    {
        MonoBehaviour selectableMono = selectable as MonoBehaviour;
        m_unitNameText.text = selectableMono.gameObject.name;
        PanelSlideInOut.SlideIn(m_rectTransform, m_shownPosition, m_animationDuration);
        //m_selectedUnitPanel.SetActive(true);
    }

    private void HidePanel(ISelectable selectable)
    {
        m_unitNameText.text = "";
        PanelSlideInOut.SlideOut(m_rectTransform, m_hiddenPosition, m_animationDuration);
        //m_selectedUnitPanel.SetActive(false);
    }
}
