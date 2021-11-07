using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using XBridge.Config;
using nillauncher;
using nillauncher.Utils;

namespace KWO
{
    class regex
    {
        public static List<RegexItem> regexs = new List<RegexItem>
        {
            new RegexItem{
                Regex = "\\[INFO\\] Player disconnected: (.+),",
                @out = new Out{
                    type = "lleft",
                    text = "$1"
                }
            },
            new RegexItem{
                Regex = "\\[INFO\\] Player connected: (.+),",
                @out = new Out
                {
                    type = "join",
                    text ="$1"
                }
            },
            new RegexItem{
                Regex = "\\[Chat\\]\\s<(.+?)> (.+)",
                @out = new Out
                {
                    type = "chat",
                    text = "$1|$2"
                }
            }

        };
        public static void on_regex(string input)
        {
            foreach (var i in regexs)
            {
                Match mat = Regex.Match(input, i.Regex);
                if (mat.Success)
                {
                    string o = i.@out.text;
                    for (int io = 1; io < mat.Groups.Count; io++)
                    {
                        o = o.Replace($"${io}", mat.Groups[io].Value);
                    }
                    o = buildString(o);
                    try
                    {
                        on_regex_item(i.@out, o);
                    }
                    catch { }
                    
                }
            }
        }
        private static void on_regex_item(Out it, string o)
        {
            switch (it.type) {
                case "chat":
                    Program.ws.sendChat(o.Split('|')[0], o.Split('|')[1]);
                    break;
                case "join":
                    Program.ws.sendEvent("join", o);
                    break;
                case "left":
                    Program.ws.sendEvent("left", o);
                    break;
                case "log":
                    Logger.info(o + Environment.NewLine);
                    break;
                case "plantext":
                    Program.ws.sendPlanText(o);
                    break;
            }
        }
        private static string buildString(string input)
        {
            StringBuilder builder = new StringBuilder(input);
            builder.Replace("%random%", new Random().Next(1, 100).ToString());
            builder.Replace("%datetime%", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            return builder.ToString();
        }
    }
}
namespace XBridge.Config
{
    public class Out
    {
        /// <summary>
        /// 执行模式
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 执行命令
        /// </summary>
        public string text { get; set; }
    }

    public class RegexItem
    {
        /// <summary>
        /// 正则主体
        /// </summary>
        public string Regex { get; set; }
        /// <summary>
        /// 响应组
        /// </summary>
        public Out @out { get; set; }
    }
}