using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace sem3_lab9
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread A = new Thread(threadFun);
            Thread B = new Thread(threadFun);
            Thread C = new Thread(threadFun);
            Thread D = new Thread(threadFun);
            Thread E = new Thread(threadFun);
            Thread F = new Thread(threadFun);
            Thread G = new Thread(threadFun);
            Thread H = new Thread(threadFun);
            Thread I = new Thread(threadFun);
            A.Name = "A";
            B.Name = "B";
            C.Name = "C";
            D.Name = "D";
            E.Name = "E";
            F.Name = "F";
            G.Name = "G";
            H.Name = "H";
            I.Name = "I";
            A.Start(new Random().Next() % 2000);
            B.Start(new Random().Next() % 2000);
            C.Start(new Random().Next() % 2000);
            A.Join();
            E.Start(new Random().Next() % 2000);
            F.Start(new Random().Next() % 2000);
            B.Join();
            E.Join();
            D.Start(new Random().Next() % 2000);
            I.Start(new Random().Next() % 2000);
            D.Join();
            F.Join();
            C.Join();
            G.Start(new Random().Next() % 2000);
            G.Join();
            I.Join();
            H.Start(new Random().Next() % 2000);
            Console.ReadKey();
        }

        private static void threadFun(object obj)
        {
            int offset = (int)obj;
            Console.WriteLine("Thread " + Thread.CurrentThread.Name + " Started");
            for (int i = 0; i < 100; i += 5)
            {
                Console.WriteLine("Thread " + Thread.CurrentThread.Name + " " + i + "%");
                Thread.Sleep(offset);
                
            }
            Console.WriteLine("Thread " + Thread.CurrentThread.Name + " Finished");
        }

     
    }
}
