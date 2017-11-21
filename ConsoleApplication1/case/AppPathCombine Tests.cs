using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class AppPathCombine_Tests
    {
        private string link="www.luding.com";
        private string xmlDoc = "";
        public void AppCombineTest()
        {
            int AppValue = Int32.Parse(ConfigurationManager.AppSettings["ludingSet"]);
            string address = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
            string status = link + "-->";
            int b = 10;
            status += "heihei";
            string c = status;
            string url = "http://" + String.Format("my", link);
            int a = 9;
        }

        // Create a temp and write something into it
        public void PathTempTest()
        {
            string mypath = Path.Combine(Path.GetTempPath(), "MyPathhaha.htm");
            FileStream fs = File.OpenWrite(mypath);
            using (TextWriter tw = new StreamWriter(fs))
            {
                tw.WriteLine("<html><head><title>Crawl Report</title><style>");
                tw.WriteLine("table { border: solid 3px black; border-collapse: collapse; }");
                tw.WriteLine("table tr th { font-weight: bold; padding: 3px; padding-left: 10px; padding-right: 10px; }");
                tw.WriteLine("table tr td { border: solid 1px black; padding: 3px;}");
                tw.WriteLine("h1, h2, p { font-family: Rockwell; }");
                tw.WriteLine("p { font-family: Rockwell; font-size: smaller; }");
                tw.WriteLine("h2 { margin-top: 20px; }");
                tw.WriteLine("</style></head><body>");
                tw.WriteLine("<h1>Crawl Report</h1>");

                tw.WriteLine("<h2>Bad Urls</h2>");
                tw.WriteLine("<p>Any bad urls will be listed here.</p>");
            }
        }
    }
}
