using System;

namespace IleŻyjesz_Dane
{
    class Program
    {
        static void Main(string[] args)
        {
            string name;
            do
            {
                Console.Clear();
                Console.WriteLine("Hej");
                Console.Write("Przedstawisz się ? (imie) : ");
                name = Console.ReadLine();
            } while (name == string.Empty);

            string location;
            do
            {
                Console.Clear();
                Console.Write("Skąd pochodzisz ? : ");
                location = Console.ReadLine();
            } while (location == string.Empty);

            string buff;
            DateTime data;
            do
            {
                Console.Clear();
                Console.Write("A którego się urodzięś ? (format: DD MM YYYY; separators: / . , - spacje) : ");
                buff = Console.ReadLine();
            } while (!DateTime.TryParse(buff, out data));

            Console.Clear();

            byte year;
            bool birthday = false;

            if (data.Month == DateTime.Now.Month)
            {
                if (data.Day == DateTime.Now.Day)
                {
                    birthday = true;
                    year = Convert.ToByte(DateTime.Now.Year - data.Year);
                }
                else
                {
                    year = Convert.ToByte(data.Day < DateTime.Now.Day ? DateTime.Now.Year - data.Year : DateTime.Now.Year - data.Year - 1);
                }
            }
            else
            {
                year = Convert.ToByte(data.Month < DateTime.Now.Month ? DateTime.Now.Year - data.Year : DateTime.Now.Year - data.Year - 1);
            }

            if (birthday)
            {
                Console.WriteLine($"Hej {name} pochodzisz z {location} i masz już dziś {year} lat");
                Console.WriteLine("100 lat");
            }
            else
            {
                Console.WriteLine($"Hej {name} pochodzisz z {location} i masz {year} lat");
            }
            Console.ReadKey();
        }
    }
}
