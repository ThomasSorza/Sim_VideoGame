using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class KsTest : MonoBehaviour
{
    public List<double> ri;
    private int n;
    private double average;
    private double dMax;
    private double dMaxP;
    private double minValue;
    private double maxValue;
    private List<int> oi;
    private List<int> oia;
    private List<double> probOi;
    private List<double> oiaA;
    private List<double> probEsp;
    private List<double> diff;
    public bool passed;
    private double alpha;
    private List<Tuple<double, double>> intervals;
    private int nIntervals;

    public KsTest(List<double> riNums, int nIntervals = 10)
    {
        ri = riNums;
        n = riNums.Count;
        average = 0;
        dMax = 0;
        dMaxP = 0;
        minValue = 0;
        maxValue = 0;
        oi = new List<int>();
        oia = new List<int>();
        probOi = new List<double>();
        oiaA = new List<double>();
        probEsp = new List<double>();
        this.diff = new List<double>();
        passed = false;
        alpha = 0.05;
        this.nIntervals = nIntervals;
        intervals = new List<Tuple<double, double>>();
    }

    public void CalculateOia()
    {
        int cumFreq = 0;
        foreach (var freq in oi)
        {
            cumFreq += freq;
            oia.Add(cumFreq);
        }
    }

    public void CalculateMin()
    {
        if (n != 0)
        {
            minValue = ri.Min();
        }
    }

    public void CalculateMax()
    {
        if (n != 0)
        {
            maxValue = ri.Max();
        }
    }

    public void CalculateAverage()
    {
        if (n != 0)
        {
            average = ri.Average();
        }
    }

    public void CheckTest()
    {
        CalculateMin();
        CalculateMax();
        CalculateAverage();
        CalculateIntervals();
        CalculateOi();
        CalculateOia();
        CalculateProbOi();
        CalculateOiaA();
        CalculateProbEsp();
        CalculateDiff();
        CalculateKS();
        passed = dMax <= dMaxP;
    }

    public double CalculateKS()
    {
        //double n = n / nIntervals;
        if (n <= 50 && n > 0)
        {
            // Utilizar tabla precalculada para n <= 50
            // Esta tabla puede encontrarse en la literatura o calculada previamente
            // Aquí se muestra solo un ejemplo con valores arbitrarios
            double[,] table = {
                {0.9, 1.22, 1.36, 1.48, 1.63, 1.73, 1.95, 2.12, 2.33, 2.54, 2.77},
                {0.68, 0.9, 1.01, 1.15, 1.32, 1.41, 1.58, 1.73, 1.95, 2.17, 2.44}
            };
            int index = (int)Math.Round(alpha * 10) - 1;
            return table[0, index];
        }
        else
        {
            // Utilizar aproximación para n > 50
            return Math.Sqrt(-0.5 * Math.Log(alpha / 2.0));
        }
    }




    public void CalculateProbEsp()
    {   
        if (oiaA !=null){
            for (int i = 0; i < oiaA.Count; i++)
            {
                probEsp.Add(oiaA[i] / n);
            }
        }
    }

    public void CalculateDiff()
    {
        if(probEsp != null){
            for (int i = 0; i < probEsp.Count; i++)
            {
                diff.Add(Math.Abs(probEsp[i] - probOi[i]));
            }
        }
    }

    public void CalculateOiaA()
    {
        double n1 = n / (nIntervals + 1);
        for (int i = 0; i < nIntervals; i++)
        {
            oiaA.Add(n1 * (i + 1));
        }
    }

    public void CalculateProbOi()
    {
        if (oia != null){
            for (int i = 0; i < oia.Count; i++)
        {
            probOi.Add(oia[i] / (double)n);
        }
        }
        
    }

    public List<int> CalculateOi()
    {
        ri.Sort();
        oi = Enumerable.Repeat(0, nIntervals).ToList();

        foreach (var valor in ri)
        {
            if (intervals != null)
            {
                    for (int i = 0; i < intervals.Count; i++)
                {
                    if (intervals[i].Item1 <= valor && valor < intervals[i].Item2)
                    {
                        oi[i]++;
                        break;
                    }
                }
            }
        }

        return oi;
    }

    public void CalculateIntervals()
    {
        if (n != 0)
        {
            double intervalSize = (maxValue - minValue) / nIntervals;
            double initial = minValue;

            for (int i = 0; i < nIntervals; i++)
            {
                var newInterval = new Tuple<double, double>(initial, initial + intervalSize);
                intervals.Add(newInterval);
                initial = newInterval.Item2;
            }
        }
    }
}