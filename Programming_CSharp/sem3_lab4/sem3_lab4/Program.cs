using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sem3_lab4
{
    class Program
    {
        // Программа возвращает минимальное слово в каждой строке текста входного файла
        // Если минимальных слов несколько, выводятся все
        // Минимальное слово в строке может не быть минимальным словом в тексте
        static void Main(string[] args)
        {
            char [] splitter = new char [] {' ', '.', ',', ';', ':', '!', '?', '\t', '(', ')'};

            try
            {
                foreach (string line in File.ReadAllLines("in.txt"))
                {
                    string[] words = line.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
                    if (words.Length > 0)
                    {
                        int minlen = words[0].Length;
                        foreach (string word in words)
                        {
                            if (word.Length < minlen)
                            {
                                minlen = word.Length;
                            }
                        }
                        foreach (string word in words)
                        {
                            if (word.Length == minlen)
                                Console.WriteLine(word);
                        }
                    }

                }
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
