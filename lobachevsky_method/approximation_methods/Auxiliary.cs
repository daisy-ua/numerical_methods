using System;

namespace lobachevsky_method.approximation_methods
{
    public static class Auxiliary
    {
        public static double epsilon = 1e-7;

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
            
            double step = 1e-1; 

            bool isGrows = f(a) < f(a + step);

            for (double i = a; i < b; i += step)
            {
                if (f(i) < f(i + step) != isGrows) 
                    return false;
            }

            return true;
        }

        public static bool IsRootObtained(double a, double b)
        {
            return Math.Abs(a - b) < epsilon;
        }
    }
}