using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseScreen;
    public string mainMenuScene;
    private bool isPaused = false;

    void Start()
    {
        pauseScreen.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void PauseUnpause()
    {
        if (!isPaused && GameObject.Find("UI/Canvas/VinShop").activeSelf == false)
        {
            pauseScreen.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.SetActive(false);
            isPaused = false;
            Time.timeScale = 1f;
        }
    }

    public void Resume()
    {
        pauseScreen.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
        Time.timeScale = 1f;
    }
}
