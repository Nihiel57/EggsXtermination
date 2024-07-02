using UnityEngine;

public class Health : MonoBehaviour
{
    public int vidaMaxima = 100;
    private int vidaActual;

    private void Start()
    {
        vidaActual = vidaMaxima;
    }

    public void TakeDamage(int cantidadDaño)
    {
        vidaActual -= cantidadDaño;

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        // Implementa la lógica de muerte del personaje aquí
        Destroy(gameObject);
    }
}