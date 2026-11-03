using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [Header("Moneda")]
    public GameObject coinPrefab;

    [Header("Carriles")]
    public float[] laneX = { -3f, 0f, 3f };

    [Header("Configuración")]
    public int rows = 8;

    public float startZ = 10f;
    public float distanceBetweenRows = 12f;

    [Header("Líneas de monedas")]
    public int minCoinsInLine = 3;
    public int maxCoinsInLine = 8;

    private void Start()
    {
        SpawnCoins();
    }

    void SpawnCoins()
    {
        if (coinPrefab == null)
        {
            Debug.LogWarning("No hay moneda asignada.");
            return;
        }

        for (int row = 0; row < rows; row++)
        {
            // Carril aleatorio
            int randomLane = Random.Range(0, laneX.Length);

            // Cantidad de monedas en esta línea
            int coinsInLine =
                Random.Range(minCoinsInLine, maxCoinsInLine + 1);

            float rowZ =
                transform.position.z +
                startZ +
                (row * distanceBetweenRows);

            for (int i = 0; i < coinsInLine; i++)
            {
                Vector3 spawnPos = new Vector3(
                    laneX[randomLane],
                    1f,
                    rowZ + (i * 2f)
                );

                Instantiate(
                    coinPrefab,
                    spawnPos,
                    Quaternion.identity,
                    transform
                );
            }
        }
    }
}