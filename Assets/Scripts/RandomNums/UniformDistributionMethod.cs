using UnityEngine;
using System;
using System.Collections.Generic;

public class UniformDistributionMethod : MonoBehaviour
{
    public float min;  // Valor mínimo para el rango de números aleatorios a crear
    public float max;  // Valor máximo para el rango de números aleatorios a crear
    public int numAmount;  // Cantidad de números a generar
    public AverageTest averageTestScript;
    public VarianceTest varianceTestScript;  // Agregado el script VarianceTest
    public PokerTest pokerTest;

    public ChiTest chiTest;

    public KsTest ksTest; //script de KsTest
    public int passed = 0; 

    private List<float> riValues = new List<float>();
    private List<float> niValues = new List<float>();

    private System.Random random = new System.Random();

    void Start()
    {
        FillRiValues();
        FillNiValues();
        
        // Encuentra el script AverageTest en el componente
        averageTestScript = GetComponent<AverageTest>();

        // Encuentra el script VarianceTest en el componente
        varianceTestScript = GetComponent<VarianceTest>();
        
        //Encontrar el script de la prueba ks en el componente
        ksTest = GetComponent<KsTest>();

        pokerTest = GetComponent<PokerTest>();

        chiTest = GetComponent<ChiTest>();

        //realiza la prueba de medias
        if (averageTestScript != null)
        {
            // Luego, pasa los valores de riValues al script AverageTest
            List<double> doubleList = new List<double>(riValues.Count);
            foreach (float value in riValues)
            {
                doubleList.Add((double)value);
            }

            // Realiza la prueba de promedio
            averageTestScript.CheckTest();

            // Comprueba si la prueba ha sido superada
            bool testPassed = averageTestScript.CheckIfPassed();

            if (testPassed)
            {
                //Debug.Log("La prueba de promedio ha sido superada.");
                passed +=1;
            }
            else
            {
                //Debug.Log("La prueba de promedio no ha sido superada.");
            }
        }

        //realiza la pueba de varianzas
        if (varianceTestScript != null)
        {
            // Pasa los valores de riValues al script VarianceTest
            List<double> doubleList = new List<double>(riValues.Count);
            foreach (float value in riValues)
            {
                doubleList.Add((double)value);
            }

            varianceTestScript.riNumbers = doubleList;

            // Realiza la prueba de varianza
            varianceTestScript.CheckTest();

            // Comprueba si la prueba de varianza ha sido superada
            bool testPassed = varianceTestScript.CheckTest();

            if (testPassed)
            {
                //Debug.Log("La prueba de varianza ha sido superada.");
                passed +=1;
            }
            else
            {
                //Debug.Log("La prueba de varianza no ha sido superada.");
            }
            
        }

        if (ksTest != null)
        {
            // Pasa los valores de riValues al script KsTest
            List<double> doubleList = new List<double>(riValues.Count);
            foreach (float value in riValues)
            {
                doubleList.Add((double)value);
            }

            ksTest.ri = doubleList;

            // Realiza la prueba KS
            ksTest.CheckTest();

            // Comprueba si la prueba KS ha sido superada
            bool testPassed = ksTest.passed;

            if (testPassed)
            {
                //Debug.Log("La prueba KS ha sido superada.");
                passed +=1;
            }
            else
            {
                //Debug.Log("La prueba KS no ha sido superada.");
            }
        }

        if(pokerTest != null){
            List<double> doubleList = new List<double>(riValues.Count);
            foreach (float value in riValues)
            {
                doubleList.Add((double)value);
            }
            
            pokerTest.riNums = doubleList;
            bool isPassed = pokerTest.CheckPoker();
            if (isPassed)
            {
                //Debug.Log("La prueba poker ha sido superada.");
                passed +=1;
            }
            else
            {
                //Debug.Log("La prueba poker no ha sido superada.");
                
            }
        }

        if(chiTest != null){
            List<double> doubleList = new List<double>(riValues.Count);
            foreach (float value in riValues)
            {
                doubleList.Add((double)value);
            }
            
            chiTest.riValues = doubleList;
            bool isPassed = pokerTest.CheckPoker();
            if (isPassed)
            {
                //Debug.Log("La prueba chi ha sido superada.");
                passed +=1;
            }
            else
            {
                //Debug.Log("La prueba chi no ha sido superada.");
                
            }
        }

        while (passed != 0)
        {
            FillRiValues();
        }


    }

    public List<float> GetRiValues()
    {
        return riValues;
    }


    public void FillRiValues()
    {
        passed = 0;
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

    public void FillNiValues()
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