using UnityEngine;
using UnityEngine.UI;

public class Temporizador : MonoBehaviour
{
    public float tiempoInicial = 300f; // Tiempo inicial en segundos
    private float tiempoRestante; // Tiempo restante actual
    private Text textoTemporizador; // Referencia al componente Text para mostrar el temporizador

    void Start()
    {
        tiempoRestante = tiempoInicial;
        textoTemporizador = GetComponent<Text>();
    }

    void Update()
    {
        tiempoRestante -= Time.deltaTime;

        if (tiempoRestante <= 0f)
        {
            tiempoRestante = 0f;
            // Aquí puedes agregar lógica adicional cuando el temporizador llegue a cero
        }

        ActualizarUI();
    }

    void ActualizarUI()
    {
        // Formatear el tiempo restante en minutos y segundos
        int minutos = Mathf.FloorToInt(tiempoRestante / 60);
        int segundos = Mathf.FloorToInt(tiempoRestante % 60);

        // Actualizar el texto mostrado en el componente Text
        textoTemporizador.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }
}
