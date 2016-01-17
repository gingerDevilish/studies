using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sem3_lab6
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                String[] names = new String[] { "Tom", "John", "Azzazzazz", "Lady Fly", "Bear", "Donna", "Kitniss" };
                Console.WriteLine("Enter a name:");

                // Считывание имени и проверка его корректности
                String toFind = Console.ReadLine();
                // Имя должно начинаться с большой буквы
                if (!Char.IsUpper(toFind[0]))
                {
                    throw new ArgumentException("Name must start with an upper case letter!");
                }
                // В имени не должно быть цифр и знаков препинания
                // Имя должно использовать только один алфавит
                foreach (Char c in toFind)
                {
                    if ((Char.IsDigit(c)) || (Char.IsPunctuation(c)))
                        throw new ArgumentException("Name must not contain digits or punctuation!");
                    if ((Char.IsLetter(c)) && ((c < 'A') || (c > 'z') || ((c > 'Z') && (c < 'a'))))
                        throw new ArgumentException("Name must contain only English letters!");
                }

                // Поиск имени
                Int32 n = Array.IndexOf(names, toFind);
                if (n == -1)
                    Console.WriteLine("Not found");
                else
                    Console.WriteLine("Found: #" + n);
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
