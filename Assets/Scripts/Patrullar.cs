using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrullar : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float distanciaMinima;
    
    UniformDistributionMethod uniformDistributionMethod; 

    private List<int> niValues = new List<int>(); //lista de enteros

    private int numeroAleatorio;
    private SpriteRenderer spriteRenderer; // Corregir el nombre de la variable
    private int i = 0;

    private void Start() // Corregir el nombre del método
    {
        uniformDistributionMethod = GetComponent<UniformDistributionMethod>();
        float[] flotantes = uniformDistributionMethod.GetNiValuesArray();
        for (int i = 0; i < flotantes.Length; i++)
        {
            niValues.Add((int)flotantes[i]);
            Debug.Log(niValues[i]);
        }
        numeroAleatorio = niValues[i];
        spriteRenderer = GetComponent<SpriteRenderer>();
        Girar();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[numeroAleatorio].position, velocidadMovimiento * Time.deltaTime); // Corregir el nombre de la variable

        if (Vector2.Distance(transform.position, puntosMovimiento[numeroAleatorio].position) < distanciaMinima)
        {
            i+=1;
            if (i >= niValues.Count)
            {
                i = 0;
                niValues.Clear();
                uniformDistributionMethod.FillNiValues();
                float[] flotantes = uniformDistributionMethod.GetNiValuesArray();
                for (int i = 0; i < flotantes.Length; i++)
                {
                    niValues.Add((int)flotantes[i]);
                    Debug.Log(niValues[i]);
                }
            }
            numeroAleatorio = niValues[i];
            Girar();
        }
    }

    private void Girar()
    {
        if (transform.position.x < puntosMovimiento[numeroAleatorio].position.x)
        {
            spriteRenderer.flipX = true; // Corregir el nombre de la propiedad
        }
        else
        {
            spriteRenderer.flipX = false; // Corregir el nombre de la propiedad
        }
    }
}
