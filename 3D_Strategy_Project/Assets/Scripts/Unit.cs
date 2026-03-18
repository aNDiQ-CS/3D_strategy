using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Unit Stats")]
    public int maxActionPoints = 6;
    public int currentActionPoints;

    [Header("Movement")]
    public float moveSpeed = 5f;          // Скорость перемещения (единиц/сек)
    public Cell currentCell;               // Текущая клетка, на которой стоит юнит

    private bool isMoving = false;
    private GridManager gridManager;

    void Start()
    {
        // Находим GridManager на сцене
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager == null)
        {
            Debug.LogError("GridManager не найден на сцене!");
        }

        // При старте занимаем клетку, на которой стоит юнит
        if (currentCell != null)
        {
            currentCell.isOccupied = true;
        }

        // Восстанавливаем очки действия (можно вызывать при начале хода)
        currentActionPoints = maxActionPoints;
    }

    // Запуск перемещения к целевой клетке
    public void MoveToCell(Cell targetCell)
    {
        if (isMoving) return;

        // Получаем путь от текущей клетки до целевой с учётом оставшихся AP
        List<Cell> path = gridManager.GetPath(currentCell, targetCell, currentActionPoints);
        if (path == null || path.Count == 0)
        {
            Debug.Log("Нет доступного пути");
            return;
        }

        // Вычитаем стоимость пути из очков действия
        int pathCost = 0;
        foreach (Cell cell in path)
        {
            if (cell != currentCell) // первую клетку (текущую) не учитываем
                pathCost += (int)cell.movementCost;
        }
        if (pathCost > currentActionPoints)
        {
            Debug.LogWarning("Не хватает AP для этого пути");
            return;
        }

        currentActionPoints -= pathCost;
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<Cell> path)
    {
        isMoving = true;

        // Освобождаем текущую клетку
        if (currentCell != null)
            currentCell.isOccupied = false;

        // Проходим по всем клеткам пути (кроме последней, но её тоже займём)
        for (int i = 1; i < path.Count; i++) // i=1 потому что первая клетка в пути – это текущая
        {
            Cell targetCell = path[i];
            Vector3 targetPos = targetCell.transform.position;
            targetPos.y = transform.position.y; // сохраняем высоту юнита

            // Поворачиваемся лицом к направлению движения (опционально)
            Vector3 direction = (targetPos - transform.position).normalized;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(direction);

            // Плавно двигаемся к центру следующей клетки
            while (Vector3.Distance(transform.position, targetPos) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }

            // Занимаем текущую клетку (если это не последняя, но для корректности займём только последнюю)
            // Можно занимать каждую промежуточную, но тогда другие юниты не смогут войти, что нежелательно.
            // Лучше занимать только конечную клетку, а промежуточные освобождать сразу.
        }

        // Занимаем конечную клетку
        Cell finalCell = path[path.Count - 1];
        finalCell.isOccupied = true;
        currentCell = finalCell;

        isMoving = false;

        // Здесь можно вызвать событие окончания движения (например, для завершения хода)
        Debug.Log($"{name} закончил движение. Осталось AP: {currentActionPoints}");
    }

    // Сброс очков действия (вызывать при начале хода)
    public void ResetActionPoints()
    {
        currentActionPoints = maxActionPoints;
    }
}