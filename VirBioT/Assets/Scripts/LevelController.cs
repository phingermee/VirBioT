using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;


public class LevelController : MonoBehaviour
{
    public static LevelController instance = null;
    public int sceneIndex;
    private int lvlCompleted;
    private string playStoreId = "3873797";
    private string appStoreId = "3873796";

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        lvlCompleted = PlayerPrefs.GetInt("LevelComplete");

        if (Advertisement.isSupported)
        {
            Advertisement.Initialize(playStoreId, false);
        }
    }

    public void isEndGame()
    {
        if (sceneIndex == 3)
        {
            LoadMainMenu();
        }
        else
        {
            if (lvlCompleted < sceneIndex)
            {
                PlayerPrefs.SetInt("LevelComplete", sceneIndex);       
            }
            NextLvl();
        }
    }

    public void NextLvl()
    {
        SceneManager.LoadScene(sceneIndex + 1);

        if (Advertisement.IsReady())
        {
            Advertisement.Show("video");
        }

    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
    
}
