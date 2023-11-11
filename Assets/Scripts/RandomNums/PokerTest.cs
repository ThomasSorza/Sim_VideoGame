
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Statistics;
using System.Drawing;

public class PokerTest : MonoBehaviour
{
    public List<double> riNums;
    private readonly List<double> prob = new List<double> { 0.3024, 0.504, 0.108, 0.072, 0.009, 0.0045, 0.0001 };
    private readonly List<int> oi = new List<int> { 0, 0, 0, 0, 0, 0, 0 };
    private List<double> ei = new List<double>();
    private List<double> eid = new List<double>();
    private bool passed = false;
    private readonly int n;
    private double totalSum = 0.0;
    private readonly double chiReverse = ChiSquareInverse(1 - 0.05, 6);

    public PokerTest(List<double> riNums)
    {
        this.riNums = riNums;
        this.n = riNums.Count;
    }

    public bool CheckPoker()
    {
        CalculateOi();
        CalculateEi();
        CalculateEid();
        CalculateTotalSum();
        passed = totalSum < chiReverse;
        return passed;
    }

    private static double ChiSquareInverse(double p, int df)
    {
        if (p <= 0 || p >= 1 || df < 1)
            throw new ArgumentException();

        double x = 0.5;
        double delta = 0.5;
        const double epsilon = 0.0001;

        while (Math.Abs(delta) > epsilon)
        {
            delta = (2.0 / (9.0 * df)) * Math.Pow(x, 2.0 / 3.0) * (1.0 - (2.0 / (9.0 * df)) * (1.0 - Math.Pow(x, 1.0 / 3.0))) - p;
            x -= delta;
        }

        return df * Math.Pow(x, 2.0 / 3.0);
    }

    private void CalculateTotalSum()
    {
        if(eid != null){
            totalSum = eid.Sum();
        }
    }

    private void CalculateOi()
    {
        if(riNums ==null ){
            foreach (var n in riNums)
        {
            var numStr = n.ToString().Split('.')[1];
            if (AllDiff(numStr))
                oi[0]++;
            else if (AllSame(numStr))
                oi[6]++;
            else if (FourOfAKind(numStr))
                oi[5]++;
            else if (OneThreeOfAKindAndOnePair(numStr))
                oi[4]++;
            else if (OnlyThreeOfAKind(numStr))
                oi[3]++;
            else if (TwoPairs(numStr))
                oi[2]++;
            else if (OnlyOnePair(numStr))
                oi[1]++;
        }
        }
    }

    private static bool AllDiff(string numStr)
    {
        return numStr.Distinct().Count() == numStr.Length;
    }

    private static bool AllSame(string numStr)
    {
        return numStr.Distinct().Count() == 1;
    }

    private static bool FourOfAKind(string numStr)
    {
        var count = numStr.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
        var numQuads = count.Values.Count(freq => freq == 4);
        return numQuads == 1;
    }

    private static bool TwoPairs(string numStr)
    {
        var count = numStr.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
        var numPairs = count.Values.Count(freq => freq == 2);
        return numPairs == 2;
    }

    private static bool OneThreeOfAKindAndOnePair(string numStr)
    {
        var count = numStr.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
        var numPairs = count.Values.Count(freq => freq == 2);
        var numTriples = count.Values.Count(freq => freq == 3);
        return numPairs == 1 && numTriples == 1;
    }

    private static bool OnlyOnePair(string numStr)
    {
        var count = numStr.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
        var numPairs = count.Values.Count(freq => freq == 2);
        return numPairs == 1;
    }

    private static bool OnlyThreeOfAKind(string numStr)
    {
        var count = numStr.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
        var numTriples = count.Values.Count(freq => freq == 3);
        return numTriples == 1;
    }

    private void CalculateEi()
    {   
        if (ei != null){
            for (var i = 0; i < 7; i++)
        {
            ei.Add(prob[i] * n);
        }
        }
    }

    private void CalculateEid()
    {
        if(oi != null){
            for (var i = 0; i < oi.Count; i++)
            {
                if (Math.Abs(prob[i] * n) > 0)
                {
                    eid.Add(Math.Pow(oi[i] - prob[i] * n, 2) / (prob[i] * n));
                }
            }
        }
        passed = true;
    }

    public override string ToString()
    {
        return $"PokerTest(ri_nums={riNums}, prob={prob}, oi={oi}, ei={ei}, eid={eid}, passed={passed}, n={n}, total_sum={totalSum}, chi_reverse={chiReverse})";
    }
}
