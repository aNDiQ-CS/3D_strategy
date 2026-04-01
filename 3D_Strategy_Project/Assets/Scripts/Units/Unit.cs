using UnityEngine;

public class Unit : MonoBehaviour, ISelectable
{
    [SerializeField] private MouseResolver m_mouseResolver;
    [SerializeField] private UnitSelection m_unitSelection;

    private bool m_isSelected = false;

    private void OnEnable()
    {
        m_mouseResolver.OnHoverEnter += OnHoverEnter;
        m_mouseResolver.OnHoverExit += OnHoverExit;
        m_mouseResolver.OnSelect += OnSelect;
        m_mouseResolver.OnDeselect += OnDeselect;
    }

    // Ќарушение SRP - Single Responcibility
    //  ласс Unit сейчас несет ответственность за обработку наведени€ мыши, что ну так себе

    public void OnHoverEnter(ISelectable selectable)
    {
        if (this.Equals(selectable) && !m_isSelected)
        {
            m_unitSelection.OnHoverEnter();
        }
    }

    public void OnHoverExit(ISelectable selectable)
    {
        if (this.Equals(selectable) && !m_isSelected)
        {
            m_unitSelection.OnHoverExit();
        }
    }

    public void OnSelect(ISelectable selectable)
    {
        if (this.Equals(selectable))
        {
            m_unitSelection.OnSelect();
            m_isSelected = true;
        }
    }

    public void OnDeselect(ISelectable selectable)
    {
        if (this.Equals(selectable))
        {
            m_unitSelection.OnDeselect();
            m_isSelected = false;
        }
    }
}
