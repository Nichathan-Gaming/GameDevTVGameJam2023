using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    internal static GameController instance;

    internal bool isGameActive = true;
    internal float gameTimer = 0;

    [SerializeField] TMP_Text TimeCounter;

    private void Awake()
    {
        //convert to DontDestroyOnLoad if we make this multi-scene
        instance = this;
    }

    private void Update()
    {
        if (isGameActive) UpdateTimer();
    }

    public void StartGame()
    {
        isGameActive = true;
    }

    internal void SetPlayerDeath()
    {
        isGameActive = false;
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
}
