using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace geekbrains_csharp2_homework1
{
    public static class PrintFile
    {
        public static void Print(string msg)
        {
            using (FileStream fstream = new FileStream(@"Output.txt", FileMode.Append))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(msg); // строка в байты
                fstream.Write(array, 0, array.Length); // запись
            }
        }
    }
}
