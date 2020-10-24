using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchInput : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 direction;
    [SerializeField] private GameObject trigger;
    public List<Cell> touchedCells = new List<Cell>();
    private Cell target;

    private void ClearList()
    {
        foreach (var cell in touchedCells)
        {
            cell.HighligthOff();
        }     
        touchedCells.Clear();
    }

    private void SegmentationListedCells(Cell target)
    {
        foreach (var cell in touchedCells)
        {
            cell.DivideCell(target); 
        }
    }

    public void SetTarget(Cell cell)
    {
        target = cell;
    }

    public void Attack()
    {
        if (target != null)
        {
            if (touchedCells.Count > 0)
            {
                SegmentationListedCells(target);     
            }
            target = null;
        }
    }

    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    trigger.SetActive(true);
                    startPos = touch.position;
                    Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);
                    pos.z = 0;
                    trigger.transform.position = pos;
                    break;
                
                case TouchPhase.Moved:
                    pos = Camera.main.ScreenToWorldPoint(touch.position);
                    pos.z = 0;
                    trigger.transform.position = pos;
                    break;
               
                case TouchPhase.Ended:
                    Attack();
                    trigger.SetActive(false);
                    ClearList();
                    break;
            }
        }
    }
}