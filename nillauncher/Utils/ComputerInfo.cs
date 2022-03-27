using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace nillauncher.Utils
{
    class ComputerInfo
    {
        public class PhysicalMemory
        {
            public string total { get; set; }
            public string used { get; set; }
            public string less { get; set; }
            public string percent { get; set; }
        }
        public static PhysicalMemory Memory()

        {
            var m = new PhysicalMemory();
            ManagementClass cimobject1 = new ManagementClass("Win32_PhysicalMemory");
            ManagementObjectCollection moc1 = cimobject1.GetInstances();
            double available = 0, capacity = 0;
            foreach (ManagementObject mo1 in moc1)
            {
                capacity += ((Math.Round(Int64.Parse(mo1.Properties["Capacity"].Value.ToString()) / 1024 / 1024 / 1024.0, 1)));
            }
            moc1.Dispose();
            cimobject1.Dispose();
            //获取内存可用大小
            ManagementClass cimobject2 = new ManagementClass("Win32_PerfFormattedData_PerfOS_Memory");
            ManagementObjectCollection moc2 = cimobject2.GetInstances();
            foreach (ManagementObject mo2 in moc2)
            {
                available += ((Math.Round(Int64.Parse(mo2.Properties["AvailableMBytes"].Value.ToString()) / 1024.0, 1)));
            }
            moc2.Dispose();
            cimobject2.Dispose();
            m.total = capacity.ToString();
            m.less = available.ToString();
            m.used = ((capacity - available)).ToString();
            m.percent = (Math.Round((capacity - available) / capacity * 100, 0)).ToString();
            return m;
        }
        public static string CPU()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select PercentProcessorTime from Win32_PerfFormattedData_PerfOS_Processor WHERE Name=\"_Total\"");
            var cpuItem = searcher.Get().Cast<ManagementObject>().Select(item => new { PercentProcessorTime = item["PercentProcessorTime"] }).First();
            if (cpuItem != null && cpuItem.PercentProcessorTime != null)
            {
                var rad = new Random();
                return cpuItem.PercentProcessorTime.ToString() + $".{rad.Next(1, 9)}";
            }
            else
                return "获取失败";
        }
    }
}
