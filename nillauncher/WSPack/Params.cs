using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBridge.WSPack
{
    public class Pos
    {
        /// <summary>
        /// 
        /// </summary>
        public double x { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double y { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double z { get; set; }
    }

    public class @params
    {
        /// <summary>
        /// 
        /// </summary>
        public string cmd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sender { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string result { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xuid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ip { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mobtype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mobname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int dmcase { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dmname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string srctype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string srcname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Pos pos { get; set; }
    }
}
