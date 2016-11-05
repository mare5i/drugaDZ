using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace sesti.cs
{
    class Program
    {
        static void Main(string[] args)
        {
            object objlock = new object();


            List<int> results = new List<int>();
            Parallel.For(0, 100, (i) =>
            {
                Thread.Sleep(1);
                lock (objlock)
                {
                    results.Add(i * i);
                }
            });
            Console.WriteLine("Bag length should be 100. Length is {0}", results.Count);
        }
    }
}
