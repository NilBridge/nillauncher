using KWO;
using nillauncher.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace nillauncher
{
    class Program
    {
        public static WSS ws;
        static void Main(string[] args)
        {
            Runtime.Parse(args);
            Logger.info($"using pwd {Runtime.ws_pwd}");
            Logger.info($"using filepath {Runtime.file}");
            Logger.info($"using restarttime {Runtime.rstart}");
            Logger.info("version 1.0.3.1");
            ProcessHelper.start_bds();
            ws = new WSS(Runtime.ws_port, Runtime.ws_pwd,Runtime.ws_endport);
            new Thread(() =>
            {
                while (true)
                {
                    string line = Console.ReadLine();
                    if (!Runtime.bds.HasExited)
                    {
                        Runtime.runcmd(line);
                    }
                    else
                    {
                        switch (line) {
                            case "start":
                                ProcessHelper.start_bds();
                                break;
                        }
                    }
                }
            }).Start();
        }
    }
}
