using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Helper
    {
        public static string path_Results = "\\"  + "_" + DateTime.Now.ToString("dd.MM.yyyyTHH-mm");
        public static string path_Sorted = path_Results + "\\Sorted";
        public static string ParsingStuff(string strSource, string strStart, string strEnd, int startPos = 0)
        {
            string result = string.Empty;
            try
            {
                int length = strStart.Length,
                    num = strSource.IndexOf(strStart, startPos),
                    num2 = strSource.IndexOf(strEnd, num + length);
                if (num != -1 & num2 != -1)
                    result = strSource.Substring(num + length, num2 - (num + length));
            }
            catch (Exception ex) { File.WriteAllText("Error.txt", ex.Message); }
            return result;
        }
    }
}
