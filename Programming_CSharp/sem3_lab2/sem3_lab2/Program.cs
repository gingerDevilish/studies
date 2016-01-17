using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sem3_lab2
{
    class Program
    {
        // Класс целочисленных множеств, состоящий из списка целых, конструкторов и методов
        public class IntegerSet
        {
            // Переменные
            private List<Int32> intset; // Здесь хранятся элементы множества
           
            // Конструкторы
            public IntegerSet(Int32[] a): this( new List<Int32>(a) )
            {
            }

            // Передача управления из любого конструктора в этот обеспечивает единообразный вид всех объектов
            // Независимо от содержимого входных аргументов, объект не содержит повторяющихся элементов
            // Также для удобства представления элементы сортируются
            public IntegerSet(List<Int32> l)
            {
                l.Sort();
                int k = 0;
                while (k < (l.Count - 1))
                {
                    if (l[k] == l[k + 1])
                        l.RemoveAt(k+1);
                    else
                        k++;
                }
                intset = l;
            }

            // Конструктор по умолчанию создает пустой список
            public IntegerSet(): this(new Int32 []{})
            {
            }

            // Перегруженные операторы

            // Явное приведение типов
            public static explicit operator List<Int32>(IntegerSet a)
            {
                return a.intset;
            }
            public static explicit operator IntegerSet(List<Int32> a)
            {
                return new IntegerSet(a);
            }

            // Объединение множеств
            public static IntegerSet operator +(IntegerSet a, IntegerSet b) 
            {
                IntegerSet c = new IntegerSet(a.intset.Union(b.intset).ToList<Int32>());
                return c;
            }
            
            // Пересечение множеств
            public static IntegerSet operator *(IntegerSet a, IntegerSet b)
            {
                IntegerSet c = new IntegerSet(a.intset.Intersect(b.intset).ToList<Int32>());
                return c;
            }

            // Разность множеств
            public static IntegerSet operator -(IntegerSet a, IntegerSet b)
            {
                IntegerSet c = new IntegerSet(a.intset.Except(b.intset).ToList<Int32>());
                return c;
            }

            // Равенство множеств
            public static Boolean operator ==(IntegerSet a, IntegerSet b)
            {
                if (a.intset.Count != b.intset.Count)
                    return false;
                for (int i = 0; i < a.intset.Count; i++)
                {
                    if (a.intset[i] != b.intset[i])
                        return false;
                }
                return true;
            }

            // Неравенство множеств
            public static Boolean operator !=(IntegerSet a, IntegerSet b)
            {
                if (a.intset.Count != b.intset.Count)
                    return true;
                for (int i = 0; i < a.intset.Count; i++)
                {
                    if (a.intset[i] != b.intset[i])
                        return true;
                }
                return false;
            }

            // Вложенность множеств
            public static Boolean operator <=(IntegerSet a, IntegerSet b)
            {
                if (a.intset.Count > b.intset.Count)
                    return false;
                foreach (Int32 x in a.intset)
                {
                    if (!b.intset.Contains(x))
                        return false;
                }
                return true;
            }
            public static Boolean operator >=(IntegerSet a, IntegerSet b)
            {
                if (a.intset.Count < b.intset.Count)
                    return false;
                foreach (Int32 x in b.intset)
                {
                    if (!a.intset.Contains(x))
                        return false;
                }
                return true;
            }

            // Неявное приведение типов
            public static implicit operator IntegerSet(Int32 a)
            {
                return new IntegerSet(new Int32[]{a});
            }

            //Функции

            //Вывод в строку
            public String ToString()
            {
              
                StringBuilder temp=new StringBuilder();
                foreach (Int32 n in this.intset)
                   temp.Append(n.ToString()+" ");
                return temp.ToString();
            }
        }
        
        // Функция Main используется просто, чтобы показать, что созданный класс работает
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter your first integer set using spaces. Press ENTER to continue.");
                String s1 = Console.ReadLine();
                Console.WriteLine("Enter your second integer set using spaces. Press ENTER to continue.");
                String s2 = Console.ReadLine();
                String[] arr1 = s1.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                String[] arr2 = s2.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                Int32[] n1 = new Int32[arr1.Length];
                Int32[] n2 = new Int32[arr2.Length];
                for (int i = 0; i < n1.Length; i++)
                    n1[i] = Int32.Parse(arr1[i]);
                for (int i = 0; i < n2.Length; i++)
                    n2[i] = Int32.Parse(arr2[i]);
                IntegerSet i1 = new IntegerSet(n1);
                IntegerSet i2 = new IntegerSet(n2);
                Console.WriteLine(i1.ToString());
                Console.WriteLine(i2.ToString());
                Console.WriteLine("Union: " + (i1 + i2).ToString());
                Console.WriteLine("Intersection: " + (i1 * i2).ToString());
                Console.WriteLine("Set difference: " + (i1 - i2).ToString());
                Console.WriteLine("Equals: " + ((i1 == i2) ? "yes" : "no"));
                Console.WriteLine("Is A a subset to B?: " + ((i1 <= i2) ? "yes" : "no"));
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
