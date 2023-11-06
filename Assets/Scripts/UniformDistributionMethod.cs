using UnityEngine;
using System;
using System.Collections.Generic;

public class UniformDistributionMethod : MonoBehaviour
{
    public float min;  // Valor mínimo para el rango de números aleatorios a crear
    public float max;  // Valor máximo para el rango de números aleatorios a crear
    public int numAmount;  // Cantidad de números a generar

    private List<float> riValues = new List<float>();
    private System.Random random = new System.Random();

    void Start()
    {
        FillRiValues();
    }

    void FillRiValues()
    {
        for (int i = 0; i < numAmount; i++)
        {
            float value = (float)random.NextDouble();
            riValues.Add((float)Math.Round(value, 5));
        }
    }

    public float[] GetRiValuesArray()
    {
        return riValues.ToArray();
    }
}
