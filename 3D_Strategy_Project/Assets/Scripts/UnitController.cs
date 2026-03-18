using UnityEngine;
using System.Collections.Generic;

public class UnitController : MonoBehaviour
{
    private GridManager gridManager;
    private Camera mainCamera;

    private Unit selectedUnit;
    private List<Cell> reachableCells;  // Клетки, доступные для перемещения выбранному юниту

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        // ЛКМ – выбор юнита или перемещение
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Проверяем, кликнули ли по юниту
                Unit unit = hit.collider.GetComponent<Unit>();
                if (unit != null)
                {
                    SelectUnit(unit);
                    return;
                }

                // Если кликнули по клетке, и у нас есть выбранный юнит, пробуем переместить
                Cell cell = hit.collider.GetComponent<Cell>();
                if (cell != null && selectedUnit != null)
                {
                    if (reachableCells != null && reachableCells.Contains(cell))
                    {
                        selectedUnit.MoveToCell(cell);
                        // После перемещения убираем подсветку
                        gridManager.HighlightCells(reachableCells, false);
                        reachableCells = null;
                    }
                }
            }
        }

        // Отмена выбора (например, правая кнопка мыши)
        if (Input.GetMouseButtonDown(1))
        {
            DeselectUnit();
        }
    }

    void SelectUnit(Unit unit)
    {
        // Если уже выбран другой юнит, снимаем подсветку с его клеток
        if (selectedUnit != null && selectedUnit != unit)
        {
            gridManager.HighlightCells(reachableCells, false);
        }

        selectedUnit = unit;

        // Получаем доступные клетки для этого юнита
        reachableCells = gridManager.GetReachableCells(unit.currentCell, unit.currentActionPoints);
        gridManager.HighlightCells(reachableCells, true);
    }

    void DeselectUnit()
    {
        if (selectedUnit != null)
        {
            gridManager.HighlightCells(reachableCells, false);
            selectedUnit = null;
            reachableCells = null;
        }
    }
}