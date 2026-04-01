using System;
using UnityEngine;
using UnityEngine.UI;

public class MouseResolver : MonoBehaviour
{
    public event Action<ISelectable> OnHoverEnter;
    public event Action<ISelectable> OnHoverExit;
    public event Action<ISelectable> OnSelect;
    public event Action<ISelectable> OnDeselect;

    private ISelectable m_lastHovered;
    private ISelectable m_lastSelected;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            ISelectable selectable = hit.transform.GetComponentInParent<ISelectable>();
            OnHoverEnter?.Invoke(selectable);
            if (selectable != m_lastHovered)
            {
                OnHoverExit?.Invoke(m_lastHovered);
                m_lastHovered = selectable;
            }

            if (Input.GetMouseButtonDown(0) && selectable != m_lastSelected)
            {
                OnDeselect?.Invoke(m_lastSelected);
                OnSelect?.Invoke(selectable);                
                m_lastSelected = selectable;
            }
        }
        else
        {
            OnHoverExit?.Invoke(m_lastHovered);
        }        
    }
}
