using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Liczba całkowita : ");
            int num;
            if (int.TryParse(Console.ReadLine(),out num))
            {
                Console.Clear();
                FizzBuzz fizzbuzz = new FizzBuzz();
                fizzbuzz.work(num);
            }
            else
            {
                Console.WriteLine("złe dane wejściowe");
            }
            Console.ReadKey();
        }
    }
}
