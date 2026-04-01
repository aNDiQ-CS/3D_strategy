using System;
using UnityEngine;
using UnityEngine.UI;

public class MouseResolver : MonoBehaviour
{
    public event Action<ISelectable> OnHoverEnter;
    public event Action<ISelectable> OnHoverExit;
    public event Action<ISelectable> OnSelect;
    public event Action<ISelectable> OnDeselect;

    private ISelectable m_lastHoveredObject;
    private ISelectable m_lastSelectableObject;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            //Debug.Log("Hit smth");            
            //if (hit.transform.TryGetComponent(out ISelectable selectable))
            //{
            //    Debug.Log(selectable);
            //    OnHoverEnter?.Invoke(selectable);
            //}

            ISelectable selectable = hit.transform.GetComponentInParent<ISelectable>();

            if (selectable != m_lastHoveredObject)
            {
                OnHoverEnter?.Invoke(selectable);
                OnHoverExit?.Invoke(m_lastHoveredObject);
                m_lastHoveredObject = selectable;
            }            
            OnHoverEnter?.Invoke(selectable);

            if (Input.GetMouseButtonDown(0))
            {
                OnSelect?.Invoke(selectable);
                OnDeselect?.Invoke(m_lastSelectableObject);
                m_lastSelectableObject = selectable;
            }
        }
        else
        {
            OnHoverExit?.Invoke(m_lastHoveredObject);
            m_lastHoveredObject = null;
        }
    }
}
