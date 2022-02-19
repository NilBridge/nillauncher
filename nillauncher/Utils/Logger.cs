using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nillauncher.Utils
{
    public class Logger
    {
        private static ConsoleColor defaultForegroundColor = ConsoleColor.White;
        private static ConsoleColor defaultBackgroundColor = ConsoleColor.Black;
        private static void ResetConsoleColor()
        {
            Console.ForegroundColor = defaultForegroundColor;
            Console.BackgroundColor = defaultBackgroundColor;
        }
        public static void info(string t,string moudle = "Nillauncher")
        {
            //Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} INFO [Nillauncher] {t}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"{DateTime.Now.ToString("HH:mm:ss")} ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("INFO ");
            ResetConsoleColor();
            Console.Write("[");
            Console.ForegroundColor = (ConsoleColor)new Random().Next(0,15);
            Console.Write(moudle);
            ResetConsoleColor();
            Console.Write("] ");
            Console.WriteLine(t);
        }
        public static void warn(string t, string moudle = "Nillauncher")
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"{DateTime.Now.ToString("HH:mm:ss")} ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("WARN ");
            ResetConsoleColor();
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(moudle);
            ResetConsoleColor();
            Console.Write("] ");
            Console.WriteLine(t);
        }
    }
}
