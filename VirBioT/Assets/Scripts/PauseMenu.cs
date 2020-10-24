using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject pauseMenu, onGameCanvas;

    public void Continue()
    {
        onGameCanvas.gameObject.SetActive(true);
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;   
    }

    public void Pause()
    {
        pauseMenu.gameObject.SetActive(true);
        onGameCanvas.gameObject.SetActive(false);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(LevelController.instance.sceneIndex);
        Time.timeScale = 1f;
    }
}
