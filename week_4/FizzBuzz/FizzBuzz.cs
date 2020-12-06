using System;

namespace FizzBuzz
{
    class FizzBuzz
    {
        public void work(int number)
        {
            bool exit = false;

            if (number % 3 == 0)
            {
                Console.Write("Fizz");
                exit = true;
            }
            if (number % 5 == 0)
            {
                Console.Write("Buzz");
                exit = true;
            }
            if (!exit)
            {
                Console.Write(number);
            }
            Console.WriteLine();
        }
    }
}
