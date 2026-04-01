using UnityEngine;

public interface ISelectable
{
    public void OnHoverEnter(ISelectable selectable);
    public void OnHoverExit(ISelectable selectable);
    public void OnSelect(ISelectable selectable);
    public void OnDeselect(ISelectable selectable);
}
