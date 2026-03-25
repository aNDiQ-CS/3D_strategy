using UnityEngine;

public class Unit : MonoBehaviour, Selectable
{
    [SerializeField] private UnitSelection m_unitSelection;

    private void OnEnable()
    {
        
    }

    private void Update()
    {
        
    }

    public void SubscribeToMouse(MouseHover mouseHover)
    {
        mouseHover.Hover += CheckHoverState;
        mouseHover.Select += CheckSelectionStatus;
    }

    private void CheckHoverState(Selectable selectable)
    {

    }

    private void CheckSelectionStatus(Selectable selectable)
    {

    }
}
