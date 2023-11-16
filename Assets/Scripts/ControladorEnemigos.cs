using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ControladorEnemigos : MonoBehaviour
{

    [SerializeField] private Transform[] puntos;
    [SerializeField] private GameObject[] enemigos;
    [SerializeField]  private float tiempoTranscurrido;
    private float tiempoEntreEnemigos;
    private GameObject enemigoActual;
    private int count;
    private UniformDistributionMethod uniformDistributionMethod;
    private float[] times;
    private void Start(){
      count = 0;
      uniformDistributionMethod = GetComponent<UniformDistributionMethod>();
      uniformDistributionMethod.FillRiValues();
      uniformDistributionMethod.FillNiValues();
      times = uniformDistributionMethod.GetNiValuesArray();
      tiempoEntreEnemigos = times[count];
    }

    private void Update()
    {       
            if (count < enemigos.Length)
            {
                 tiempoTranscurrido += Time.deltaTime;
                 if(tiempoTranscurrido >= tiempoEntreEnemigos)
                 {
                      SpawnEnemigo();
                      tiempoEntreEnemigos = times[count];
                      tiempoTranscurrido = 0f; 
                }
            }
    }

      private void SpawnEnemigo()
    {        // Instancia el enemigo actual en la posición actual

        enemigoActual = Instantiate(enemigos[count], puntos[0].position, transform.rotation);
        count++;

    }
}