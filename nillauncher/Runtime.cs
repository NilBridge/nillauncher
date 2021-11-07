﻿using nillauncher.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace nillauncher
{
    public  class Runtime
    {
        public static int ws_port = 8080;
        public static string ws_endport = "/mcws";
        public static string ws_pwd = "password";
        public static string file = "./bedrock_server.exe";
        public static int rstart = 5;
        public static bool exit_by_stop = false;
        public static Process bds;
        public class is_list { public static bool @is = false;public static int line = 0; };
        public static bool is_runcmd = false;
        public static void runcmd(string t)
        {
            if (t == "stop")
                exit_by_stop = true;
            if (t == "list")
                is_list.@is = true;
            bds.StandardInput.WriteLine(t);
        }
        public static void Parse(string [] arg)
        {
            var arguments = CommandLineArgumentParser.Parse(arg);
            if (arguments.Has("-ff"))
            {
                Logger.info("loading setting from nillauncher.json...");
                var tmp = JsonConvert.DeserializeObject<setting>(File.ReadAllText("nillauncher.json"));
                ws_endport = tmp.ws_endport;
                ws_port = tmp.ws_port;
                ws_pwd = tmp.ws_key;
                file = tmp.file;
                rstart = tmp.restart;
                return;
            }
            if (arguments.Has("-f"))
            {
                file = arguments.Get("-f").Next;
            }
            if (arguments.Has("-p"))
            {
                ws_port = int.Parse(arguments.Get("-p").Next);
            }
            if (arguments.Has("-r"))
            {
                rstart = int.Parse(arguments.Get("-r").Next);
            }
            if (arguments.Has("-ep"))
            {
                ws_endport = arguments.Get("-ep").Next;
            }
            if (arguments.Has("-pwd"))
            {
                ws_pwd = arguments.Get("-pwd").Next;
            }
        }
        public class setting
        {
            public string ws_endport { get; set; }
            public string ws_key { get; set; }
            public int ws_port { get; set; }
            public string file { get; set; }
            public int restart { get; set; }
        }
    }
}
