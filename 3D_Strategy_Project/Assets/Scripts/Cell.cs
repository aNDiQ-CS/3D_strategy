using UnityEngine;

public class Cell : MonoBehaviour
{
    [Header("Grid Info")]
    public Vector2Int coordinates;      // Координаты в сетке
    public float movementCost = 1f;     // Стоимость прохождения (1 – обычная)
    public bool isWalkable = true;       // Можно ли вообще ходить
    public bool isOccupied = false;      // Занята ли другим юнитом

    [Header("Visual")]
    public GameObject highlightObject;   // Дочерний объект для подсветки (например, плоскость с материалом)

    // Для удобства можно добавить метод включения/выключения подсветки
    public void SetHighlight(bool state)
    {
        if (highlightObject != null)
            highlightObject.SetActive(state);
    }
}