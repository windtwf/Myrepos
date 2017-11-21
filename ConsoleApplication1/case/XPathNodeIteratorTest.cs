using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ConsoleApplication1
{
    public class XPathNodeIteratorTest
    {
        private static string MapFile = "URLMap.xml";
        public Dictionary<string, List<URLData>> URLFeatureList;
        public Dictionary<string, string> URLLookUp = new Dictionary<string, string>();

        public XPathNodeIteratorTest() { }

        public XPathNodeIteratorTest(string[] features)
        {
            URLFeatureList = new Dictionary<string, List<URLData>>();
            string urlMapFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, MapFile);
            if (!File.Exists(urlMapFilePath))
            {
                throw new FileNotFoundException("File not find");
            }

            XPathDocument configXML = new XPathDocument(urlMapFilePath);
            XPathNodeIterator FeatureIterator;

            if (features.Length == 0)
            {
                FeatureIterator = configXML.CreateNavigator().Select("/URLList/Feature");
                GetFeatureList(FeatureIterator);
            }
            else
            {
                foreach (string feature in features)
                {
                    FeatureIterator = configXML.CreateNavigator().Select(string.Format("/URLList/Feature[@name='{0}']", feature));
                    GetFeatureList(FeatureIterator);
                }
            }
        }

        private void GetFeatureList(XPathNodeIterator FeatureIterator)
        {
            if (FeatureIterator == null)
            {
                throw new Exception("Error: Feature is null");
            }
            while (FeatureIterator.MoveNext())
            {
                string feature = FeatureIterator.Current.GetAttribute("name", string.Empty);
                string baseUrl = FeatureIterator.Current.GetAttribute("url", string.Empty);
                if (!URLLookUp.ContainsKey(feature))
                {
                    URLLookUp.Add(feature, baseUrl);
                }
                List<URLData> urlList = new List<URLData>();
                XPathNodeIterator urlIterator = FeatureIterator.Current.Select("URL");
                while (urlIterator.MoveNext())
                {
                    int crealLevel = 1;
                    int maxPageToCrwal = -1;
                    string innerXml = urlIterator.Current.InnerXml;
                    string url = "http://" + string.Format(urlIterator.Current.InnerXml, baseUrl);
                    Int32.TryParse(urlIterator.Current.GetAttribute("CrawlLevel", string.Empty), out crealLevel);
                    Int32.TryParse(urlIterator.Current.GetAttribute("MaxPageToCrawl", string.Empty), out maxPageToCrwal);
                    urlList.Add(new URLData(url, crealLevel, maxPageToCrwal));
                }
                URLFeatureList.Add(feature, urlList);
            }
        }


        public class URLData
        {
            public string URL { get; set; }
            public int CrawlLevel { get; set; }
            public int MaxPageToCrawl { get; set; }

            public URLData(string url, int level, int maxPagetoCrwal)
            {
                this.URL = url;
                this.CrawlLevel = level;
                this.MaxPageToCrawl = maxPagetoCrwal;
            }
        }



        public void XpathNodeIterator()
        {
            string nameSpace="urn:mtpg-com:mtps/2004/1/search";
            IXmlNamespaceResolver resolve;
            //string space = resolve.LookupNamespace("urn:mtpg-com:mtps/2004/1/search");

            //string url = "http://int.msdn.microsoft.com/en-us/library/windows/apps/hh465253.aspx";
            //string versionUrl = url + "?view=debug";
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(versionUrl);
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //XPathDocument doc = new XPathDocument(response.GetResponseStream());
            //XPathNavigator nav = doc.CreateNavigator();

            string test = "this is firtst0";
            string a = "asdg";
            var b=a.GetType();
        }
    }
}
