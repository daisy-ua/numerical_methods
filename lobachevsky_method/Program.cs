using System;

namespace lobachevsky_method
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var item in LobachevskyMethod.Calculate())
            {
                Console.WriteLine(item);
            };
        }
    }
}
