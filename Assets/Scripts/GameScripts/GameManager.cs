
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static bool WasGameOver { get; private set; }
    public static string CurrentUIMode { get; private set; } = "Menu";

    public bool IsGameOver { get; private set; }
    public bool IsPaused { get; private set; }

    [Header("Nombres de escenas")]
    [SerializeField] private string gameSceneName = "Game";
    [SerializeField] private string uiSceneName = "UI";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        CurrentUIMode = "Game";
        IsGameOver = false;
        IsPaused = false;

        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void Pause()
    {
        if (IsGameOver || IsPaused) return;

        IsPaused = true;
        CurrentUIMode = "Pause";
        Time.timeScale = 0f;

        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        Debug.Log("PAUSA ACTIVADA");
        Debug.Log("Modo UI: " + CurrentUIMode);
    }

    public void Resume()
    {
        if (!IsPaused || IsGameOver) return;

        IsPaused = false;
        CurrentUIMode = "Game";
        Time.timeScale = 1f;

        SceneManager.UnloadSceneAsync("UI");
    }

    public void GameOver()
    {
        if (IsGameOver) return;

        IsGameOver = true;
        WasGameOver = true;
        IsPaused = false;

        CurrentUIMode = "GameOver";

        Time.timeScale = 1f;

        SceneManager.LoadScene(uiSceneName);
    }

    public void Retry()
    {
        WasGameOver = false;
        IsPaused = false;
        Time.timeScale = 1f;

        SceneManager.LoadScene(gameSceneName);
    }

    public void BackToMenu()
    {
        WasGameOver = false;
        IsPaused = false;
        Time.timeScale = 1f;

        SceneManager.LoadScene(uiSceneName);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }


    public void TogglePause()
    {
        if (IsGameOver) return;

        if (IsPaused)
            Resume();
        else
            Pause();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != gameSceneName)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

}
