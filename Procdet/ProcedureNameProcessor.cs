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

namespace Procdet
{
    public class ProcedureNameProcessor
    {
        /// <summary>
        /// This procedure <see cref="GetProcedureNames"></see>
        ///     fetches all defined procedures from a source file
        /// </summary>
        /// <param name="fileName">file containg the source code</param> 
        /// <returns> allProceduresList,  a list of all distinct procedures</returns>
        public List<string> GetProcedureNames(string fileName)
        {
            List<string> allProceduresList = new List<string>();
            var strMethodLines = File.ReadAllLines(Path.GetFullPath(fileName))
                .Where(a => (a.Contains("protected") ||
                             a.Contains("private") ||
                             a.Contains("public")) ||
                            a.Contains("function") &&
                            !a.Contains("_") && !a.Contains("class"));
            foreach (var item in strMethodLines)
            {
                if (item.IndexOf("(") != -1)
                {
                    string strTemp = String.Join("",
                        item.Substring(0, item.IndexOf(")", StringComparison.Ordinal)).Reverse());
                    allProceduresList.Add(String.Join("", strTemp.Substring(0, strTemp.IndexOf(" ")).Reverse()));
                }
            }
            return allProceduresList.Distinct().ToList();
        }

        /// <summary>
        /// This procedure <see cref="GetUnUsedProcedures"/>
        /// fetches all invoked procedures from a source file and does a comparison
        /// with all procedures from <see cref="GetProcedureNames"/> to get unused procedures
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="procedures"></param>
        /// <returns> unCalledProcedures </returns>
        public List<string> GetUnUsedProcedures(string fileName, List<string> procedures)
        {
            List<string> calledProcedures = new List<string>();

            //Fetching only invoked procedures
            foreach (var procedure in procedures)
            {
                var strProcedureLines = File.ReadAllLines(Path.GetFullPath(fileName))
                    .Where(a => a.Contains(procedure.ToString()) &&
                                !a.Contains("private") &&
                                !a.Contains("public") &&
                                !a.Contains("function") &&
                                !a.Contains("_") && !a.Contains("class"));

                foreach (var item in strProcedureLines)
                {
                    if (item.Contains(procedure))
                    {
                        if (procedure.IndexOf(".") != -1)
                        {
                            var a = procedure.IndexOf(".");
                            var b = procedure.IndexOf("(");
                            string strTemp = String.Join("",
                                procedure.Substring(procedure.IndexOf("."),
                                    (procedure.IndexOf(")", StringComparison.Ordinal)) - procedure.IndexOf("."))
                                    .Reverse());
                            calledProcedures.Add(String.Join("", strTemp.Substring(0, strTemp.IndexOf(".")).Reverse()));
                        }
                        else
                        {
                            calledProcedures.Add(procedure);
                        }
                    }
                }
            }
            //extracting unused procedures
            var unCalledProcedures = procedures.Except(calledProcedures.Distinct().ToList());
            return unCalledProcedures.Distinct().ToList();
        }
    }
}