using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace tMetrics
{
    class LinesOfCode
    {
        public static int LOC(string path)
        {
            int count = 0;

            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string lineOfCode = sr.ReadLine();

                    if (!isEmpty(lineOfCode))
                    {
                        ++count;
                    }
                    
                }
            }

            return count;
        }

        private static bool isEmpty(string line)
        {
            return line.Length < 0;
        }
    }
}
