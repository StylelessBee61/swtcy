
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] obstaclePrefabs;
    public GameObject coinPrefab;

    [Header("Carriles")]
    public float[] laneX = { -3f, 0f, 3f };

    [Header("Obstáculos")]
    public int rows = 8;
    public float startZ = 15f;
    public float distanceBetweenRows = 12f;

    [Header("Monedas")]
    public float coinHeight = 2f;
    [Range(0f, 1f)]
    public float coinChance = 0.6f;

    private void Start()
    {
        SpawnObstacles();
    }

    private void SpawnObstacles()
    {
        if (obstaclePrefabs == null ||
            obstaclePrefabs.Length == 0)
        {
            Debug.LogWarning("No hay obstáculos asignados.");
            return;
        }

        for (int row = 0; row < rows; row++)
        {
            float zPos = transform.position.z +
                         startZ +
                         row * distanceBetweenRows;

            // Solo 1 obstáculo por fila:
            // siempre queda una ruta libre.
            int randomLane = Random.Range(0, laneX.Length);

            Vector3 spawnPos = new Vector3(
                laneX[randomLane],
                0.5f,
                zPos
            );

            int randomObstacle = Random.Range(
                0,
                obstaclePrefabs.Length
            );

            Instantiate(
                obstaclePrefabs[randomObstacle],
                spawnPos,
                Quaternion.identity,
                transform
            );

            // Moneda encima del obstáculo
            if (coinPrefab != null &&
                Random.value < coinChance)
            {
                Instantiate(
                    coinPrefab,
                    spawnPos + new Vector3(0f, coinHeight, 0f),
                    Quaternion.identity,
                    transform
                );
            }
        }
    }
}
