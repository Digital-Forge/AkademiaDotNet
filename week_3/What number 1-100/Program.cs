using System;

namespace ZaganijLiczbe_1_100
{
    class Program
    {
        static void Main(string[] args)
        {
            byte rnd;
            {
                Random rand = new Random(DateTime.Now.Second);
                rnd = Convert.ToByte(rand.Next(1,101));
            }

            Console.WriteLine("Pomyślałem o liczbe z zakresu od 1 do 100 :)");
            Console.WriteLine("Proszę zgadnij jaka to liczba ;)");

            string s_buff;
            int b_buff;
            uint licznik = 1;
            while (true)
            {
                Console.Write("Podaj liczbę od 1 do 100 : ");
                s_buff = Console.ReadLine();
                Console.Clear();

                if (int.TryParse(s_buff,out b_buff))
                {
                    if (b_buff > 100 || b_buff < 1)
                    {
                        Console.WriteLine("Przekroczono zakres");
                    }
                    else
                    {
                        if (b_buff == rnd)
                        {
                            Console.WriteLine($"Zagadłeś za {licznik} razem :)");
                            Console.ReadKey();
                            break;
                        }
                        else if (b_buff > rnd)
                        {
                            Console.WriteLine("Liczba za duża");
                        }
                        else
                        {
                            Console.WriteLine("Liczba za mała");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Nie ma takiej liczby");
                }
                licznik++;
            }
        }
    }
}
