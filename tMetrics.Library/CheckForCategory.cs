using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tMetrics.Library
{
    static class CheckForCategory
    {
        private static string[] selection = new string[] { "if", "case", "default" };
        private static string[] loops = new string[] { "for", "while", "break", "continue" };
        private static string[] operators = new string[] { "&&", "||", "?", ":" };
        private static string[] exceptions = new string[] { "catch", "finally", "throw" };

        private static string[][] group = new string[][]
        {
            selection,
            loops,
            operators,
            exceptions
        };


        public static int CountOfCategories(string lineOfCode)
        {
            int count = 0;

            foreach (var item in group)
            {
                count += checkCategory(item, lineOfCode);
            }

            return count;
        }

        private static int checkCategory(string[] categorys, string lineOfCode)
        {
            int count = 0;

            foreach (string category in categorys)
            {
                if (lineOfCode.Contains(category) && !isString(lineOfCode))
                {
                    ++count;
                    break;
                }
            }

            return count;
        }

        private static bool isString(string lineOfCode)
        {
            return lineOfCode.Contains("\"");
        }
    }
}
