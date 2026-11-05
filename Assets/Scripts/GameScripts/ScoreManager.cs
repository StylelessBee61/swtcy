
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text coinText;

    [Header("Puntuación")]
    public float puntosPorSegundo = 10f;

    public int coins { get; private set; }
    public float score { get; private set; }

    private bool detenido = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        if (detenido) return;

        if (GameManager.Instance != null &&
            (GameManager.Instance.IsGameOver ||
             GameManager.Instance.IsPaused))
        {
            return;
        }

        score += puntosPorSegundo * Time.deltaTime;

        UpdateUI();
    }

    public void AddCoin()
    {
        if (detenido) return;

        coins++;
        UpdateUI();
    }

    public void StopScore()
    {
        detenido = true;
    }

    public void ResetScore()
    {
        score = 0f;
        coins = 0;
        detenido = false;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Puntos: " + Mathf.FloorToInt(score);

        if (coinText != null)
            coinText.text = "Monedas: " + coins;
    }
}
