using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private int m_width = 10;
    [SerializeField] private int m_height = 10;
    [SerializeField] private float m_cellScale = 1f;

    // TODO: здесь пока что только можно создать только 1 блок
    //  реализовать создание нескольких типов
    [SerializeField] private CellData m_cellData;
    [SerializeField] private Cell[,] m_grid;

    private void Awake()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        m_grid = new Cell[m_width, m_height];

        for (int x = 0; x < m_width; x++)
        {
            for (int y = 0; y < m_height; y++)
            {
                Vector3 worldPos = new Vector3(x * m_cellScale, 0, y * m_cellScale);
                GameObject cellObj = Instantiate(m_cellData.cellPrefab, worldPos, Quaternion.identity, transform);
                cellObj.name = $"Cell_{x}_{y}";

                Cell cell = cellObj.GetComponent<Cell>();
                cell.coords = new Vector2Int(x, y);
                m_grid[x, y] = cell;
            }
        }
    }
}
