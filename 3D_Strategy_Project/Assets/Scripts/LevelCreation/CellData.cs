using UnityEngine;

[CreateAssetMenu(fileName = "CellData", menuName = "Scriptable Objects/CellData")]
public class CellData : ScriptableObject
{
    [SerializeField] private GameObject m_cellPrefab;
    [SerializeField] private GameObject m_selectedCellPrefab;
    [SerializeField] private bool m_isWalkable = true;
    [SerializeField] private float m_movementCost = 1f;    

    public GameObject cellPrefab => m_cellPrefab;
    public GameObject selectedCellPrefab => m_selectedCellPrefab;
    public bool isWalkable => m_isWalkable;
    public float movementCost => m_movementCost;
}
