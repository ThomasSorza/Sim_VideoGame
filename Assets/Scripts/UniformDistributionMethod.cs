using UnityEngine;
using System;
using System.Collections.Generic;

public class UniformDistributionMethod : MonoBehaviour
{
    public float min;  // Valor mínimo para el rango de números aleatorios a crear
    public float max;  // Valor máximo para el rango de números aleatorios a crear
    public int numAmount;  // Cantidad de números a generar

    private List<float> riValues = new List<float>();
    private List<float> niValues = new List<float>();

    private System.Random random = new System.Random();

    void Start()
    {
        FillRiValues();
        FillNiValues();
    }

    void FillRiValues()
    {
        for (int i = 0; i < numAmount; i++)
        {
            float value = (float)random.NextDouble();
            riValues.Add((float)Math.Round(value, 5));
        }
    }

    float ObtainMinValue()
    {
        return min;
    }

    float ObtainMaxValue()
    {
        return max;
    }

    void FillNiValues()
    {
        float min_value = ObtainMinValue();
        float max_value = ObtainMaxValue();

        for (int i = 0; i < riValues.Count; i++)
        {
            float value = min_value + (max_value - min_value) * riValues[i];
            niValues.Add((float)Math.Round(value, 5));
        }
    }

    public float[] GetRiValuesArray()
    {
        return riValues.ToArray();
    }

    public float[] GetNiValuesArray()
    {
        return niValues.ToArray();
    }
}
