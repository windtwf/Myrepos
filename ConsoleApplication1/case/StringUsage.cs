using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
  
    public class StringUsage
    {
        public void Usage()
        {
            string str = "<div><a title=\"Click to collapse. Double-click to collapse all.\" class=\"LW_CollapsibleArea_TitleAhref\" href=\"javascript:void(0)\"><span class=\"cl_CollapsibleArea_expanding LW_CollapsibleArea_Img\"></span><span class=\"LW_CollapsibleArea_Title\">Syntax</span></a><div class=\"LW_CollapsibleArea_HrDiv\"><hr class=\"LW_CollapsibleArea_Hr\"></div></div>";
            string str1 = "\r\n asdfsa \r\n";
            string b = str1.Replace(System.Environment.NewLine, "haha");
            //string ssss="abcdefg";
            //string a = ssss.Split('a', 'e')[1];
            string a = str.Split('"', '"')[1];
            Console.WriteLine(b);
            Console.ReadKey();

            //foreach (int i in Power(2, 8))
            //{
            //    Console.WriteLine("{0}", i);
            //    Console.ReadLine();
            //}
        }

        public void SubStringUse()
        {
            string a ="res://ieframe.dll/http_500.htm#http://msdn.microsoft.com/en-us/selectlocale-dmc?action=SelectLocale&currentLocale=en-us&newLocale=zh-cn&fromPage=%2Fen-us%2Flibrary%2Fbb229148.aspx%26%2339%3B%3Bonmouseover%3Dalert%281337%29";
            string b = a.Substring(a.IndexOf("#")+1);
            int c = 2;
        }

        
        public void StringBuildTest()
        {
            StringBuilder sb = new StringBuilder();
            string a = "abc";
            sb.Append("lu\r\n");
            sb.Append("ya");
            Console.WriteLine(sb);
            Console.ReadLine();
        }
    }
}
