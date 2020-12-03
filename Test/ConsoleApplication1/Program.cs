using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            double amount = 40;
            for (int i = 0; i < 15; i++)
            {
                amount = amount * (1 + 0.12) + 10;
            }
            Console.WriteLine(amount);
            Console.ReadLine();
        }
    }
}
