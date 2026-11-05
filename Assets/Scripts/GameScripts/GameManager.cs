
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsGameOver { get; private set; }
    public bool IsPaused { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        if (IsGameOver) return;

        IsPaused = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        if (IsGameOver) return;

        IsPaused = false;
        Time.timeScale = 1f;
    }



    public void GameOver()
    {
        if (IsGameOver) return;

        IsGameOver = true;
        IsPaused = false;

        Time.timeScale = 1f;

        SceneManager.LoadScene("UI");
    }



    public void Retry()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Menu");
    }
}

