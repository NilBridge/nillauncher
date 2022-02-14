using KWO;
using nillauncher.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nillauncher
{
    class bds_output_line
    {
        public static string tmp = string.Empty;
        public static void online(string dt)
        {
            if (dt == null) return;
            if (dt.Contains( "No targets matched selector")) return;
            if (string.IsNullOrEmpty(dt)) return;
            if (Runtime.is_runcmd)
            {
                if (Runtime.is_list.@is)
                {
                    switch(Runtime.is_list.line)
                    {
                        case 0:

                            tmp = dt;
                            if(tmp.Contains("There are 0/"))
                            {
                                Runtime.is_list.@is = false;
                                Program.ws.sendCMD(tmp+"\n");
                                Runtime.is_runcmd = false;
                                return;
                            }
                            Runtime.is_list.line++;
                            return;
                        case 1:
                            Runtime.is_list.line = 0;
                            tmp += "\n" + dt;
                            Runtime.is_list.@is = false;
                            Program.ws.sendCMD(tmp);
                            Runtime.is_runcmd = false;
                            return;
                    }
                }
                Program.ws.sendCMD(dt);
                Runtime.is_runcmd = false;
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
            Console.WriteLine(dt);
        }
    }
}
