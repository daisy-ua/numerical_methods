using System;

namespace lobachevsky_method
{
    class Program
    {
        public static Func<Function, double> GetRootFindingFunction;
        public static Function function;

        public static double xStart;
        
        public static double xEnd;

        static void OutputLobachevskyConsole()
        {
            Console.WriteLine("Roots calculated by Lobachevsky method:");

            double[] roots = LobachevskyMethod.Calculate();

            foreach (var root in roots)
            {
                Console.WriteLine("\t{0}", root);
            }

            Console.WriteLine("Select option to continue:");
            Console.WriteLine("1. Get approximated roots.");
            Console.WriteLine("0. Back.");

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    {
                        try
                        {
                            PrintApproximatedLobachevskyRoots(roots);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("\t{0}", e.Message);
                        }
                        finally
                        {
                            Console.WriteLine("PRESS ANY KEY TO GO TO MAIN MENU.");
                            Console.ReadLine();
                        }
                        break;
                    }
                case "0":
                default: return;
            }
        }

        private static void PrintApproximatedLobachevskyRoots(double[] roots)
        {
            Console.Clear();

            string method = ReadApproximationMethod();
            InitApproximationMethods(method);

            double[] approximatedRoots = LobachevskyMethod.GetApproximateRoots(GetRootFindingFunction, roots);

            for (int i = 0; i < roots.Length; i++)
            {
                Console.WriteLine("\tBefore: {0}   After: {1}", roots[i], approximatedRoots[i]);
            }
        }

        static void OutputApproximationConsole()
        {
            string option = ReadApproximationMethod();

            if (option == "0") return;

            while (true)
            {
                ReadEquationOptions();
                InitApproximationMethods(option);

                try
                {
                    Console.WriteLine("\n\tAproximated root: {0}\n", GetRootFindingFunction(function));
                }
                catch (Exception e)
                {
                    Console.WriteLine("\t{0}\n", e.Message);
                }

                Console.WriteLine("PRESS ENTER TO CHANGE THE OPTIONS OR'0' TO GO BACK.");

                if (Console.ReadLine() == "0") break;
            }
        }

        private static string ReadApproximationMethod()
        {
            Console.Clear();

            Console.WriteLine("Select root approximation method:");
            Console.WriteLine("1. Secant method.");
            Console.WriteLine("2. Descrete Newton method.");
            Console.WriteLine("3. Fixed-point iteration.");
            Console.WriteLine("0. Back");

            return Console.ReadLine();
        }

        private static void ReadEquationOptions()
        {
            Console.WriteLine("Enter xStart or left empty [Negative Infinity]:");

            string value = Console.ReadLine();
            xStart = (string.IsNullOrEmpty(value)) ? Double.NegativeInfinity : Convert.ToDouble(value);

            Console.WriteLine("Enter xEnd or left empty [Positive Infinity]:");

            value = Console.ReadLine();
            xEnd = (string.IsNullOrEmpty(value)) ? Double.PositiveInfinity : Convert.ToDouble(value);

            Console.WriteLine("Enter epsilon or left empty [{0}]:", Auxiliary.epsilon);

            value = Console.ReadLine();
            if (!string.IsNullOrEmpty(value)) Auxiliary.epsilon = Convert.ToDouble(value);

        }
        private static void InitApproximationMethods(string option)
        {
            switch (option)
            {
                case "1":
                    {
                        GetRootFindingFunction = approximation_methods.SecantMethod.GetRoot;
                        function = new Function(Functions.f1, xStart, xEnd);
                        break;
                    }
                case "2":
                    {
                        GetRootFindingFunction = approximation_methods.DiscreteNewtonMethod.GetRoot;
                        function = new Function(Functions.f2, xStart, xEnd);
                        break;
                    }
                case "3":
                default:
                    {
                        GetRootFindingFunction = approximation_methods.FixedPointIterationMethod.GetRoot;
                        function = new Function(Functions.f2, xStart, xEnd);
                        break;
                    }
            }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("1. Solve the given equation by the Lobachevsky method.");
                Console.WriteLine("2. Get approximated roots of given equation.");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        OutputLobachevskyConsole();
                        break;
                    case "2":
                        OutputApproximationConsole();
                        break;
                    default: return;
                }
            }
        }
    }
}
