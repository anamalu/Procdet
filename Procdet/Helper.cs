/*!
 * UnUsedProcedures C#: Determin used and unused procedures from 
 * source files
 * License: http://creativecommons.org/licenses/by/2.5/ (basically, do anything you want, just leave my name)
 * (c) 2015 Apollo Namalu
 */

using System;
using System.Collections.Generic;
using System.IO;
using Procdet;

namespace Procdet
{
    internal class Helper
    {
        /// <summary>
        /// <see cref="ProcessDirectory"/> check to make sure the specified directory has file(s)
        /// </summary>
        /// <param name="path"></param>
        public void ProcessDirectory(string path)
        {
            string[] fileEntries = Directory.GetFiles(path);
            if (fileEntries.Length <= 0)
            {
                Console.WriteLine("{0} has no files to process.", path);
                Console.ReadKey();
            }
            else
            {
                foreach (string fileName in fileEntries)
                {
                    ProcessFile(fileName);
                }
            }
        }
        /// <summary>
        /// <see cref="ProcessFile"/> process received files by invoking respective procedures, then prints results
        /// </summary>
        /// <param name="filePath"></param>
        public void ProcessFile(string filePath)
        {
            ProcedureNameProcessor procedureNames = new ProcedureNameProcessor();

            Console.WriteLine("=================================");
            Console.WriteLine(" ALL PROCEDURES INSIDE: {0}", filePath);
            Console.WriteLine("=================================");
            Console.WriteLine();

            List<string> allProcedures = procedureNames.GetProcedureNames(filePath);
            foreach (var procedureName in allProcedures)
            {
                Console.WriteLine("--->" + procedureName.Substring(0, procedureName.IndexOf("(")));
            }
            Console.WriteLine();
            Console.WriteLine("==================================");
            Console.WriteLine("=======UNUSED PROCEDURES=========");
            Console.WriteLine("==================================");
            foreach (var unUsedprocedure in procedureNames.GetUnUsedProcedures(filePath, allProcedures))
            {
                Console.WriteLine("--->" + unUsedprocedure.Substring(0, unUsedprocedure.IndexOf("(")));
            }
            Console.WriteLine("==================================");
            Console.WriteLine();
        }
    }
}