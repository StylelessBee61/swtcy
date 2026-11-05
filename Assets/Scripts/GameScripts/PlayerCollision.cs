
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private bool isDead;

    private void OnControllerColliderHit(
        ControllerColliderHit hit)
    {
        // Ignorar objetos que no sean obstáculos
        if (!hit.collider.CompareTag("Obstacle"))
            return;

        // Dirección del impacto
        float direccionImpacto = Vector3.Dot(
            hit.normal,
            transform.forward
        );

        // Solo morir si el impacto es frontal
        if (direccionImpacto < -0.5f)
        {
            Morir();
        }
    }


    private void Morir()
    {
        if (isDead)
            return;

        isDead = true;

        Debug.Log("¡GAME OVER!");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
        else
        {
            Debug.LogError("No se encontró GameManager.Instance");
        }
    }

}
