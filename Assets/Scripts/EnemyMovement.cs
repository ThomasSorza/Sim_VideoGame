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

    // Referencia al script de distribución uniforme
    public UniformDistributionMethod uniformDistributionScript;
    private List<float> riValues = new List<float>();

    void Start()
    {
        // Obtener referencia al script UniformDistributionMethod
        uniformDistributionScript = GetComponent<UniformDistributionMethod>();

        // Si la referencia es válida, obtener los valores generados
        if (uniformDistributionScript != null)
        {
            riValues = uniformDistributionScript.GetRiValues();
        }
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

        // Lógica de persecución
        if (isChasing)
        {
            if (transform.position.x > playerTransform.position.x)
            {
                transform.localScale = new Vector3(0.6f, 0.6f, 1);
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            if (transform.position.x < playerTransform.position.x)
            {
                transform.localScale = new Vector3(-0.6f, 0.6f, 1);
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
        }
    }
}
