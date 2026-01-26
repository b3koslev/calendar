using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calendar
{
    internal class Program
    {
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

        static void PrintCalendar()
        {

        }

        static Event NewEvent()
        {
            Console.Clear();

            string parent = string.Empty;
            DateTime date;
            int duration;

            do
            {
                Console.WriteLine("Add meg a felhasználót (anya/apa): ");
                parent = Console.ReadLine();
                parent = parent.ToLower();
                if (parent != "anya" && parent != "apa")
                {
                    Console.WriteLine("Hibás felhasználó! Kérlek, próbáld újra.");
                }
            } while (parent != "anya" && parent != "apa");

            do
            {
                Console.WriteLine("Add meg az esemény dátumát (ÉÉÉÉ-HH-NN ÓÓ:PP): ");
                date = Convert.ToDateTime(Console.ReadLine());  
                if (date.Year != 2028 || date.Month != 02 || date.Hour < 8 || date.Hour > 20)
                {
                    Console.WriteLine("Hibás dátum! Kérlek, próbáld újra.");
                }
            } while (date.Year != 2028 || date.Month != 02 || date.Hour < 8 || date.Hour > 20);

            do
            {
                Console.WriteLine("Add meg az esemény időtartamát (perc): ");
                duration = Convert.ToInt32(Console.ReadLine());
                if (duration < 30 || duration > 120)
                {
                    Console.WriteLine("Hibás időtartam! Kérlek, próbáld újra.");
                }
            } while (duration < 30 || duration > 120);

            Event new_event = new Event(parent, date, duration);

            return new_event;
        }

        static void ViewNextEvent()
        {
        }

        static bool Exit()
        {
            return true;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("================================================="); 
            Console.WriteLine("\tNaptár alkalmazás - Február hónap");
            Console.WriteLine("=================================================");
            Console.WriteLine();

            bool endOfCalendar = false;
            List<Event> events = new List<Event>();

            do
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
                    events.Add(NewEvent());
                }
                else if (choice == 3)
                {
                    ViewNextEvent();
                }
                else if (choice == 4)
                {
                    endOfCalendar = Exit();
                    Console.WriteLine("Kilépés...");
                }
            } while (false);

            StreamWriter file = new StreamWriter("naptar.txt", false, Encoding.UTF8);

            foreach (var ev in events)
            {
                file.WriteLine($"{ev.Parent};{ev.Date};{ev.Duration}");
            }

            file.Close();
        }
    }
}
