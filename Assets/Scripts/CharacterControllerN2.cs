using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCharacterController : MonoBehaviour
{
    [Header("Movimiento")]
    private float movimientohorizontal = 0f;
    [SerializeField] private float VelocidadDeMovimiento;
    [SerializeField] private float SuavizadoDeMovimiento;
    private Vector3 velocidad = Vector3.zero;
    [SerializeField] private bool mirandoDerechaInicial = false; // Inicialmente mirando a la izquierda
    private bool mirandoderecha;

    private Rigidbody2D rb2D;

    [Header("Salto")]
    [SerializeField] private float fuerzaDeSalto;
    [SerializeField] private LayerMask QueEsSuelo;
    [SerializeField] private Transform ControladorSuelo;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] private bool enSuelo;

    private bool salto = false;

    [Header("Animación")]
    private Animator animator;

    [SerializeField]
    private AudioClip saltoSonido;

    [Header("Ataque")]
    [SerializeField] private Transform puntoDeAtaque;
    [SerializeField] private float rangoDeAtaque = 0.5f;
    [SerializeField] private LayerMask capasDeEnemigos;
    [SerializeField] private int dañoDeAtaque = 10;
    public float fuerzaRetroceso = 5f;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mirandoderecha = mirandoDerechaInicial;  // Establecer la dirección inicial

        // Girar el personaje si no está mirando a la derecha
        if (!mirandoderecha)
        {
            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }
    }

    private void Update()
    {
        movimientohorizontal = Input.GetAxisRaw("Horizontal2") * VelocidadDeMovimiento;

        // Código del salto
        if (Input.GetButtonDown("Jump2"))
        {
            salto = true;
        }

        // Código del ataque
        if (Input.GetButtonDown("Attack2"))
        {
            EjecutarAtaque();
        }

        // Configurar los parámetros del animador
        animator.SetFloat("Horizontal", movimientohorizontal);
        animator.SetBool("EnSuelo", enSuelo);
        animator.SetFloat("VelocidadY", rb2D.velocity.x);
    }

    private void EjecutarAtaque()
    {
        // Reproducir animación de ataque
        GetComponent<Animator>().SetTrigger("Ataque");

        // Detectar enemigos en rango de ataque
        Collider2D[] enemigosGolpeados = Physics2D.OverlapCircleAll(puntoDeAtaque.position, rangoDeAtaque, capasDeEnemigos);

        // Aplicar daño y retroceso a los enemigos
        foreach (Collider2D enemigo in enemigosGolpeados)
        {
            enemigo.GetComponent<Health>().TakeDamage(dañoDeAtaque);
            AplicarRetroceso(enemigo);
        }
    }

    private void AplicarRetroceso(Collider2D enemigo)
    {
        Rigidbody2D rbEnemigo = enemigo.GetComponent<Rigidbody2D>();
        if (rbEnemigo != null)
        {
            Vector2 direccionRetroceso = (enemigo.transform.position - transform.position).normalized;
            rbEnemigo.AddForce(direccionRetroceso * fuerzaRetroceso, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        // Código de salto
        enSuelo = Physics2D.OverlapBox(ControladorSuelo.position, dimensionesCaja, 0f, QueEsSuelo);

        // Mover
        Mover(movimientohorizontal * Time.fixedDeltaTime, salto);

        salto = false;
    }

    private void Mover(float Mover, bool saltar)
    {
        Vector3 velocidadObjeto = new Vector2(Mover, rb2D.velocity.y);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, velocidadObjeto, ref velocidad, SuavizadoDeMovimiento);

        if (Mover > 0 && !mirandoderecha)
        {
            // Girar
            Girar();
        }
        else if (Mover < 0 && mirandoderecha)
        {
            // Girar
            Girar();
        }

        if (enSuelo && saltar)
        {
            enSuelo = false;
            rb2D.AddForce(new Vector2(0f, fuerzaDeSalto));
        }
    }

    private void Girar()
    {
        mirandoderecha = !mirandoderecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private void Atacar()
    {
        // Detectar enemigos en el rango de ataque
        Collider2D[] enemigosGolpeados = Physics2D.OverlapCircleAll(puntoDeAtaque.position, rangoDeAtaque, capasDeEnemigos);

        // Aplicar daño a los enemigos
        foreach (Collider2D enemigo in enemigosGolpeados)
        {
            enemigo.GetComponent<Health>().TakeDamage(dañoDeAtaque);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(ControladorSuelo.position, dimensionesCaja);

        if (puntoDeAtaque == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(puntoDeAtaque.position, rangoDeAtaque);
    }
}