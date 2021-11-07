using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nillauncher.Utils
{
    public class Logger
    {
        public static void info(string t)
        {
            Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} INFO] {t}");
        }
        public static void warn(string t)
        {
            Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} WARN] {t}");
        }
    }
}
