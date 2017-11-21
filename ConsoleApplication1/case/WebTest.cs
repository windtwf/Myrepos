using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleApplication1
{
    [TestClass]
    public class WebTest
    {       

        [TestMethod]
        public void testWeb()
        {
            //string url = "http://msdn.microsoft.com/de-de/library/";
            string url = "http://msdn.microsoft.com/en-us/library/windows/apps/mtps.test.octagon.controls.codesnippet.languageorder";
            //string snippetUrl = "http://msdn.microsoft.com/zh-cn/library/system.collections.hashtable(v=vs.110).aspx";
            Uri uri = new Uri(url);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://msdn.microsoft.com/en-us/library/windows/apps/mtps.test.octagon.controls.codesnippet.languageorder?cs-save-lang=1&cs-lang=javascript#code-snippet-1");
            request.CookieContainer = new CookieContainer();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //request.Referer = "http://msdn.microsoft.com/de-de/library/";
            request.AllowAutoRedirect = false;

            int count = response.Cookies.Count;
            foreach (Cookie cookie in response.Cookies)
            {
                string name = cookie.Name;
                string value = cookie.Value;
                DateTime expires = cookie.Expires;
                DateTime timestamp = cookie.TimeStamp;
                int version = cookie.Version;
                Uri commicalurl = cookie.CommentUri;
                string comment = cookie.Comment;
            }

            
           




         
        }
    }
}
