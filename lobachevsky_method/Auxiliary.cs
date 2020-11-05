using System;
using System.Collections.Generic;

namespace lobachevsky_method
{
    public static class Auxiliary
    {
        public static double epsilon = 1e-7;
        public static double step = 1e-1; 

        private static double minInfiniteValue = -1e3;
        private static double maxInfiniteValue = 1e3;

        public static bool IsRootOnRange(double a, double b, Func<double, double> f)
        {
            return f(a) * f(b) < 0;
        }

        public static bool IsMonotonicOnRange(double a, double b, Func<double, double> f)
        {
            if (Double.IsNegativeInfinity(a)) a = minInfiniteValue;
            if (Double.IsPositiveInfinity(b)) b = maxInfiniteValue;
            
            bool isGrows = f(a) <= f(a + step);

            for (double i = a; i < b; i += step)
            {
                if (f(i) <= f(i + step) != isGrows) 
                    return false;
            }

            return true;
        }

        public static List<double> GetMonotonicIntervals(double lowerBound, double upperBound, Func<double, double> f)
        {
            List<double> intervalBreaks = new List<double>(); 

            intervalBreaks.Add(lowerBound);
            
            bool isGrows = f(lowerBound) <= f(lowerBound + step);

            for (double i = lowerBound; i < upperBound; i += step)
            {
                if (f(i) <= f(i + step) != isGrows)
                {
                    isGrows = !isGrows;
                    intervalBreaks.Add(i);
                }
            }

            intervalBreaks.Add(upperBound);

            return intervalBreaks;
        }

        public static double[] GetMergedMonotonicIntervals(double lowerPositiveBound, double upperPositiveBound,
            double lowerNegativeBound, double upperNegativeBound, Func<double, double> f) 
            {
                List<double> monotonicBreaks = new List<double>();
                monotonicBreaks.AddRange(GetMonotonicIntervals(lowerNegativeBound, upperNegativeBound, f));
                monotonicBreaks.Add(Double.NaN);
                monotonicBreaks.AddRange(GetMonotonicIntervals(lowerPositiveBound, upperPositiveBound, f));

                return monotonicBreaks.ToArray();
            }

        public static bool IsRootObtained(double a, double b)
        {
            return Math.Abs(a - b) < epsilon;
        }

        public static bool IsRootObtained(double a, double b, double q)
        {
            return Math.Abs(a-b) <= (1 - q) / q * epsilon;
        }
    }
}