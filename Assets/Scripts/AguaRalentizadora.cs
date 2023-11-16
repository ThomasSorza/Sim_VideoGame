using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AguaRalentizadora : MonoBehaviour
{
    [Range(0.1f, 1.0f)] // Rango para el factor de ralentización (20% a 100%)
    public float factorDeRalentizacion = 0.8f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto que entra en el área es el jugador o un enemigo
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                // Ralentizar el jugador
                Debug.Log("Ralentizando al jugador");
                playerMovement.Ralentizar(factorDeRalentizacion);
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            EnemyMovement enemyMovement = other.GetComponent<EnemyMovement>();
            if (enemyMovement != null)
            {
                // Ralentizar al enemigo
                Debug.Log("Ralentizando al enemigo");
                enemyMovement.Ralentizar(factorDeRalentizacion);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Verificar si el objeto que sale del área es el jugador o un enemigo
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                // Restablecer la velocidad del jugador al salir del agua
                Debug.Log("Restableciendo la velocidad del jugador");
                playerMovement.RestablecerVelocidad();
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            EnemyMovement enemyMovement = other.GetComponent<EnemyMovement>();
            if (enemyMovement != null)
            {
                // Restablecer la velocidad del enemigo al salir del agua
                Debug.Log("Restableciendo la velocidad del enemigo");
                enemyMovement.RestablecerVelocidad();
            }
        }
    }
}
