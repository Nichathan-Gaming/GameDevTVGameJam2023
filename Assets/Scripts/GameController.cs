using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    internal static GameController instance;

    internal bool isGameActive = false;
    internal float gameTimer = 0;

    [SerializeField] TMP_Text TimeCounter;
    [SerializeField] TMP_Text KillsCounter;
    int kills=0;

    [SerializeField] GameObject menu;
    [SerializeField] GameObject display;

    private void Awake()
    {
        //convert to DontDestroyOnLoad if we make this multi-scene
        instance = this;
    }

    private void Update()
    {
        if (isGameActive) UpdateTimer();

        if (Input.GetKeyDown(KeyCode.Escape)) ToggleGame();
    }

    public void ToggleGame()
    {
        AudioManager.PlaySFX(SoundEffects.Click);
        isGameActive = !isGameActive;
        menu.SetActive(!isGameActive);
        display.SetActive(isGameActive);
    }

    public void QuitGame()
    {
        AudioManager.PlaySFX(SoundEffects.Click);
        Application.Quit();
    }

    public void ToggleControls(GameObject obj)
    {
        AudioManager.PlaySFX(SoundEffects.Click);
        obj.SetActive(!obj.activeInHierarchy);
    }

    internal void SetPlayerDeath()
    {
        AudioManager.PlaySFX(SoundEffects.Die0);
        SceneManager.LoadScene("GameScene");
    }

    public void UpdateTimer()
    {
        gameTimer += Time.deltaTime;
        int minutes = (int) gameTimer / 60;
        string seconds = "" + ((int) gameTimer - (minutes * 60));
        seconds = (seconds.Length < 2 ? "0" : "") + seconds;
        TimeCounter.text = $"Time: { minutes }:{ seconds }";
    }

    internal void UpdateKills()
    {
        AudioManager.PlaySFX((SoundEffects)UnityEngine.Random.Range(0,3));
        KillsCounter.text = $"Kills : {++kills}";
    }
}
