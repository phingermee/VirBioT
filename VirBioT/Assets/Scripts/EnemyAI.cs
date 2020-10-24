using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    private List<Cell> controlled = new List<Cell>();
    
    void Start()
    {
        StartCoroutine(Battle());
    }

    private Cell FindTarget()
    {
        var cellsInScene = FindObjectsOfType<Cell>();
        Cell.Type baseType = Cell.Type.Enemy;
        List<Cell> targets = new List<Cell>();
        foreach (var cell in cellsInScene)
        {
            if(cell.type != baseType)
            {
                targets.Add(cell);
            }
        }
        return targets[Random.Range(0, targets.Count)];
    }

    IEnumerator Battle()
    {
        while (true)
        {
            Attack();
            yield return new WaitForSeconds(3f);
        }
    }
    
    private void Attack()
    {
        Cell target = FindTarget();
        UpdateControlCells();
        foreach (var cell in controlled)
        {
            if (cell.currentPatrticles >= target.currentPatrticles - 3 && cell.currentPatrticles>cell.maxSize/3)
            {
                cell.DivideCell(target);
            }
        }    
    }

    private void UpdateControlCells()
    {
        var cellsInScene = FindObjectsOfType<Cell>();
        Cell.Type baseType = Cell.Type.Enemy;
        foreach (var cell in cellsInScene)
        {
            if (cell.type == baseType)
            {
                controlled.Add(cell);
            }
        }        
    }
}
