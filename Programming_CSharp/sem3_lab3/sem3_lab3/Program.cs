using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sem3_lab3
{
    
    
    class Program
    {
        // Функция сортировки прямыми вставками. Вроде работает
        static void sortInsert( Int32[]  num)
        {
            int i, j;
            Int32 temp;
            for (i = 1; i < num.Length; i++)
            {
                temp = num[i];
                for (j = i - 1; (j >= 0) && (temp < num[j]); j--)
                {
                    num[j + 1] = num[j];
                }
                num[j + 1] = temp;

            }
        }

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter your integer numbers using any splitters:");
                String initial = Console.ReadLine();
                Char[] splitters = new Char[] { ' ', ',', '.', '\t' };
                String[] numbers = initial.Split(splitters, StringSplitOptions.RemoveEmptyEntries);
                Int32[] num = new Int32[numbers.Length];
                for (int i = 0; i < num.Length; i++)
                {
                    num[i] = Int32.Parse(numbers[i]);
                }
                sortInsert(num);
                foreach (Int32 n in num)
                {
                    Console.Write(n + " ");
                }
                Console.ReadKey();
            }
            catch(Exception fEx)
            {
                Console.WriteLine(fEx.Message);
            }
            
        }
    }
}
