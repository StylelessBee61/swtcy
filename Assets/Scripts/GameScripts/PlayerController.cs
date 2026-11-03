
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidad = 8f;
    public float distanciaCarril = 3f;
    public float fuerzaSalto = 8f;
    public float gravedad = -20f;

    private CharacterController controller;
    private Animator anim;
    private Vector3 movimiento;
    private int carril = 1;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Cambiar de carril
        if (Input.GetKeyDown(KeyCode.A))
            carril = Mathf.Max(0, carril - 1);

        if (Input.GetKeyDown(KeyCode.D))
            carril = Mathf.Min(2, carril + 1);

        // Salto
        if (controller.isGrounded)
        {
            movimiento.y = -2f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                movimiento.y = Mathf.Sqrt(fuerzaSalto * -2f * gravedad);

                if (anim != null)
                    anim.SetTrigger("Jump");
            }
        }

        // Gravedad
        movimiento.y += gravedad * Time.deltaTime;

        // Movimiento lateral
        float objetivoX = (carril - 1) * distanciaCarril;
        float movimientoX = (objetivoX - transform.position.x) * 5f;

        // Movimiento hacia adelante
        Vector3 movimientoFinal = new Vector3(
            movimientoX,
            movimiento.y,
            velocidad
        );

        controller.Move(movimientoFinal * Time.deltaTime);
    }
}
