using System;

namespace EvenNumberOrNot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Liczba całkowita : ");
            int num;

            if (int.TryParse(Console.ReadLine(), out num))
            {
                if (num % 2 == 0)
                {
                    Console.WriteLine("liczba jest parzysta");
                }
                else
                {
                    Console.WriteLine("liczba nie jest parzysta");
                }
            }
            else
            {
                Console.WriteLine("To nie jest liczba całkowita");
            }
            Console.ReadKey();
        }
    }
}
