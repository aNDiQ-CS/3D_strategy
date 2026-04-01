using UnityEngine;

public class Unit : MonoBehaviour, ISelectable
{
    [SerializeField] private MouseResolver m_mouseResolver;
    [SerializeField] private UnitSelection m_unitSelection;

    private void OnEnable()
    {
        m_mouseResolver.OnHoverEnter += OnHoverEnter;
        m_mouseResolver.OnHoverExit += OnHoverExit;
        m_mouseResolver.OnSelect += OnSelect;
        m_mouseResolver.OnDeselect += OnDeselect;
    }

    public void OnHoverEnter(ISelectable selectable)
    {
        if (this.Equals(selectable))
        {
            m_unitSelection.OnHoverEnter();
        }
    }

    public void OnHoverExit(ISelectable selectable)
    {
        if (Equals(selectable))
        {
            m_unitSelection.OnHoverExit();
        }
    }    

    public void OnSelect(ISelectable selectable)
    {
        if (Equals(selectable))
        {
            m_unitSelection.OnSelect();
        }
    }

    public void OnDeselect(ISelectable selectable)
    {
        if (Equals(selectable))
        {
            m_unitSelection.OnDeselect();
        }
    }
}
