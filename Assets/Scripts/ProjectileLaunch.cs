using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLaunch : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject criticalPrefab;

    public Transform launchPoint;
    public float shootTime;
    private float shootCounter;

    // Probabilidades de disparo normal y crítico
    private float probNormal = 0.8f;
    private float probCritico = 0.2f;

    // Tiempo de espera después de disparar normal y crítico
    public float cooldownNormal = 6f;
    public float cooldownCritico = 10f;

    private float cooldownCounterNormal;
    private float cooldownCounterCritico;

    // Start is called before the first frame update
    void Start()
    {
        shootCounter = shootTime;
        cooldownCounterNormal = 0f;
        cooldownCounterCritico = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownCounterNormal > 0)
        {
            cooldownCounterNormal -= Time.deltaTime;
        }

        if (cooldownCounterCritico > 0)
        {
            cooldownCounterCritico -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1") && shootCounter <= 0)
        {
            // Genera un número aleatorio entre 0 y 1
            float randomValue = Random.Range(0f, 1f);

            // Decide si se lanza un disparo normal o crítico basado en las probabilidades
            if (randomValue < probNormal && cooldownCounterNormal <= 0)
            {
                Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
                cooldownCounterNormal = cooldownNormal;
            }
            else if (randomValue >= probNormal && cooldownCounterCritico <= 0)
            {
                Instantiate(criticalPrefab, launchPoint.position, Quaternion.identity);
                cooldownCounterCritico = cooldownCritico;
            }

            shootCounter = shootTime;
        }

        shootCounter -= Time.deltaTime;
    }
}
