using System;

namespace lobachevsky_method
{
    class LobachevskyMethod
    {
        public static double[] polynomial = { -278, 747, 625, -966, -207, 275, -4, 5 };

        public static double epsilon = 1e-7;

        public static int maxNumberPositiveRoots;

        static LobachevskyMethod()
        {
            int signChange = 0, sign = 1;
            for (int i = 0; i < polynomial.Length; i++)
            {
                if (Math.Sign(polynomial[i]) == 0) continue;
                else if(Math.Sign(polynomial[i]) != sign)
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

            for  (int i = 0; i < coeffs.Length; i++)
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

        public static int SetSign(double x)
        {
            double sum = 0;
            for (int i = 0; i < polynomial.Length; i++)
            {
                sum += polynomial[i] * Math.Pow(x, polynomial.Length -1 - i);
            }

            return (Math.Abs(sum) < maxNumberPositiveRoots) ? 1 : -1; 
        } 
    }
}
