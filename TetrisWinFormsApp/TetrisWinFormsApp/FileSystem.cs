using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisWinFormsApp
{
    public  static class FileSystem
    {
        public static void SaveInformation(this string filePath, string information)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(information);
            }
        }
        public static string GetInformation(this string filePath)
        {
            string information;
            using (StreamReader sr = new StreamReader(filePath))
            {
                information = sr.ReadToEnd();
            }
            return information;
        }
    }
}
