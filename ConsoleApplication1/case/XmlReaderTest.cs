using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace ConsoleApplication1
{
    [TestClass]
    public class XmlReaderTest
    {
        [TestMethod]
        public void xmlReadTest()
        {
            //XmlTextWriter textWrite;
            XmlDocument xmlDoc = new XmlDocument();
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = @"D:\ConfigTest.config";
            XmlReader read = XmlReader.Create(filePath);
            xmlDoc.Load(read);

            XmlNodeList nodeList = xmlDoc.DocumentElement.ChildNodes;
            string key = null;
            foreach (XmlNode node in nodeList)
            {
                //string b = node.BaseURI;
                //string d = node.InnerText;
                //string e = node.LocalName;
                //string f = node.Name;
                //string g = node.Prefix;
                //string h = node.Value;
                if (node.Name.Equals("TocNavNode"))
                {
                    if (node.Attributes["MTPSID"].Value.Equals("aa187916"))
                    {
                        key = node.Attributes["SCRIPTVALUE"].Value;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    continue;
                }
            }

            string b = key;

            //textWrite = new XmlTextWriter(@"D:\NewConfigTest.config", null);
            //textWrite.WriteStartDocument();
            //textWrite.WriteComment("Hello");
            //textWrite.Close();
        }
    }
}
