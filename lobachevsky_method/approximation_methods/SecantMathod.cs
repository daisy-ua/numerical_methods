using System;

namespace lobachevsky_method.approximation_methods
{
    public static class SecantMethod
    {
        private static double xStart;

        private static double xEnd;

        public static double GetRoot(Function f)
        {
            CheckInitialConditions(f);

            xStart = f.a;
            xEnd = f.b;

            double x = 0;

            do
            {
                x = xStart - f.function(xStart) * (xEnd - xStart)
                    / (f.function(xEnd) - f.function(xStart));

                if (Math.Abs(f.function(x)) < Auxiliary.epsilon)
                {
                    return x;
                }
                if (f.function(x) * f.function(xEnd) < 0)
                {
                    xStart = x;
                }
                else if (f.function(xStart) * f.function(x) < 0)
                {
                    xEnd = x;
                }     

            } while( !Auxiliary.IsRootObtained(xStart, xEnd));

            return (xStart + xEnd) / 2;
        }

        private static void CheckInitialConditions(Function f)
        {
            if (!Auxiliary.IsMonotonicOnRange(f.a, f.b, f.derivative))
                throw new System.InvalidOperationException(
                    "Function isn't monotonic on this range! Choose another one.");
            
            else if (!Auxiliary.IsRootOnRange(f.a, f.b, f.function))
                throw new System.InvalidOperationException(
                    "Function have no roots on this range! Choose another one.");
        }
    }
}