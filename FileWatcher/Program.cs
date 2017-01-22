using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileWatcher
{
    class Program
    {     
        static void Main(string[] args)
        {
            string path = "test.txt";
            var myFile=File.Create(path);
            myFile.Close();
            var watcher = new FileWatchers(path);
            
            watcher.ChangeFile += FileIsChange;
            watcher.Start();
            Console.ReadLine();
        }

        private static void FileIsChange()
        {
            string path = "test.txt";

            if (File.ReadAllText(path) == "1")
            {
                File.WriteAllText(path, "0");
                Thread.Sleep(10000);
                Console.WriteLine("File content changed to 0 ten seconds ago ");
            }
            else
            {                
                Thread.Sleep(10000);
                Console.WriteLine("File content is 0");
            }
        }
    }
}
