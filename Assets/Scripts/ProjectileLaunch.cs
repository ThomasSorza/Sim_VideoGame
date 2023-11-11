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

    private UniformDistributionMethod uniformDistributionScript;

    private int indexRi = 0; 
    // Start is called before the first frame update
    void Start()
    {
        uniformDistributionScript = GetComponent<UniformDistributionMethod>();
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
            
            List<float> riValues = uniformDistributionScript.GetRiValues();
            float randomValue = riValues[indexRi];
            // Hacer algo con los valores de riValues
            

            // Decide si se lanza un disparo normal o crítico basado en las probabilidades
            if (randomValue < probNormal && cooldownCounterNormal <= 0)
            {   
                LaunchProjectile(projectilePrefab);
                cooldownCounterNormal = cooldownNormal;
            }
            else if (randomValue >= probNormal && cooldownCounterCritico <= 0)
            {
                LaunchProjectile(criticalPrefab, true);
                cooldownCounterCritico = cooldownCritico;
            }

            shootCounter = shootTime;
            indexRi += 1;
            if (indexRi >= riValues.Count)
            {
                uniformDistributionScript.FillRiValues();
                indexRi = 0;
            }
        }

        shootCounter -= Time.deltaTime;
    }

    private void LaunchProjectile(GameObject projectilePrefab, bool isCritical = false)
    {
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.Initialize(isCritical);
        }
    }
}
