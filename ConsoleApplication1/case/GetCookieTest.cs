using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ConsoleApplication1
{
    [TestClass]
    public class GetCookieTest
    {
        [DllImport("wininet.dll", SetLastError = true)]
        public static extern bool InternetGetCookie(string lpszUrl, string lpszCookieName, StringBuilder lpszCookieData, ref int lpdwSize);
        //private static string url = "http://msdn.microsoft.com/en-us/library/windows/apps/mtps.test.octagon.controls.codesnippet.languageorder";
        private static string url = "http://int.msdn.microsoft.com/en-us/library/";
        string url2 = "http://msdn.microsoft.com/en-us/library/windows/apps/mtps.test.octagon.controls.codesnippet.languageorder?cs-save-lang=1&cs-lang=cpp#code-snippet-1";
        private static string langCookie = "CodeSnippetContainerLang";
        [TestMethod]

        public void GetCookie()
        {
            getCookie(url);
        }
        
        private void getCookie(string url)
        {
            //string langCookie = "CodeSnippetContainerLang";
            //Process.Start(url);
            int size = 255;
            InternetGetCookie(url, null, null, ref size);
            StringBuilder bulide=new StringBuilder(size);
            InternetGetCookie(url, null, bulide, ref size);
            string cookie = bulide.ToString();                     
        }

        public void getHttpCookie()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = new CookieContainer();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();          
            int count = response.Cookies.Count;
            
        }

        
    }
}
