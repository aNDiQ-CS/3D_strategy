using UnityEngine;

public interface ISelectable
{
    public void OnHoverEnter(ISelectable selectable);
    public void OnHoverExit();
    public void OnSelect();
}
