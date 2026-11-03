using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    public GameObject roadSection;
    public Transform spawnPoint;

    private bool spawned = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (spawned) return;

        spawned = true;

        Instantiate(
            roadSection,
            spawnPoint.position,
            spawnPoint.rotation
        );

        // destruir piso viejo
        Destroy(transform.root.gameObject, 8f);
    }
}