using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Variables de patrullaje
    public Transform[] patrolPoints;
    public float speed;
    public int patrolDestination;

    // Variables de persecución
    public Transform playerTransform;
    public bool isChasing;
    public float chaseDistance;

    private float velocidadOriginal;

    // Referencia al script de distribución uniforme
    public UniformDistributionMethod uniformDistributionScript;
    private List<float> riValues = new List<float>();

    // Ralentización variables
    private float factorDeRalentizacionActual = 1.0f; // Valor por defecto

    void Start()
    {
        // Obtener referencia al script UniformDistributionMethod
        uniformDistributionScript = GetComponent<UniformDistributionMethod>();

        // Si la referencia es válida, obtener los valores generados
        if (uniformDistributionScript != null)
        {
            riValues = uniformDistributionScript.GetRiValues();
        }

        velocidadOriginal = speed;
    }

    void Update()
    {
        // Verificar la distancia entre el enemigo y el jugador
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // Si la distancia es menor o igual a la distancia de persecución
        if (distanceToPlayer <= chaseDistance)
        {
            // Iniciar o continuar la persecución
            isChasing = true;
        }
        else
        {
            // Detener la persecución si la distancia es mayor
            isChasing = false;
        }

        // Lógica de persecución ralentizada
        if (isChasing)
        {
            if (transform.position.x > playerTransform.position.x)
            {
                transform.localScale = new Vector3(0.6f, 0.6f, 1);
                transform.position += Vector3.left * speed * factorDeRalentizacionActual * Time.deltaTime;
            }
            if (transform.position.x < playerTransform.position.x)
            {
                transform.localScale = new Vector3(-0.6f, 0.6f, 1);
                transform.position += Vector3.right * speed * factorDeRalentizacionActual * Time.deltaTime;
            }
        }
    }

    public void Ralentizar(float factor)
    {
        // Llama a esta función desde el script del agua
        factorDeRalentizacionActual = factor;

        // Aplica la ralentización a la velocidad
        speed = velocidadOriginal * factorDeRalentizacionActual;
    }

    public void RestablecerVelocidad()
    {
        // Restablece la velocidad a su valor original
        speed = velocidadOriginal;
    }
}
