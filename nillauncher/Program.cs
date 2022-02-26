using KWO;
using nillauncher.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace nillauncher
{
    class Program
    {
        public static WSS ws;
        public delegate bool ControlCtrlDelegate(int CtrlType);
        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleCtrlHandler(ControlCtrlDelegate HandlerRoutine, bool Add);
        private static ControlCtrlDelegate cancelHandler = new ControlCtrlDelegate(HandlerRoutine);
        public static bool HandlerRoutine(int CtrlType)
        {
            switch (CtrlType)
            {
                case 0:
                    Logger.warn("工具被强制关闭"); //Ctrl+C关闭  
                    if(Runtime.bds.HasExited==false)
                        Runtime.bds.Kill();
                    break;
                case 2:
                    Logger.warn("工具被强制关闭");//按控制台关闭按钮关闭  
                    if (Runtime.bds.HasExited == false)
                        Runtime.bds.Kill();
                    break;
            }
            return false;
        }
        static void Main(string[] args)
        {
            Runtime.LoadFile();
            RemoveCloseMenu();
            SetConsoleCtrlHandler(cancelHandler, true);
            Runtime.Parse(args);
            Logger.info($"using pwd {Runtime.ws_pwd}");
            Logger.info($"using filepath {Runtime.file}");
            Logger.info($"using restarttime {Runtime.rstart}");
            Logger.info("version 1.0.5.8");
            regex.loadFile();
            ProcessHelper.start_bds();
            ws = new WSS(Runtime.ws_port, Runtime.ws_pwd,Runtime.ws_endport);
            new Thread(() =>
            {
                while (true)
                {
                    string line = Console.ReadLine();
                    if (!Runtime.bds.HasExited)
                    {
                        Runtime.runcmd(line,false);
                    }
                    else
                    {
                        switch (line) {
                            case "start":
                                ProcessHelper.start_bds();
                                break;
                            case "backup":
                                if (Directory.Exists("backups") == false) { Directory.CreateDirectory("backups"); }
                                Runtime.runcmd("save hold");
                                try
                                {
                                    Logger.info("backuping " + getLevelName());
                                    ZipHelper.CompressDirectory($@"{new FileInfo(Runtime.file).DirectoryName}\worlds\{getLevelName()}\", $"./backups/{DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss")}.zip",false);
                                    Logger.info("备份成功！");
                                }
                                catch(Exception e)
                                {
                                    Logger.warn($"备份失败：{e}");
                                }
                                Runtime.runcmd("save resume");
                                //R7z.R7Zip($"{AppContext.BaseDirectory}worlds/{getLevelName()}", $"./backups/{DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss")}.7z");
                                break;
                            case "exit":
                                Logger.info("即将退出");
                                Environment.Exit(0);
                                break;
                        }
                    }
                }
            }).Start();
        }
        static string getLevelName()
        {
            string[] pro = File.ReadAllLines("server.properties");
            foreach (string line in pro)
            {
                if (line.StartsWith("level-name"))
                {
                    return line.Substring("level-name=".Length);
                }
            }
            return "Bedrock level";
        }
        public static class Win32Helper
        {
            [DllImport("User32.dll", EntryPoint = "FindWindow")]
            public static extern int FindWindow(string lpClassName, string lpWindowName);

            [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
            public static extern IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);

            [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
            public static extern int RemoveMenu(IntPtr hMenu, int nPos, int flags);
        }
        public static void RemoveCloseMenu()
        {
            // 与控制台标题名一样的路径
            // Process.GetCurrentProcess().MainModule.FileName;
            string fullPath = Console.Title;
            // 根据控制台标题找控制台
            int WINDOW_HANDLER = Win32Helper.FindWindow(null, fullPath);
            // 找关闭按钮
            IntPtr CLOSE_MENU = Win32Helper.GetSystemMenu((IntPtr)WINDOW_HANDLER, IntPtr.Zero);
            int SC_CLOSE = 0xF060;
            // 关闭按钮禁用
            Win32Helper.RemoveMenu(CLOSE_MENU, SC_CLOSE, 0x0);
        }
    }
}
