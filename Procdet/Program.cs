/*!
 * UnUsedProcedures C#: Determin used and unused procedures from 
 * source files
 * License: http://creativecommons.org/licenses/by/2.5/ (basically, do anything you want, just leave my name)
 * (c) 2015 Apollo Namalu
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procdet
{
    internal class Program
    {
        /// <summary>
        /// Entry point for the program
        /// </summary>
        /// <param name="args">list of commandline arguments</param>
        private static void Main(string[] args)
        {
            Helper _helper = new Helper();
            Console.WriteLine();
            Console.WriteLine("===========OUTPUT==========");
            Console.WriteLine();
            //Get the second argument that holds the file/Directory
            var fileORDirectory = args[1];

            if (File.Exists(fileORDirectory))
            {
                // The path is a file
                _helper.ProcessFile(fileORDirectory);
            }
            else if (Directory.Exists(fileORDirectory))
            {
                // The path is a directory
                _helper.ProcessDirectory(fileORDirectory);
            }
            else
            {
                Console.WriteLine("{0} is not a valid file or directory.", fileORDirectory);
                Console.ReadKey();
            }

            Console.ReadKey();
        }
    }
}
