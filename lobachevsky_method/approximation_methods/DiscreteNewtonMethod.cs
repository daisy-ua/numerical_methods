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
            df2 = Function.GetDerivative(f.derivative);

            CheckInitialConditions();

            int counter = 1;

            xStart = GetInitialApproximation();

            while (true)
            {
                double coeff = GetCoeff(counter);
                xCurr = xStart - f.function(xStart) * coeff
                    / (f.function(xStart + coeff) - f.function(xStart));

                if (Auxiliary.IsRootObtained(xStart, xCurr)) return xCurr;

                xStart = xCurr;

                Console.WriteLine("\n\tRoot in iteration {0}: {1}", counter, xCurr);

                counter += 1;
            }
        }

        private static double GetInitialApproximation()
        {
            double start = f.a;
            double end = f.b;
            double step = 1e-7;

            while (true)
            {
                if (CheckFourierCondition(start)) return start;
                if (CheckFourierCondition(end)) return end;

                start += step;
                end -= step;
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

        private static bool CheckFourierCondition(double x) => (f.function(x) * df2(x) <= 0) ? false : true;

        private static double GetCoeff(int counter)
        {
            return Math.Sqrt(2) / (counter  + 2);
        }
    }
}