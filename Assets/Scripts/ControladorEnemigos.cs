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
      uniformDistributionMethod = GetComponent<UniformDistributionMethod >();
      times = uniformDistributionMethod.GetNiValuesArray();
      tiempoEntreEnemigos = times[count];
    }

    private void Update()
    {       
           
            if (count < enemigos.Length && count < puntos.Length)
            {
                 tiempoTranscurrido += Time.deltaTime;
                 if(tiempoTranscurrido >= tiempoEntreEnemigos)
                 {
                    Debug.Log(tiempoTranscurrido);
                      SpawnEnemigo();
                      tiempoTranscurrido = 0f;
                      count++;
                        if (count < times.Length)
                        {
                          tiempoEntreEnemigos = times[count];
                        }
                         else
                        {
                          Debug.Log("Se han generado todos los enemigos");
                        }
                }
            }
    }

      private void SpawnEnemigo()
    {        // Instancia el enemigo actual en la posición actual

        enemigoActual = Instantiate(enemigos[count], puntos[count].position, transform.rotation);
        
    }
}


