using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorEnemigos : MonoBehaviour
{

    [SerializeField] private Transform[] puntos;
    [SerializeField] private GameObject[] enemigos;
    private GameObject enemigoActual;
    private int count;
    private void Start(){
      enemigoActual = Instantiate(enemigos[0], puntos[0].position, transform.rotation);
      count = 0;
    }

    private void Update()
    {
        if(enemigoActual == null)
        {
            count ++;
            if (count < enemigos.Length && count < puntos.Length)
            {
                SpawnEnemigo();
            }
        }
    }

      private void SpawnEnemigo()
    {
        // Instancia el enemigo actual en la posición actual
        enemigoActual = Instantiate(enemigos[count], puntos[count].position, transform.rotation);
    }
}


