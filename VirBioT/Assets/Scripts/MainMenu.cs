using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Button btnLvl2, btnLvl3;
    int levelController;

    void Start()
    {
        levelController = PlayerPrefs.GetInt("LevelComplete");
        btnLvl2.gameObject.SetActive(false);
        btnLvl3.gameObject.SetActive(false);

        switch(levelController)
        {
            case 1:
                btnLvl2.gameObject.SetActive(true);
                break;
            case 2:
                btnLvl2.gameObject.SetActive(true);
                btnLvl3.gameObject.SetActive(true);
                break;
        }
    }

    public void LoadLvl(int lvl)
    {
        SceneManager.LoadScene(lvl);
        
    }

    public void Reset()
    {
        btnLvl2.gameObject.SetActive(false);
        btnLvl3.gameObject.SetActive(false);
        PlayerPrefs.DeleteAll();
    }
}
