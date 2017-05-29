using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace tMetrics.Library
{
    public static class Metrics
    {
        public async static Task<int[]> LOC(FileInfo[] pathToFile)
        {
            var lengthOfCode = new int[pathToFile.Length];

            for (int i = 0; i < lengthOfCode.Length; ++i)
            {
                lengthOfCode[i] = await Task.Run(() => LinesOfCode.LOC(pathToFile[i].FullName));
            }

            return lengthOfCode;
        }

        public async static Task<int[]> CYC(FileInfo[] pathToFile)
        {
            var cyclomatic = new int[pathToFile.Length];

            for (int i = 0; i < cyclomatic.Length; ++i)
            {
                cyclomatic[i] = await Task.Run(() => CYCLO.CYC(pathToFile[i].FullName));
            }

            return cyclomatic;
        }
    }
}
