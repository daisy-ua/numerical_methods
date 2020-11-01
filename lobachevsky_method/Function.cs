using System;

namespace lobachevsky_method
{
    public class Function
    {
        public double a;

        public double b;

        public Func<double, double> function;

        public Func<double, double> derivative;

        public Function(Func<double, double> f, double a, double b)
        {
            this.function = f;
            this.a = a;
            this.b = b;
            this.derivative = GetDerivative(f);
        }

        private Func<double, double> GetDerivative(Func<double, double> f)
        {
            double step = 1e-4;

            return ( double x ) => ( f(x + step) - f(x) / step );
        }
    }
}