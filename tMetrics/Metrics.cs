using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace tMetrics
{
    public static class Metrics
    {
        public static void LOC(FileInfo[] pathToFile, out int[] lengthOfCode)
        {
            lengthOfCode = new int[pathToFile.Length];

            for (int i = 0; i < lengthOfCode.Length; ++i)
            {
                lengthOfCode[i] = LinesOfCode.LOC(pathToFile[i].FullName);
            }
        }

        public static void CYC(FileInfo[] pathToFile, out int[] cyclomatic)
        {
            cyclomatic = new int[pathToFile.Length];

            for (int i = 0; i < cyclomatic.Length; ++i)
            {
                cyclomatic[i] = CYCLO.CYC(pathToFile[i].FullName);
            }
        }
    }
}
