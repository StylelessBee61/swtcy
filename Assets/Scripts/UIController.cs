
using UnityEngine;

public class UIController : MonoBehaviour
{
    private void Start()
    {
        if (UIManager.Instance == null)
        {
            Debug.LogError("No se encontró UIManager");
            return;
        }

        if (GameManager.Instance != null &&
            GameManager.Instance.IsGameOver)
        {
            Debug.Log("Mostrando GameOverUI");

            UIManager.Instance.ShowWindow("GameOverUI");
        }
        else
        {
            Debug.Log("Mostrando MenuUI");

            UIManager.Instance.ShowWindow("MenuUI");
        }
    }
}
