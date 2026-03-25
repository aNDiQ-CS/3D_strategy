using UnityEngine;

public class Unit : MonoBehaviour, ISelectable
{
    [SerializeField] private MouseResolver m_mouseResolver;
    [SerializeField] private UnitSelection m_unitSelection;

    private void OnEnable()
    {
        m_mouseResolver.OnHoverEnter += OnHoverEnter;
    }

    public void OnHoverEnter(ISelectable selectable)
    {
        if (this.Equals(selectable))
        {
            m_unitSelection.OnHoverEnter();
        }
    }

    public void OnHoverExit()
    {
        throw new System.NotImplementedException();
    }

    public void OnSelect()
    {
        throw new System.NotImplementedException();
    }    
}
