using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sem3_lab7
{
    
    
    class Program
    {
        
        // Функция вычисления факториала
        public static UInt64 Factorial(UInt64 n)
        {
            if (n == 0)
                return 1;
            else return n * Factorial(n - 1);
        }
        
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter your number. It must be non-negative integer.");
                UInt64 n = UInt64.Parse(Console.ReadLine());
                Func<UInt64, UInt64> del1 = Factorial; // Делегат
                Func<UInt64, UInt64> del2 = del1; // Делегат делегата
                Console.WriteLine("Factorial is " + del2(n));
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}
