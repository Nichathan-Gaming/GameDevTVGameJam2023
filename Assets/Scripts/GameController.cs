using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    internal static GameController instance;

    internal bool isGameActive = true;
    internal float gameTimer = 0;

    private void Awake()
    {
        //convert to DontDestroyOnLoad if we make this multi-scene
        instance = this;
    }

    private void Update()
    {
        if (isGameActive) gameTimer += Time.deltaTime;
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
}
