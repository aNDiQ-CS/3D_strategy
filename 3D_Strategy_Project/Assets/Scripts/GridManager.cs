using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    public int width = 10;
    public int height = 10;
    public float cellSize = 1f;          // Размер клетки в мировых единицах
    public GameObject cellPrefab;         // Префаб клетки с компонентом Cell

    private Cell[,] grid;

    void Awake()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        grid = new Cell[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 worldPos = new Vector3(x * cellSize, 0, y * cellSize);
                GameObject cellObj = Instantiate(cellPrefab, worldPos, Quaternion.identity, transform);
                cellObj.name = $"Cell_{x}_{y}";

                Cell cell = cellObj.GetComponent<Cell>();
                cell.coordinates = new Vector2Int(x, y);
                grid[x, y] = cell;
            }
        }
    }

    // Получить клетку по координатам
    public Cell GetCell(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
            return grid[x, y];
        return null;
    }

    // Получить клетку по мировой позиции (луч из камеры)
    public Cell GetCellFromWorldPos(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt(worldPos.x / cellSize);
        int y = Mathf.FloorToInt(worldPos.z / cellSize); // используем Z, т.к. плоскость XZ
        return GetCell(x, y);
    }

    // Получить соседние клетки (4 направления)
    public List<Cell> GetNeighbors(Cell cell)
    {
        List<Cell> neighbors = new List<Cell>();
        Vector2Int[] dirs = {
            new Vector2Int(1, 0), new Vector2Int(-1, 0),
            new Vector2Int(0, 1), new Vector2Int(0, -1)
        };

        foreach (Vector2Int dir in dirs)
        {
            Cell neighbor = GetCell(cell.coordinates.x + dir.x, cell.coordinates.y + dir.y);
            if (neighbor != null)
                neighbors.Add(neighbor);
        }
        return neighbors;
    }

    // Найти все клетки, достижимые за указанное количество очков действия (BFS)
    public List<Cell> GetReachableCells(Cell startCell, int actionPoints)
    {
        Dictionary<Cell, int> costSoFar = new Dictionary<Cell, int>();
        Queue<Cell> frontier = new Queue<Cell>();

        costSoFar[startCell] = 0;
        frontier.Enqueue(startCell);

        while (frontier.Count > 0)
        {
            Cell current = frontier.Dequeue();

            foreach (Cell neighbor in GetNeighbors(current))
            {
                if (!neighbor.isWalkable || neighbor.isOccupied)
                    continue;

                int newCost = costSoFar[current] + (int)neighbor.movementCost;
                if (newCost <= actionPoints && (!costSoFar.ContainsKey(neighbor) || newCost < costSoFar[neighbor]))
                {
                    costSoFar[neighbor] = newCost;
                    frontier.Enqueue(neighbor);
                }
            }
        }

        return new List<Cell>(costSoFar.Keys);
    }

    // Построить путь от start до target с учётом ограничения по очкам действия
    public List<Cell> GetPath(Cell start, Cell target, int actionPoints)
    {
        Dictionary<Cell, Cell> cameFrom = new Dictionary<Cell, Cell>();
        Dictionary<Cell, int> costSoFar = new Dictionary<Cell, int>();
        Queue<Cell> frontier = new Queue<Cell>();

        cameFrom[start] = null;
        costSoFar[start] = 0;
        frontier.Enqueue(start);

        while (frontier.Count > 0)
        {
            Cell current = frontier.Dequeue();
            if (current == target) break;

            foreach (Cell neighbor in GetNeighbors(current))
            {
                if (!neighbor.isWalkable || neighbor.isOccupied)
                    continue;

                int newCost = costSoFar[current] + (int)neighbor.movementCost;
                if (newCost <= actionPoints && (!costSoFar.ContainsKey(neighbor) || newCost < costSoFar[neighbor]))
                {
                    costSoFar[neighbor] = newCost;
                    cameFrom[neighbor] = current;
                    frontier.Enqueue(neighbor);
                }
            }
        }

        // Если целевая клетка не достигнута
        if (!cameFrom.ContainsKey(target))
            return null;

        // Восстанавливаем путь
        List<Cell> path = new List<Cell>();
        Cell step = target;
        while (step != null)
        {
            path.Add(step);
            step = cameFrom[step];
        }
        path.Reverse();
        return path;
    }

    // Вспомогательный метод для подсветки списка клеток
    public void HighlightCells(List<Cell> cells, bool state)
    {
        if (cells == null) return;
        foreach (Cell cell in cells)
        {
            cell.SetHighlight(state);
        }
    }
}