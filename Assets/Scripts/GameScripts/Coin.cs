
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (ScoreManager.Instance != null)
            ScoreManager.Instance.AddCoin();

        Destroy(gameObject);
    }
}
