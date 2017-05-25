using System.Collections.Generic;
using System.IO;

namespace tMetrics.Library
{
    class CYCLO
    {
        public static int CYC(string path)
        {
            int cyclo = 0;

            using (StreamReader sr = new StreamReader(path))
            {
                
                while (!sr.EndOfStream)
                {
                    List<string> method = GetMethod.getMethod(sr);

                    if (method != null)
                    {
                        int count = 1;
                        string bigline = "";

                        foreach (string lineOfCode in method)
                        {
                            if (lineOfCode.Length > 1)
                            {
                                bigline += lineOfCode;
                                count += CheckForCategory.CountOfCategories(lineOfCode);
                            }
                        }

                        cyclo += count;
                    }
                }
            }

            return cyclo;

        }
     }  
}
