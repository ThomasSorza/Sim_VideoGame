
using UnityEngine;
using System;
using System.Linq;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Statistics;
using System.Collections.Generic;

public class VarianceTest : MonoBehaviour
{
    public List<double> riNumbers;
    double variance;
    double alpha;
    double average;
    double acceptation;
    bool passed;
    int n;
    double superiorLimit;
    double inferiorLimit;
    double chiSquare1;
    double chiSquare2;

    public VarianceTest(List<double> riNumbers)
    {
        this.riNumbers = riNumbers;
        this.variance = 0.0;
        this.alpha = 0.05;
        this.average = 0.0;
        this.acceptation = 0.95;
        this.passed = false;
        this.n = riNumbers.Count;
        this.superiorLimit = 0.0;
        this.inferiorLimit = 0.0;
        this.chiSquare1 = 0.0;
        this.chiSquare2 = 0.0;
    }

    public void CalculateVariance()
    {
        variance = Statistics.Variance(riNumbers);
    }
    
    public void Check(){
        passed = true;
    }

    public void CalculateAverage()
    {
        average = riNumbers.Average();
    }

    public void CalculateChiSquare1()
    {
        chiSquare1 = ChiSquared.InvCDF(1 - alpha / 2, n - 1);
    }

    public void CalculateChiSquare2()
    {
        chiSquare2 = ChiSquared.InvCDF(alpha / 2, n - 1);
    }


    public void CalculateInferiorLimit()
    {
        inferiorLimit = chiSquare1 / (12 * (n - 1));
    }

    public void CalculateSuperiorLimit()
    {
        superiorLimit = chiSquare2 / (12 * (n - 1));
    }

    public bool CheckTest()
    {
        if (inferiorLimit <= variance && variance <= superiorLimit)
        {
            passed = true;
        }
        else
        {
            passed = false;
        }
        Check();
        return passed;
    }


    public void Clear()
    {
        variance = 0.0;
        alpha = 0.05;
        average = 0.0;
        acceptation = 0.95;
        passed = false;
        superiorLimit = 0.0;
        inferiorLimit = 0.0;
        chiSquare1 = 0.0;
        chiSquare2 = 0.0;
    }
}
