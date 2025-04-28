using System;
using System.Timers;
using Time;
using System.Text;

/*Використовуючи делегати, написати клас Timer, який може виконати певний метод
  кожних t секунд.*/

namespace Time
{    
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = UTF8Encoding.UTF8;
            Timer timer = new Timer(4);
            timer.OnTick += ShowTime;   
            timer.Start();

            Console.WriteLine("Натисніть Enter для зупинки");
            Console.ReadLine();
            timer.Stop();
        }

        static void ShowTime()
        {
            Console.WriteLine($"Таймер спрацював у {DateTime.Now}");
        }
    }
}