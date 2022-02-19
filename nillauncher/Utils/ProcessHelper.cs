using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace nillauncher.Utils
{
    public class ProcessHelper
    {
        static int start_time = 0;
        public static Process CreateProcess(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException($"无法找到{filename}!");
            FileInfo fi = new FileInfo(filename);
            Process ps = new Process();
            ps.StartInfo.FileName = filename;
            ps.StartInfo.RedirectStandardOutput = true;
            ps.StartInfo.UseShellExecute = false;
            ps.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            ps.StartInfo.CreateNoWindow = true;
            ps.StartInfo.RedirectStandardInput = true;
            ps.StartInfo.RedirectStandardError = true;
            ps.StartInfo.WorkingDirectory = fi.DirectoryName;
            ps.EnableRaisingEvents = true;
            ps.StartInfo.StandardOutputEncoding = Runtime.encoding;
            ps.Exited += bds_exit;
            ps.OutputDataReceived += bds_console_output;
            return ps;
        }

        private static void bds_console_output(object sender, DataReceivedEventArgs e)
        {
            bds_output_line.online(e.Data);
        }

        public static void start_bds()
        {

            Runtime.bds = CreateProcess(Runtime.file);
            var b = new Thread(() =>
            {
                Runtime.bds.Start();
                Runtime.bds.BeginErrorReadLine();
                Runtime.bds.BeginOutputReadLine();
                Runtime.bds.WaitForExit(0);
            });
            b.IsBackground = true;
            b.Start();
        }
        private static void bds_exit(object sender, EventArgs e)
        {
            start_time++;
            Logger.info($"BDS进程退出，当前非正常启动次数：{start_time}");
            Program.ws.sendClose();
            if (!Runtime.exit_by_stop)
            {
                if (start_time != Runtime.rstart)
                {
                    Logger.info("进程非正常退出，即将重启");
                    start_bds();
                }
            }
            else
            {
                Logger.info("输入start可重新启动");
                Logger.info("输入backup进行备份");
                Logger.info("输入exit退出程序");
                start_time = 0;
            }
        }
    }
}
