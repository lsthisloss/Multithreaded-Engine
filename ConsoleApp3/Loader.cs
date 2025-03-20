using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Loader
    {
        public static bool work = false;
        public static List<string> AgentList = new List<string>();
        public static List<string> proxyList = new List<string>();
        public static List<Thread> threadList = new List<Thread>();
        public static List<string> uaList = new List<string>();
        public static int count = 0;
        public static string time = DateTime.Now.ToString("HH_mm_ss");
        public string path = "Results\\";
        public static string GetProxy()
        {
            int secondsSinceMidnight = Convert.ToInt32(DateTime.Now.Subtract(DateTime.Today).TotalMilliseconds);
            Random rand = new Random(secondsSinceMidnight);
            return Convert.ToString(proxyList[rand.Next(0, proxyList.Count)]);

        }
        public static string GetUA()
        {
            int secondsSinceMidnight = Convert.ToInt32(DateTime.Now.Subtract(DateTime.Today).TotalMilliseconds);
            Random rand = new Random(secondsSinceMidnight);
            return Convert.ToString(uaList[rand.Next(0, uaList.Count)]);
        }
    }
}
