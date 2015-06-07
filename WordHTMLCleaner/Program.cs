using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTMLCleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            var textFile = File.ReadAllText(args[0]);
            HTMLClean.Cleaner.CleanWordHTML(ref textFile);
            File.WriteAllText(args[1], textFile, Encoding.UTF8);
        }
    }
}
