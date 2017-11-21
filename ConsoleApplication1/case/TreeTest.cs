using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ConsoleApplication1
{
    [TestClass]
    public class TreeTest
    {
        private const string url = "http://msdn.microsoft.com/en-us/library/";
        private const string url1 = "http://msdn.microsoft.com/en-us/library/cc294537(v=expression.40).aspx";
        private const string noLinkUrl = "http://msdn.microsoft.com/en-us/library/aa286501.aspx";
        private const string host = "http://msdn.microsoft.com";
        private XDocument xDocument;
        private XNamespace xhtml = "http://www.w3.org/1999/xhtml";
        private const string toclevel0 = "toclevel0";
        private const string toclevel1 = "toclevel1 current";
        private const string toclevel2 = "toclevel2";
        private List<string> Links = null;
       
        public void TestTree()
        {
            // Get the div list except "tocnav"
            var divList = from xDoc in GetPageSource.Elements("div").Elements()
                       where xDoc.Attribute("class").Value.Equals(toclevel2)
                       select xDoc;
            
            int name = divList.Count(n => n.Elements("span").Count() > 0);           
                      
            foreach (var div in divList)
            {               
                // Get the "a" link element                
                var ss = div.Element("a").Attribute("href").Value;               
                Console.WriteLine(ss);
            }
            Console.ReadLine();
        }

        private XDocument GetPageSource
        {
            get
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(noLinkUrl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string pageSource = new StreamReader(response.GetResponseStream()).ReadToEnd().Replace("\n", "");
                string pattern = string.Format(@"<div id=\u0022tocnav\u0022.*?</div></div>");
                Match match = Regex.Match(pageSource, pattern, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    string toc = match.Groups[0].ToString();
                    xDocument = XDocument.Parse(toc);
                }
                else
                {
                    throw new Exception("The page source is not match");
                }

                return xDocument;
            }
        }
      
        public void XpathSelectElementTest()
        {
            string markup = @"<aw:Root xmlns:aw='http://www.adventure-works.com'>
                                <aw:Child1>child one data</aw:Child1>
                                <aw:Child2>child two data</aw:Child2>
                            </aw:Root>";                            
            XmlReader reader = XmlReader.Create(new StringReader(markup));
            XElement root = XElement.Load(reader);
            XmlNameTable nameTable = reader.NameTable;
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(nameTable);
            namespaceManager.AddNamespace("aw", "http://www.adventure-works.com");
            XElement child1 = root.XPathSelectElement("./aw:Child1", namespaceManager);
            Console.WriteLine(child1);
        }





        //XElement xmlTree = new XElement("Root",
        //                new XElement("Child1", 1),
        //                new XElement("Child2", 2),
        //                new XElement("Child3", 3),
        //                new XElement("Child4", 4),
        //                new XElement("Child5", 5)
        //                );

        //Console.WriteLine(xmlTree);
        //Console.ReadLine();
        //XNode firstNode = xmlTree.FirstNode;
        //Console.WriteLine(firstNode);
        //Console.ReadLine();
    }
}
