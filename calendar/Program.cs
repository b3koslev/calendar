using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        static int CreateMenu()
        {
            Console.WriteLine("1. Naptár megjelenítése");
            Console.WriteLine("2. Új esemény hozzáadása");
            Console.WriteLine("3. Legközelebbi esemény megjelenítése");
            Console.WriteLine("4. Kilépés");

            Console.WriteLine();

            Console.Write("Válassz egy opciót (1-4): ");

            int choice = Convert.ToInt32(Console.ReadLine());

            return choice;
        }

        static string[,] CreateCalendar()
        {
            Console.Clear();

            Console.WriteLine("=========================================================");
            Console.WriteLine("\t\t\tFebruár");
            Console.WriteLine("=========================================================");
            Console.WriteLine();

            string[,] calendar = new string[6, 8];
            calendar[0, 1] = "H";
            calendar[0, 2] = "K";
            calendar[0, 3] = "Sze";
            calendar[0, 4] = "Cs";
            calendar[0, 5] = "P";
            calendar[0, 6] = "Szo";
            calendar[0, 7] = "V";
            calendar[1, 0] = "1. hét";
            calendar[2, 0] = "2. hét";
            calendar[3, 0] = "3. hét";
            calendar[4, 0] = "4. hét";
            calendar[5, 0] = "5. hét";

            int day = 0;

            for (int i = 1; i < calendar.GetLength(0); i++)
            {
                for (int j = 1; j < calendar.GetLength(1); j++)
                {
                    if (day > 29)
                    {
                        break;
                    }
                    else if (day != 0)
                    {
                        calendar[i, j] = Convert.ToString(day);
                        day++;
                    }
                    else
                    {
                        day++;
                    }
                }
            }

            return calendar;
        }

        static void PrintCalendar(string[,] calendar)
        {
            for (int i = 0; i < calendar.GetLength(0); i++)
            {
                for (int j = 0; j < calendar.GetLength(1); j++)
                {
                    Console.Write(calendar[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        static List<Event> RandomEvents(string[,] calendar)
        {
            List<Event> random_events = new List<Event>();

            Random random = new Random();

            string parent = string.Empty;
            DateTime date = new DateTime();
            int duration = 0;

            for (int i = 1; i <= calendar.GetLength(0); i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    int parents = random.Next(1, 3);
                    int day = random.Next(1, 30);
                    int hours = random.Next(8, 21);
                    int minutes = random.Next(0, 60);
                    int durations = random.Next(30, 121);

                    if (parents == 1)
                    {
                        parent = "anya";
                        date = new DateTime(2028, 2, day, hours, minutes, 0);
                        duration = durations;
                    }
                    else
                    {
                        parent = "apa";
                        date = new DateTime(2028, 2, day, hours, minutes, 0);
                        duration = durations;
                    }

                    random_events.Add(new Event(parent, date, duration));
                }
            }

            return random_events;
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

        static void ViewNextEvent(List<Event> event_list)
        {
            Random random = new Random();
            int day = random.Next(1, 30);
            bool ifEvent = false;

            DateTime date = new DateTime(2028, 02, day);
            
            for (int i = 0; i < event_list.Count; i++)
            {
                if (event_list[i].Date >= date)
                {
                    Console.WriteLine("A következő esemény adatai: ");
                    Console.WriteLine("Felhasználó: " + event_list[i].Parent);
                    Console.WriteLine("Dátum: " + event_list[i].Date);
                    Console.WriteLine("Időtartam: " + event_list[i].Duration + " perc");
                    ifEvent = true;
                    break;
                }
            }

            if (ifEvent == false)
            {
                Console.WriteLine("Nincs közelgő esemény!");
            }
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
            int choice = 0;
            List<Event> events = RandomEvents(CreateCalendar());

            choice = CreateMenu();

            do
            {
                if (choice == 1)
                {
                    PrintCalendar(CreateCalendar());
                    Console.WriteLine();
                    Console.Write("Kilépés a főmenübe...");
                    Console.ReadKey();
                    Console.Clear();
                    choice = CreateMenu();
                }
                else if (choice == 2)
                {
                    events.Add(NewEvent());
                    Console.WriteLine();
                    Console.WriteLine("Az esemény rögzítésre került.");
                    Console.Write("Kilépés a főmenübe...");
                    Console.ReadKey();
                    Console.Clear();
                    choice = CreateMenu();
                }
                else if (choice == 3)
                {
                    ViewNextEvent(events);
                    Console.WriteLine();
                    Console.Write("Kilépés a főmenübe...");
                    Console.ReadKey();
                    Console.Clear();
                    choice = CreateMenu();
                }
                else if (choice == 4)
                {
                    endOfCalendar = Exit();
                    Console.WriteLine("Kilépés...");
                }
            } while (endOfCalendar == false);

            StreamWriter file = new StreamWriter("naptar.txt", false, Encoding.UTF8);

            foreach (Event ev in events)
            {
                file.WriteLine($"{ev.Parent};{ev.Date};{ev.Duration}");
            }

            file.Close();
        }
    }
}
