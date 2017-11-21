using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
   public class RegexTest
    {
       public void RegexTestMethod()
       {
           string pattern = @"(\d{3})-(\d{3}-\d{4})";
           string input = "212-555-6666 906-932-1111 415-222-3333 425-888-9999";
           MatchCollection matches = Regex.Matches(input, pattern);

           foreach (Match match in matches)
           {
               string s = match.Groups[0].Value;
               string a = match.Groups[1].Value;
               string b = match.Groups[2].Value;
               string f = match.Groups[3].Value;
           }
       }
    }
}
