using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace tMetrics
{
    class Metrics
    {
        public int LOC(string path)
        {
            return LinesOfCode.LOC(path);       
        }

        public int CYC(string path)
        {
            return CYCLO.CYC(path);
        }
    }
}
