using UnityEngine;

public class Health : MonoBehaviour
{
    public int vidaMaxima = 100;
    private int vidaActual;

    private void Start()
    {
        vidaActual = vidaMaxima;
    }

    public void TakeDamage(int cantidadDa�o)
    {
        vidaActual -= cantidadDa�o;

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        // Implementa la l�gica de muerte del personaje aqu�
        Destroy(gameObject);
    }
}