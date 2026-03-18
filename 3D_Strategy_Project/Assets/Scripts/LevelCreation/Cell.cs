using UnityEngine;

public class Cell : MonoBehaviour
{
    [Header("Current cell data")]
    [SerializeField] private CellData m_cellData;
    
    private Vector2Int m_coords;
    private bool m_isOccupied = false;

    // Нарушение инкапсуляции
    public Vector2Int coords { 
        get 
        { return m_coords; }
        set 
        { m_coords = value; }
    }
}
