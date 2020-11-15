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

            if (q >= 1) throw new System.Exception("Cannot find root by this method. q parameter >= 1.");

            int counter = 0;

            while (true)
            {
                xEnd = GetPhi(xStart);

                Console.WriteLine("\n\tRoot in iteration {0}: {1}", counter, xEnd);

                if ((Auxiliary.IsRootObtained(xStart, xEnd, q)))
                    break;
                
                if (xEnd > f.b || xEnd < f.a) 
                    throw new System.Exception("Cannot find root by this method. Out of range!");

                if (Math.Abs(1 - lambda * f.derivative(xEnd)) > q)
                    throw new System.Exception("Cannot find root by this method.");

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
            xStart = (f.a + f.b) / 2;
         
            double min = Math.Min(f.function(f.a), f.function(f.b));
            double max = Math.Max(f.function(f.a), f.function(f.b));

            if (min <= 0) min = Auxiliary.epsilon;

            lambda = 1 / f.derivative(xStart);

            q = 1 - min / max;
        }
    }
}