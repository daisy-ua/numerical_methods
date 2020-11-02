using System;

namespace lobachevsky_method.approximation_methods
{
    public static class DiscreteNewtonMethod
    {
        private static Function f;
        private static Func<double, double> df2;

        private static double xStart;
        private static double xCurr;

        public static double GetRoot(Function function)
        {
            f = function;
            df2 = f.GetDerivative();
            xStart = f.a;

            CheckInitialConditions();

            int counter = 0;

            while (true)
            {
                double coeff = GetCoeff(counter);
                xCurr = xStart - f.function(xStart) * coeff 
                    / (f.function(xStart + coeff) - f.function(coeff));
                
                if (Auxiliary.IsRootObtained(xStart, xCurr)) return xCurr;

                xStart = xCurr;
                counter += 1;
            } 
        }

        private static void CheckInitialConditions()
        {
            if (!Auxiliary.IsMonotonicOnRange(f.a, f.b, f.function))
                throw new System.InvalidOperationException(
                    "Function isn't monotonic on this range! Choose another one.");
            
            else if (!Auxiliary.IsRootOnRange(f.a, f.b, f.function))
                throw new System.InvalidOperationException(
                    "Function have no roots on this range! Choose another one.");
        }

        private static void CheckFourierCondition(double x)
        {
            if (f.function(x) * df2(x) <= 0)
                throw new System.InvalidOperationException(
                    "Cannot get roots! Fourier condition was not fulfilled."
                );
        }

        private static double GetCoeff(int counter)
        {
            return Math.Sqrt(2) / (counter + 2);
        }
    }
}