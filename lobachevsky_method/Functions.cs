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
        return Math.Exp(x) + Math.Sin(x) - 10* x + 
            Math.Pow(x, 10) + Math.Exp(Math.Cos(x));
    }

    public static double f3 (double x)
    {
        return -278 * Math.Pow(x, 7) + 747 * Math.Pow(x, 6) + 625 * Math.Pow(x, 5) -966 * Math.Pow(x, 4) -207 * Math.Pow(x, 3) + 275 * Math.Pow(x, 2) -4 * x -5; 
    }
}