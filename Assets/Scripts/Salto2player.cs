using UnityEngine;

public class SecondPlayerJump : MonoBehaviour
{
    public float jumpForce = 5f; // Fuerza del salto
    public LayerMask groundLayer; // Capa del suelo para detección
    public Transform feetTransform; // Transform del punto de los pies del jugador
    public float checkRadius = 0.2f; // Radio para verificar el suelo

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Verificar si el jugador está tocando el suelo
        isGrounded = Physics2D.OverlapCircle(feetTransform.position, checkRadius, groundLayer);

        // Saltar cuando se presiona la tecla Keypad0 y el jugador está en el suelo
        if (Input.GetKeyDown(KeyCode.Keypad0) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Dibujar un círculo en la posición de los pies para visualizar la detección del suelo
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(feetTransform.position, checkRadius);
    }
}