using KWO;
using nillauncher.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.IO;

namespace nillauncher
{
    class bds_output_line
    {
        public static string tmp = string.Empty;
        static int maching = 0;
        public static bool build(string input)
        {
            Match mat = Regex.Match(input,@"(.+) INFO \[(.+)\] (.+?)");
            if (mat.Success)
            {
                Logger.info(mat.Groups[3].Value, mat.Groups[2].Value);
                return false;
            }
            else
            {
                Match mat2 = Regex.Match(input,@"(.+) WARN \[(.+)\] (.+?)");
                if (mat2.Success)
                {

                    Logger.warn(mat2.Groups[3].Value, mat2.Groups[2].Value);
                    return false;
                }
            }
            return true;
        }
        public static void online(string dt)
        {
            if (dt == null) return;
            if (dt.Contains( "No targets matched selector")) return;
            if (string.IsNullOrEmpty(dt)) return;
            if (Runtime.is_runcmd.running)
            {
                tmp += dt + '\n';
                maching++;
                if (maching == Runtime.is_runcmd.line)
                {
                    Program.ws.sendCMD(tmp);
                    tmp = string.Empty;
                    Runtime.is_runcmd.running = false;
                    maching = 0;
                    return;
                }
                return;
            }
            try
            {
                regex.on_regex(dt);
                if (dt.Contains("Server started.") || dt.Contains("For help, type \"help\" or \"?\""))
                {
                    Program.ws.sendStart();
                    Runtime.exit_by_stop = false;
                }
            }
            catch (Exception ex) { Logger.warn(ex.ToString()); }
            try
            {
                if (build(dt)) Console.WriteLine(dt);
            }
            catch(Exception e)
            {
                Console.WriteLine(dt);
            }

        }
    }
}
