using System;
using System.IO;
using System.Linq;

namespace Statement
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            string directory = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

            Console.WriteLine(DirectoryCountLines(directory));
        }

        private static int DirectoryCountLines(string directory)
        {
            return Directory.GetFiles(
                directory, "*.cs").Sum(CountLines) + 
                   Directory.GetDirectories(directory).Sum(DirectoryCountLines);
        }

        private static int CountLines(string file)
        {
            int lineCount = 0;
            FileStream stream = new FileStream(file, FileMode.Open);
            StreamReader reader = new StreamReader(stream);
            string line = reader.ReadLine();
            while (line != null)
            {
                if (line.Trim() != "")
                {
                    lineCount++;
                }

                line = reader.ReadLine();
            }
            reader.Close();
            return lineCount;
        }
    }
    
}