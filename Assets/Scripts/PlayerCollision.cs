
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private bool isDead = false;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (isDead) return;

        if (!hit.gameObject.CompareTag("Obstacle"))
            return;

        if (hit.normal.y > 0.5f)
            return;

        Die();
    }

    void Die()
    {
        isDead = true;

        if (controller != null)
            controller.enabled = false;

        UIManager.Instance.ShowWindow("GameOverUI");

        Time.timeScale = 0f;
    }
}
