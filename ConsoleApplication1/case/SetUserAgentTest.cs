using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using SHDocVw;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ConsoleApplication1
{
    public class SetUserAgentTest
    {
        //[DllImport("wininet.dll")]
        //public static extern bool HttpQueryInfo(IntPtr hRequest, uint dwinfoLevel, ref string lpvBuff, ref int ipdwBuffLength, ref uint lpdwindex);



        public void TestUserAgent()
        {
            string url = "http://msdn.microsoft.com/en-us/library";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //string agent = request.UserAgent;
            
           // int count = re.Headers.Count;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            int a=request.Headers.Count;
            int c = response.Headers.Count;
            //string a = response.ProtocolVersion.ToString();

            

            //Stream stream = response.GetResponseStream();
            //StreamReader reader = new StreamReader(stream);

            WebClient client = new WebClient();
            int b=client.Headers.Count;
           
            // string[] a = HttpContext.Current.Request.Headers.GetValues("User-Agent");
            
            
            string newlineaa = "adg";         
        }
    }
}
