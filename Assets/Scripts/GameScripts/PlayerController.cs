

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 8f;
    public float velocidadMaxima = 20f;
    public float incrementoVelocidad = 0.2f;

    [Header("Carriles")]
    public float distanciaCarril = 3f;
    public float velocidadCambioCarril = 10f;

    [Header("Salto")]
    public float fuerzaSalto = 8f;
    public float gravedad = -20f;

    private CharacterController controller;
    private Animator anim;

    private Vector3 movimiento;
    private float velocidadX;
    private int carril = 1;

    private bool saltando = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.Instance != null &&
            (GameManager.Instance.IsGameOver ||
             GameManager.Instance.IsPaused))
        {
            return;
        }

        // Aumentar velocidad progresivamente
        velocidad = Mathf.Min(
            velocidad + incrementoVelocidad * Time.deltaTime,
            velocidadMaxima
        );

        // Cambiar de carril
        if (Input.GetKeyDown(KeyCode.A))
            carril = Mathf.Max(0, carril - 1);

        if (Input.GetKeyDown(KeyCode.D))
            carril = Mathf.Min(2, carril + 1);

        // Salto
        if (controller.isGrounded)
        {
            movimiento.y = -2f;
            saltando = false;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                movimiento.y = Mathf.Sqrt(
                    fuerzaSalto * -2f * gravedad
                );

                saltando = true;

                if (anim != null)
                    anim.SetTrigger("Jump");
            }
        }

        // Gravedad
        movimiento.y += gravedad * Time.deltaTime;

        // Movimiento lateral suave
        float objetivoX = (carril - 1) * distanciaCarril;

        float nuevaX = Mathf.SmoothDamp(
            transform.position.x,
            objetivoX,
            ref velocidadX,
            1f / velocidadCambioCarril
        );

        float movimientoX = nuevaX - transform.position.x;

        Vector3 movimientoFinal = new Vector3(
            movimientoX,
            movimiento.y * Time.deltaTime,
            velocidad * Time.deltaTime
        );

        controller.Move(movimientoFinal);
    }
}
