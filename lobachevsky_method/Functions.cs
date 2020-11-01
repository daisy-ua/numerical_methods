using System;

public static class Functions 
{
    public static double f1 (double x) 
    {
        return x * x + Math.PI * ( Math.Log2(5 * Math.PI)) - 7 *
            Math.PI * Math.Cos(x) - 3 * x; 
    }

    public static double f2 (double x) 
    {
        return Math.Exp(x) + Math.Sin(x) - 10* x - 
            Math.Pow(x, 10) - Math.Exp(Math.Cos(x));
    }
}