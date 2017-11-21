using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleApplication1
{
    public class XDocumentTest
    {

        public void TestXDocument()
        {
            //string url = "http://msdn.microsoft.com/en-us/library/system.xml.nametable.aspx";
            //string url = "http://msdn.microsoft.com/en-us/library/ms123401";
            //string url = "http://msdn.microsoft.com/en-us/library/windows/desktop/ee663259(v=vs.85).aspx";
            // string url = "http://msdncp.redmond.corp.microsoft.com/en-us/library/bb960840";
            string url = "http://msdn.microsoft.com/en-us/library/windows/apps/br211362";

            // XmlDocument xmlDoc = null;
            XDocument xDoc = null;
            string versionUrl = url + "?view=debug";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(versionUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //xmlDoc = new XmlDocument();
            //xmlDoc.Load(response.GetResponseStream());            
            //XmlNodeList nodeList = xmlDoc.DocumentElement.ChildNodes;

            xDoc = XDocument.Load(response.GetResponseStream());
            //IEnumerable<XElement> info = from node in xDoc.Elements("mtpsDebugDocument").Elements()
            //                             where node.Name.ToString().Equals("urlParts")
            //                             select node;

            // XNamespace nameSpace = "urn:mtpg-com:mtps/2004/1/toc";
            // string nameSpace="http://www.w3.org/1999/xhtml";
            // string nameSpace="urn:mtpg-com:mtps/2004/1/toc";
            //var info = from node in xDoc.Elements("mtpsDebugDocument").Elements("tocChildren").Elements(XName.Get("Node", nameSpace))
            //           select new
            //           {
            //               NodeName = node.Attribute(XName.Get("Title", nameSpace)).Value
            //           };

            //var nodeInfo = from node in xDoc.Elements("mtpsDebugDocument").Elements("tocChildren").Elements(XName.Get("Node", tocNameSpace))
            //               select new
            //               {
            //                   NodeName = node.Attribute(XName.Get("Title", tocNameSpace)).Value,
            //                   AssetId = node.Attribute(XName.Get("Target", tocNameSpace)).Value.ToLower()
            //               };

            //var targetTopic = from node in xDoc.Elements("mtpsDebugDocument").Elements("links").Elements("link")
            //                  where node.Attribute("assetIdKey").Value.Contains("8998de77-8a81-49bc-bba6-b0848cfcb27a")
            //                  select new
            //                  {
            //                      ShortId = node.Attribute("shortId").Value
            //                  };
            //string a = targetTopic.ElementAt(0).ShortId;


            string searchNameSpace = "urn:mtpg-com:mtps/2004/1/search";
            var isExist = (from node in xDoc.Elements("mtpsDebugDocument").Elements("searchMetadata").Elements(XName.Get("search", searchNameSpace)).Elements()
                           where node.Attribute("content").Value.Contains("RelatedTopic").Equals(true)
                           select node).Any();



            bool hah = isExist;

            int jad = 0;

            //    var info = from node in xDoc.Elements("mtpsDebugDocument").Elements("tocChildren").Elements(XName.Get("Node", nameSpace))
            //               select new
            //               {
            //                   NodeName = node.Attribute(XName.Get("Target", nameSpace)).Value
            //               };


            //    string a = info.ElementAt(0).NodeName.ToLower();

            //    var assetid = from node in xDoc.Elements("mtpsDebugDocument").Elements("links").Elements("link")
            //                  where node.Attribute("assetIdKey").Value.Equals(a)
            //                  select node.Attribute("shortId").Value;


            //    int cout = assetid.Count();


            //    string b = "a";

        }

        //foreach (string i in info)
        //{
        //    string locale = i.Element("locale").Value;
        //    string logicalSiteName = i.Element("logicalSiteName").Value;
        //}

        string b = null;


        string namespace1 = "http://www.w3.org/1999/xhtml";
        string namespace2 = "http://msdn2.microsoft.com/mtps";
        public void TestAncestor()
        {
            string namespace1 = "http://www.w3.org/1999/xhtml";
            string namespace2 = "http://msdn2.microsoft.com/mtps";
            string url = "http://msdn.microsoft.com/en-us/library/windows/desktop/mtps.test.octagon.controls.codesnippet.containsmarkup";
           // XDocument xdoc = XDocument.Parse(Helper.debugSource(url));

           // string value1 = Snippet(url, "Language");

            bool language = isExist(url, "Language");
            bool hah = isExist(url, "aaa");

            //var value = from doc in xdoc.Descendants("contentSource").Descendants(XName.Get("div", namespace1))
            //            where doc.Attribute("id") != null && doc.Attribute("id").Value.Equals("GroupMulti")
            //            select doc.Elements(XName.Get("CodeSnippet", namespace2)).Attributes("Language").ElementAt(0).Value;

                  
            int abc = 9;
        }

        private string Snippet(string url,string attribute)      
        {

                XDocument xDoc = XDocument.Parse(Helper.debugSource(url));
                var value = from doc in xDoc.Descendants("contentSource").Descendants(XName.Get("div", namespace1))
                            where doc.Attribute("id") != null && doc.Attribute("id").Value.Equals("GroupMulti")
                            select doc.Elements(XName.Get("CodeSnippet", namespace2)).ElementAt(1).Attribute(attribute).Value;             
                
                return value.ElementAt(0);          
        }

        private bool isExist(string url, string attribute)
        {
            XDocument xDoc = XDocument.Parse(Helper.debugSource(url));
            //var value = xDoc.Descendants("contentSource").Descendants(XName.Get("div", namespace1))
            //            .First(node => node.Attribute("id") != null && node.Attribute("id").Value.Equals("GroupMulti"))
            //            .Elements(XName.Get("CodeSnippet", namespace2)).ElementAt(0)
            //            .Attributes().Any(n => n.Name.LocalName.Equals(attribute));

            var value = (from doc in xDoc.Descendants("contentSource").Descendants(XName.Get("div", namespace1))
                         where doc.Attribute("id") != null && doc.Attribute("id").Value.Equals("GroupMulti")
                         where doc.Elements(XName.Get("CodeSnippet", namespace2)).ElementAt(0).Attributes().Any(n => n.Name.LocalName.Equals(attribute))
                         select doc).Any();
            return value;
        }
    }
}
