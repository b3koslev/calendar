using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calendar
{
    internal class Program
    {
        static void PrintCalendar()
        {

        }

        static void NewEvent()
        {

        }

        static void ViewNextEvent()
        {
        }

        static void Exit()
        {

        }
        static void Main(string[] args)
        {
            Console.WriteLine("1. Naptár megjelenítése");
            Console.WriteLine("2. Új esemény hozzáadása");
            Console.WriteLine("3. Legközelebbi esemény megjelenítése");
            Console.WriteLine("4. Kilépés");

            Console.WriteLine();

            Console.Write("Válassz egy opciót (1-4): ");

            int choice = Convert.ToInt32(Console.ReadLine());
            if (choice == 1)
            {
                PrintCalendar();
            }
            else if (choice == 2)
            {
                NewEvent();
            }
            else if (choice == 3)
            {
                ViewNextEvent();
            }
            else if (choice == 4)
            {
                Exit();
            }
        }

        public struct Event
        {
            public string Parent;
            public DateTime Date;
            public int Duration;

            public Event(string parent, DateTime date, int duration)
            {
                Parent = parent;
                Date = date;
                Duration = duration;
            }
        }
    }
}
