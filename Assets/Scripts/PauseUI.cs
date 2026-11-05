
using UnityEngine;

public class PauseUI : UIWindow
{
    public void PauseGame()
    {
        Time.timeScale = 0f;
        UIManager.Instance.ShowWindow("PauseUI");
    }

    public void ResumeGame()
    {
        UIManager.Instance.HideWindow("PauseUI");
        Time.timeScale = 1f;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}

