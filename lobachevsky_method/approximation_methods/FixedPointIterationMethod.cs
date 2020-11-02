using System;

namespace lobachevsky_method.approximation_methods
{
    public static class FixedPointIterationMethod
    {
        private static Function f;

        private static double lambda;
        private static double q;

        private static double xStart;
        private static double xEnd;

        private static double GetPhi(double value) => value - lambda * f.function(value);

        public static double GetRoot(Function function)
        {
            f = function;

            CheckInitialConditions();

            InitValues();

            int counter = 0;

            while (true)
            {
                xEnd = GetPhi(xStart);

                if ((Auxiliary.IsRootObtained(xStart, xEnd, q)))
                    break;

                xStart = xEnd;
                counter += 1;
            }

            return (xStart + xEnd) / 2;
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

        private static void InitValues()
        {
            if (Auxiliary.IsMonotonicOnRange(f.a, f.b, f.derivative))
            {
                double minValue = Math.Min(f.derivative(f.a), f.derivative(f.b));
                double maxValue = Math.Max(f.derivative(f.a), f.derivative(f.b));

                lambda = 2 / (minValue + maxValue);

                q = (maxValue - minValue) / (maxValue + minValue);

                xStart = f.a;
                xEnd = f.b;
            }
        }
    }
}