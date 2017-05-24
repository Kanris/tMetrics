using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace tMetrics
{
    class GetMethod
    {
        private static string[] ident = new string[] { "private", "public", "protected" };

        public static List<string> getMethod(StreamReader sr)
        {
            string lineOfCode = sr.ReadLine();

            if (isMethod(lineOfCode) || isConstructor(lineOfCode))
            {

                int count = 0;
                List<string> method = new List<string>();

                if(lineOfCode.Contains("{")) count++;
                if (lineOfCode.Contains("}")) count--;

                for (int i = 0; !sr.EndOfStream; ++i)
                {
                    method.Add(sr.ReadLine());

                    if (method[i].Contains("{"))
                        count++;

                    if (method[i].Contains("}"))
                        count--;

                    if (count == 0)
                        break;
                }

                return method;
            }

            return null;
        }

        private static bool isMethod(string lineOfCode)
        {
            bool check = false;

            foreach (String line in ident)
            {
                if (lineOfCode.Contains(line) && lineOfCode.Contains("("))
                {
                    check = true;
                    break;
                }
            }

            return check;
        }

        private static bool isConstructor(string lineOfCode)
        {
            return lineOfCode.Contains(")");
        }
    }
}

