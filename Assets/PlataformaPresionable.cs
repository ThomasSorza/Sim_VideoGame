using UnityEngine;

public class PlataformaPresionable : MonoBehaviour
{
    public float velocidadBajada = 5f;
    public float alturaBajada = 2f;

    private bool estaPresionada = false;
    private Vector3 posicionInicial;

    void Start()
    {
        // Guardar la posición inicial
        posicionInicial = transform.position;
    }

    void Update()
    {
        if (estaPresionada)
        {
            // Mover la plataforma hacia abajo
            transform.Translate(Vector3.down * velocidadBajada * Time.deltaTime);

            // Verificar si la plataforma ha bajado lo suficiente
            if (transform.position.y <= posicionInicial.y - alturaBajada)
            {
                estaPresionada = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el jugador ha colisionado con la plataforma
        if (other.CompareTag("Player"))
        {
            // Marcar la plataforma como presionada
            estaPresionada = true;

            Debug.Log("Plataforma presionada");
        }
    }
}
