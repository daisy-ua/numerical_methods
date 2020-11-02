using System;
using System.Linq;

namespace lobachevsky_method
{
    class LobachevskyMethod
    {
        public static double[] polynomial = { -278, 747, 625, -966, -207, 275, -4, -5 };

        public static double epsilon = 1e-7;

        public static int maxNumberPositiveRoots;

        public static double upperPositiveBound;
        public static double lowerPositiveBound;
        public static double upperNegativeBound;
        public static double lowerNegativeBound;

        static LobachevskyMethod()
        {
            int signChange = 0, sign = 1;
            for (int i = 0; i < polynomial.Length; i++)
            {
                if (Math.Sign(polynomial[i]) == 0) continue;
                else if (Math.Sign(polynomial[i]) != sign)
                {
                    sign *= -1;
                    signChange += 1;
                }
            }

            maxNumberPositiveRoots = signChange;
        }

        public static double[] Calculate()
        {
            double[] coeffs = (double[])polynomial.Clone();
            double[] prevStateCoeffs = new double[coeffs.Length];
            int counter = 0;

            do
            {
                Array.Copy(Minimize(coeffs), prevStateCoeffs, coeffs.Length);
                coeffs = GetCoeffs(prevStateCoeffs);
                counter += 1;
            } while (!IsFinalCoeffsObtained(prevStateCoeffs, coeffs));

            return GetRoots(coeffs, counter);
        }

        private static double ConvertCoeffsToPolynomial(double x)
        {
            double function = 0;

            for (int i = 0; i < polynomial.Length; i++)
            {
                function += polynomial[i] * Math.Pow(x, polynomial.Length - 1 - i);
            }

            return function;
        }

        private static bool IsFinalCoeffsObtained(double[] prev, double[] curr)
        {
            for (int i = 0; i < prev.Length; i++)
            {
                double precision = Math.Abs(prev[i] * prev[i] - curr[i]);
                if (precision > epsilon) return false;
            }

            return true;
        }

        private static double[] Minimize(double[] coeffs)
        {
            double coeff = 50;

            for (int i = 0; i < coeffs.Length; i++)
            {
                coeffs[i] /= coeff;
            }

            return coeffs;
        }

        private static double[] GetCoeffs(double[] prevStateCoeffs)
        {
            double[] coeffs = (double[])prevStateCoeffs.Clone();
            coeffs[0] = coeffs[0] * coeffs[0];

            for (int i = 0; i < prevStateCoeffs.Length; i++)
            {
                coeffs[i] = prevStateCoeffs[i] * prevStateCoeffs[i];

                int j = 1, sign = -1;
                while ((i + j) < prevStateCoeffs.Length && (j <= i))
                {
                    coeffs[i] += 2 * sign * prevStateCoeffs[i - j] * prevStateCoeffs[j + i];
                    j += 1;
                    sign *= -1;
                }
            }

            return coeffs;
        }

        private static double[] GetRoots(double[] coeffs, int counter)
        {
            double[] roots = new double[coeffs.Length - 1];
            double power = Math.Pow(2, -counter);

            int j = 0;
            for (int i = 1; i < coeffs.Length; i++, j++)
            {
                roots[j] = Math.Pow(coeffs[i] / coeffs[i - 1], power);

                roots[j] *= SetSign(roots[j]);

                if (Double.IsNaN(roots[j])) j--;
            }

            Array.Resize(ref roots, j);

            return roots;
        }

        private static int SetSign(double x)
        {
            double sum = 0;
            for (int i = 0; i < polynomial.Length; i++)
            {
                sum += polynomial[i] * Math.Pow(x, polynomial.Length - 1 - i);
            }

            return (Math.Abs(sum) < maxNumberPositiveRoots) ? 1 : -1;
        }

        private static void SetBounds()
        {
            double[] lowerPositiveCoeffs = new double[polynomial.Length];
            double[] upperNegativeCoefss = new double[polynomial.Length];
            double[] lowerNegativeCoeffs = (double[])polynomial.Clone();

            for (int i = 0; i < polynomial.Length; i++)
            {
                lowerPositiveCoeffs[polynomial.Length - 1 - i] = polynomial[i];

                if ((polynomial.Length - 1 - i) % 2 != 0)
                {
                    upperNegativeCoefss[polynomial.Length - 1 - i] = -polynomial[i];
                    lowerNegativeCoeffs[i] *= -1;
                }
                else 
                {
                    upperNegativeCoefss[polynomial.Length - 1 - i] = polynomial[i];
                }
            }

            upperPositiveBound = GetUpperBoundOfPositiveRoots(polynomial);
            lowerNegativeBound = -GetUpperBoundOfPositiveRoots(lowerNegativeCoeffs);

            try
            {
                lowerPositiveBound = 1 / GetUpperBoundOfPositiveRoots(lowerPositiveCoeffs);
            }
            catch(System.DivideByZeroException)
            {
                lowerPositiveBound = 0;
            }

            try
            {
                upperNegativeBound = -1 / GetUpperBoundOfPositiveRoots(upperNegativeCoefss);
            }
            catch(System.DivideByZeroException)
            {
                upperNegativeBound = 0;
            }
        }

        private static double GetUpperBoundOfPositiveRoots(double[] coeffs)
        {
            int firstNegativeIndex = -1;
            double maxAbsNegative = 0;

            for (int i = 0; i < coeffs.Length; i++)
            {
                if (coeffs[i] < 0)
                {
                    if (firstNegativeIndex == -1)
                        firstNegativeIndex = i + 1;

                    if (Math.Abs(coeffs[i]) > maxAbsNegative)
                        maxAbsNegative = Math.Abs(coeffs[i]);
                }
            }

            if (firstNegativeIndex == -1) return 0;

            return 1 + Math.Pow(maxAbsNegative / Math.Abs(coeffs[0]), firstNegativeIndex);
        }
    }
}
