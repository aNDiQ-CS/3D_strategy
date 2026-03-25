using System;
using UnityEngine;
using UnityEngine.UI;

public class MouseHover : MonoBehaviour
{
    public event Action<Selectable> Hover;
    public event Action<Selectable> Select;

    [SerializeField] private LayerMask m_mask;

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, float.MaxValue, m_mask))
        {
            if (hit.transform.TryGetComponent(out Unit unit))
            {
                Hover?.Invoke(unit);
                if (Input.GetMouseButtonDown(0))
                {
                    Select?.Invoke(unit);
                }
            }
        }
    }
}

