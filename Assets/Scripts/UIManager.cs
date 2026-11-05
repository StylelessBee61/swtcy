
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private List<UIWindow> _uiWindows;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private IEnumerator Start()
    {
        yield return null;

        foreach (UIWindow window in _uiWindows)
        {
            if (window != null)
                window.Hide(true);
        }

        if (GameManager.Instance != null &&
            GameManager.Instance.IsPaused)
        {
            Debug.Log("MOSTRANDO PAUSE UI");
            ShowWindow("PauseUI");
        }
        else if (GameManager.WasGameOver)
        {
            ShowWindow("GameOverUI");
        }
        else
        {
            ShowWindow("MenuUI");
        }
    }

    public void ShowWindow(string windowName)
    {
        foreach (UIWindow window in _uiWindows)
        {
            if (window != null && window.WindowID == windowName)
            {
                window.Show();
                return;
            }
        }

        Debug.LogWarning("No se encontró la ventana: " + windowName);
    }

    

    public void HideWindow(string windowName)
    {
        foreach (UIWindow window in _uiWindows)
        {
            if (window != null && window.WindowID == windowName)
            {
                window.Hide();
                return;
            }
        }
    }
}
