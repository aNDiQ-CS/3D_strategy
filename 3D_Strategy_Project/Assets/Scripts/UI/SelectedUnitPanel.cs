using TMPro;
using UnityEngine;

public class SelectedUnitPanel : MonoBehaviour
{
    [SerializeField] private MouseResolver m_mouseResolver;
    [SerializeField] private TMP_Text m_unitNameText;

    private void OnEnable()
    {
        m_mouseResolver.OnSelect += SetName;
        m_mouseResolver.OnDeselect += ClearName;
    }

    private void SetName(ISelectable selectable)
    {
        MonoBehaviour selectableMono = selectable as MonoBehaviour;
        m_unitNameText.text = selectableMono.gameObject.name;
    }

    private void ClearName(ISelectable selectable)
    {
        m_unitNameText.text = "";
    }
}
