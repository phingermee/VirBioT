using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public static NextLevel instance = null;

    private void StartNextLevel(Cell.Type winnerType)
    {
        if (winnerType == Cell.Type.Friend)
        {
            LevelController.instance.isEndGame();
        }
        else if(winnerType == Cell.Type.Enemy)
        {
            SceneManager.LoadScene(LevelController.instance.sceneIndex);
        }
    }

    public void CheckEndGame()
    {
        var cellsInScene = FindObjectsOfType<Cell>();
        Cell.Type baseType = cellsInScene[0].type;
        bool isEnded = true;
        foreach (var cell in cellsInScene)
        {
            if (cell.type != baseType)
            {
                isEnded = false;
                    break;
            }
        }
        if (isEnded)
        {
            StartNextLevel(baseType);
        }
    }

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
