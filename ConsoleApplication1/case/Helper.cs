using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public static class Helper
    {
        public static string debugSource(string url)
        {
            string webPageContent = string.Empty;
            string versionUrl = url + "?view=debug";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(versionUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            webPageContent=new StreamReader(response.GetResponseStream()).ReadToEnd();
            return webPageContent.Replace(Environment.NewLine, "");
        }
 
    }
}
