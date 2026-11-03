using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] obstaclePrefabs;

    [Header("Carriles")]
    public float[] laneX = { -3f, 0f, 3f };

    [Header("Configuración")]
    public int rows = 8;

    [Header("Monedas")]
    public GameObject coinPrefab;
    public float coinHeight = 2f;
    public float coinChance = 0.6f;

    public float startZ = 15f;
    public float distanceBetweenRows = 12f;

    private void Start()
    {
        SpawnObstacles();
    }

    void SpawnObstacles()
    {
        if (obstaclePrefabs.Length == 0)
        {
            Debug.LogWarning("No hay obstáculos.");
            return;
        }

        for (int row = 0; row < rows; row++)
        {
            float zPos =
                transform.position.z +
                startZ +
                (row * distanceBetweenRows);

            int obstaclesInRow = Random.Range(1, 3);

            bool[] usedLanes = new bool[laneX.Length];

            for (int i = 0; i < obstaclesInRow; i++)
            {
                int randomLane;

                do
                {
                    randomLane = Random.Range(0, laneX.Length);
                }
                while (usedLanes[randomLane]);

                usedLanes[randomLane] = true;

                Vector3 spawnPos = new Vector3(
                    laneX[randomLane],
                    0.5f,
                    zPos
                );

                int randomObstacle =
                    Random.Range(0, obstaclePrefabs.Length);

                GameObject obstacle = Instantiate(
                    obstaclePrefabs[randomObstacle],
                    spawnPos,
                    Quaternion.identity,
                    transform
                );

                // Moneda encima del obstáculo
                if (coinPrefab != null && Random.value < coinChance)
                {
                    Instantiate(
                        coinPrefab,
                        spawnPos + new Vector3(0, coinHeight, 0),
                        Quaternion.identity,
                        transform
                    );
                }
            }
        }
    }
}
