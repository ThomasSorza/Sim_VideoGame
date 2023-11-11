using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Statistics;
using System.Drawing;

public class AverageTest : MonoBehaviour
{
    public List<double> ri_nums;
    double average;
    double alpha;
    double acceptation;
    bool passed;
    int n;
    double z;
    double superior_limit;
    double inferior_limit;

    public AverageTest(List<double> ri_nums)
    {
        this.ri_nums = ri_nums;
        this.average = 0;
        this.alpha = 0.05;
        this.acceptation = 0.95;
        this.passed = false;
        this.n = ri_nums.Count;
        this.z = 0.0;
        this.superior_limit = 0.0;
        this.inferior_limit = 0.0;
    }

    public void CalcAverage()
    {
        if (n != 0)
        {
            average = ri_nums.Average();
        }
    }

    public void CalculateZ()
    {
        z = Normal.InvCDF(1 - (alpha / 2), 0, 1);
    }

    public void CalculateSuperiorLimit()
    {
        if (n > 0)
        {
            superior_limit = (1.0 / 2) + (z * (1.0 / Math.Sqrt(12 * n)));
        }
    }

    public void CalculateInferiorLimit()
    {
        if (n > 0)
        {
            inferior_limit = (1.0 / 2) - (z * (1.0 / Math.Sqrt(12 * n)));
        }
    }

    public void CheckTest()
    {
        CalcAverage();
        CalculateZ();
        CalculateSuperiorLimit();
        CalculateInferiorLimit();
        if (inferior_limit <= average && average <= superior_limit)
        {
            passed = true;
        }
        else
        {
            passed = false;
        }
    }

    public bool CheckIfPassed()
    {
        if (inferior_limit <= average && average <= superior_limit)
        {
            passed = true;
        }
        else
        {
            passed = false;
        }
        return passed;
    }

    public void Clear()
    {
        average = 0;
        alpha = 0.05;
        acceptation = 0.95;
        passed = false;
        z = 0.0;
        superior_limit = 0.0;
        inferior_limit = 0.0;
    }

}
