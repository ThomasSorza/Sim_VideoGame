using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.Distributions;
using System.Linq;


public class ChiTest : MonoBehaviour
{
    public List<double> riValues = new List<double>();
    public List<double> niValues = new List<double>();
    public int a = 8;
    public int b = 10;
    public int niMin;
    public int niMax;
    public int numAmount;
    public int intervalsAmount = 8;
    public List<double> intervalsValues = new List<double>();
    public List<int> frequencyObtained = new List<int>();
    public List<double> expectedFrequency = new List<double>();
    public List<double> chiSquaredValues = new List<double>();
    public double chiReverse;
    public double sumChi2;
    public bool passed;

    void Start()
    {
        // Calculate the number of elements in the riValues list.
        numAmount = riValues.Count;

        // Perform the chi-squared test.
        CheckTest();
    }

    void FillNiValues()
    {
        // Fill the niValues list with the expected values.
        for (int i = 0; i < numAmount; i++)
        {
            double value = a + (b - a) * riValues[i];
            niValues.Add(value);
        }
    }

    void SortNiArray()
    {
        // Sort the niValues list in ascending order.
        niValues.Sort();
    }

    double ObtainMinNiValue()
    {
        // Get the minimum value in the niValues list.
        niMin = Convert.ToInt32(niValues.Min());
        return niMin;
    }

    double ObtainMaxNiValue()
    {
        // Get the maximum value in the niValues list.
        niMax = Convert.ToInt32(niValues.Max());
        return niMax;
    }

    void FillIntervalsValuesArray()
    {
        // Fill the intervalsValues list with the interval values.
        double minValue = ObtainMinNiValue();
        double maxValue = ObtainMaxNiValue();
        intervalsValues.Add(minValue);

        for (int i = 0; i < intervalsAmount; i++)
        {
            double value = Math.Round(intervalsValues[i] + (maxValue - minValue) / intervalsAmount, 5);
            intervalsValues.Add(value);
        }
    }

    void FillFrequenciesArrays()
    {
        // Fill the frequencyObtained list with the observed frequencies and the expectedFrequency list with the expected frequencies for each interval.
        double expectedFreq = Convert.ToDouble(numAmount) / intervalsAmount;
        int counter = 0;

        for (int i = 0; i < intervalsValues.Count - 1; i++)
        {
            for (int j = 0; j < niValues.Count; j++)
            {
                if ((niValues[j] >= intervalsValues[i]) && (niValues[j] < intervalsValues[i + 1]))
                {
                    counter += 1;
                }
            }
            frequencyObtained.Add(counter);
            expectedFrequency.Add(expectedFreq);
            counter = 0;
        }
    }

    void FillChiSquaredValuesArray()
    {
        // Fill the chiSquaredValues list with the chi-squared values for each interval.
        for (int i = 0; i < frequencyObtained.Count; i++)
        {
            double value = Math.Round(Math.Pow((frequencyObtained[i] - expectedFrequency[i]), 2) / expectedFrequency[i], 2);
            chiSquaredValues.Add(value);
        }
    }

    double CumulativeObtainedFrequency()
    {
        // Calculate the cumulative observed frequency.
        return frequencyObtained.Sum();
    }

    double CumulativeExpectedFrequency()
    {
        // Calculate the cumulative expected frequency.
        return expectedFrequency.Sum();
    }

    double CumulativeChiSquaredValues()
    {
        // Calculate the cumulative chi-squared value.
        return chiSquaredValues.Sum();
    }

    public double ChiSquaredTestValue()
    {
            // Calculate the chi-squared critical value.
                double marginOfError = 0.05;
            int degreesOfFreedom = intervalsAmount - 1;
            // Calculate the chi-squared critical value.
            double alpha = 1.0 - marginOfError;
            // Look up the chi-squared critical value in a chi-squared distribution table.
            //double chiSquaredCriticalValue = ChiSquared.InvCDF(degreesOfFreedom, alpha);
            double chiSquaredCriticalValue = Cdf(degreesOfFreedom, marginOfError);
            return chiSquaredCriticalValue;
    }

    public bool CheckTest()
    {
        // Fill the niValues list.
        FillNiValues();

        // Sort the niValues list in ascending order.
        SortNiArray();

        // Fill the intervalsValues list.
        FillIntervalsValuesArray();

        // Fill the frequencyObtained list and the expectedFrequency list.
        FillFrequenciesArrays();

        // Fill the chiSquaredValues list.
        FillChiSquaredValuesArray();

        // Calculate the chi-squared critical value.
        chiReverse = ChiSquaredTestValue();

        // Calculate the cumulative chi-squared value.
        sumChi2 = CumulativeChiSquaredValues();

        // Check if the test was passed.
        if (sumChi2 <= chiReverse)
        {
            passed = true;
        }
        else
        {
            passed = false;
        }
        return passed;
    }















    








    public double Cdf(int degreesOfFreedom, double marginOfError)
    {
        // Calculate the chi-squared critical value.
        double alpha = 1.0 - marginOfError;

        // Look up the chi-squared critical value in a chi-squared distribution table.
        double chiSquaredCriticalValue = 0;
        switch (degreesOfFreedom)
        {
            case 1:
                chiSquaredCriticalValue = 3.841;
                break;
            case 2:
                chiSquaredCriticalValue = 5.991;
                break;
            case 3:
                chiSquaredCriticalValue = 7.815;
                break;
            case 4:
                chiSquaredCriticalValue = 9.488;
                break;
            case 5:
                chiSquaredCriticalValue = 11.070;
                break;
            case 6:
                chiSquaredCriticalValue = 12.592;
                break;
            case 7:
                chiSquaredCriticalValue = 14.067;
                break;
            case 8:
                chiSquaredCriticalValue = 15.507;
                break;
            case 9:
                chiSquaredCriticalValue = 16.919;
                break;
            case 10:
                chiSquaredCriticalValue = 18.307;
                break;
            default:
                throw new Exception("Degrees of freedom must be between 1 and 10.");
        }

        return chiSquaredCriticalValue;
    }

}

