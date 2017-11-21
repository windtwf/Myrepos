using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.UI;
using System.Web;
using System.Xml;
using System.Net;
using System.Xml.Linq;
using SHDocVw;
using EnvDTE80;
//using EnvDTE;
using System.IO;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using System.Collections;

namespace ConsoleApplication1
{
    public class Date<T>
    {
        private T[] arr = new T[100];

        public T this[int i]
        {
            get
            {
                return arr[i];
            }
            set
            {
                arr[i] = value;
            }
        }
    }

  



    public class Program
    {
        public static System.Collections.Generic.IEnumerable<int> EvenSequence(int firstNumber, int lastNumber)
        {
            for (int i = firstNumber; i <= lastNumber; i++)
            {
                if (i % 2 == 0)
                {
                    yield return i;
                }
            }
        }

        public static void Main(string[] args)
        {
            foreach (int num in EvenSequence(5, 18))
            {
                Console.WriteLine(num.ToString()+ " ");
            }
            Console.ReadKey();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);                                            
            //Object o = new Object();
            //bool right = o is Object;

            //ThreadTest test = new ThreadTest();
            //Thread t = new Thread(test.RunMe);
            //t.Start();

            // Email
            //string body = "this is my first email";
            //string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //SendEmailTest.SendEmail(Body, "ludingEnvironment");
            //var system=Environment.Is64BitOperatingSystem;
            //AppPathCombine_Tests test = new AppPathCombine_Tests();
            //test.AppCombineTest();                 
        }



        private static string Body
        {
            get
            {
                StringWriter swriter = new StringWriter();
                Table infoTable = new Table();
                infoTable.Attributes.Add("border", "2");
                TableRow infoRow;
                TableCell infoCell;

                #region - Table Title
                infoRow = new TableRow();
                infoCell = new TableCell();
                infoCell.Text = "Refresh Failures";
                infoRow.Cells.Add(infoCell);
                infoTable.Rows.Add(infoRow);
                #endregion
                string[] fNames = { "file1.txt", "file2.txt", "file3.txt" };
                foreach (String fName in fNames)
                {
                    infoRow = new TableRow();
                    infoCell = new TableCell();
                    infoCell.Text = fName;
                    infoRow.Cells.Add(infoCell);
                    infoTable.Rows.Add(infoRow);
                }

                using (HtmlTextWriter htmlWriter = new HtmlTextWriter(swriter))
                {
                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Html);//Html
                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Head);//Head
                    htmlWriter.AddAttribute(HtmlTextWriterAttribute.Type, "text/css");//Style att
                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Style);//Style
                    htmlWriter.Write("body {margin-left: 20px; font-family: segoe ui, tahoma, sans-serif;}\n");
                    htmlWriter.Write("h1 {color: #f00; text-align: center;}\n");
                    htmlWriter.Write("td {padding-right: 10px;}");
                    htmlWriter.RenderEndTag();//Style
                    htmlWriter.RenderEndTag();//Head

                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Body);//Body
                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.H1);//Header1
                    htmlWriter.Write("***** - Data Refresh Failures.");
                    htmlWriter.RenderEndTag();//Header1

                    infoTable.RenderControl(htmlWriter);//Render the table here..while still inside HTML tags.

                    htmlWriter.RenderEndTag();//Body
                    htmlWriter.RenderEndTag();//Html


                }

                return swriter.ToString();
            }
        }

        //string value = string.Empty;
        //string iroot = "/tocChildren/ns:toc:Node";
        //string name = "toc:Title";
        //string xml ="<tocChildren><toc:Node toc:Title=\"Networking\" toc:SubTree=\"AssetId:MSDN%7cNetworking%7c%24%5cnetworking.hxt%400%2c0\" toc:SubTreeVersion=\"MSDN.10\" toc:SubTreeLocale=\"en-us\" toc:Description=\"\" toc:Target=\"AssetId:f80bf168-4936-4736-bc62-d0a557b3cd58\" toc:TargetLocale=\"en-us\" toc:TargetVersion=\"VS.85\" toc:IsPhantom=\"false\" xmlns:toc=\"urn:mtpg-com:mtps/2004/1/toc\" xmlns:mtps=\"http://msdn2.microsoft.com/mtps\" xmlns:mshelp=\"http://msdncp.redmond.corp.microsoft.com/mshelp\" xmlns:mtps-scripts=\"urn:mtps-scripts\">  <toc:Node toc:Title=\"Network Authentication\" toc:SubTree=\"AssetId:MSDN%7cNetworking%7c%24%5cnetworking.hxt%400%2c0%2c0\" toc:SubTreeVersion=\"MSDN.10\" toc:SubTreeLocale=\"en-us\" toc:Target=\"AssetId:cfa660a0-00e9-496e-9cb1-f6bc99391726\" toc:TargetLocale=\"en-us\" toc:TargetVersion=\"VS.85\" toc:IsPhantom=\"false\" />  <toc:Node toc:Title=\"Network Communication\" toc:SubTree=\"AssetId:MSDN%7cNetworking%7c%24%5cnetworking.hxt%400%2c0%2c1\" toc:SubTreeVersion=\"MSDN.10\" toc:SubTreeLocale=\"en-us\" toc:Target=\"AssetId:4a26e513-0b15-4b61-a076-8ce9e8d1c4bd\" toc:TargetLocale=\"en-us\" toc:TargetVersion=\"VS.85\" toc:IsPhantom=\"false\" />  <toc:Node toc:Title=\"Network Firewall and Routing\" toc:SubTree=\"AssetId:MSDN%7cNetworking%7c%24%5cnetworking.hxt%400%2c0%2c2\" toc:SubTreeVersion=\"MSDN.10\" toc:SubTreeLocale=\"en-us\" toc:Target=\"AssetId:6fe94836-e439-4bf7-8972-c287afd78990\" toc:TargetLocale=\"en-us\" toc:TargetVersion=\"VS.85\" toc:IsPhantom=\"false\" />  <toc:Node toc:Title=\"Network Management\" toc:SubTree=\"AssetId:MSDN%7cNetworking%7c%24%5cnetworking.hxt%400%2c0%2c3\" toc:SubTreeVersion=\"MSDN.10\" toc:SubTreeLocale=\"en-us\" toc:Target=\"AssetId:363deb9c-a09b-4387-990a-1555390f2a3e\" toc:TargetLocale=\"en-us\" toc:TargetVersion=\"VS.85\" toc:IsPhantom=\"false\" />  <toc:Node toc:Title=\"Network Protocols\" toc:SubTree=\"AssetId:MSDN%7cNetworking%7c%24%5cnetworking.hxt%400%2c0%2c4\" toc:SubTreeVersion=\"MSDN.10\" toc:SubTreeLocale=\"en-us\" toc:Target=\"AssetId:3bcbde3e-3af2-4fab-a3bd-0e62c1e13a4b\" toc:TargetLocale=\"en-us\" toc:TargetVersion=\"VS.85\" toc:IsPhantom=\"false\" />  <toc:Node toc:Title=\"Wireless Networking\" toc:SubTree=\"AssetId:MSDN%7cNetworking%7c%24%5cnetworking.hxt%400%2c0%2c5\" toc:SubTreeVersion=\"MSDN.10\" toc:SubTreeLocale=\"en-us\" toc:Target=\"AssetId:54303cfc-7737-47d4-a0ea-b5a531c3014d\" toc:TargetLocale=\"en-us\" toc:TargetVersion=\"VS.85\" toc:IsPhantom=\"false\" /></toc:Node></tocChildren>";
        //XmlDocument doc = new XmlDocument();
        //XmlNamespaceManager nameSpace = new XmlNamespaceManager(doc.NameTable);
        //nameSpace.AddNamespace("ns", "urn:mtpg-com:mtps/2004/1/toc");
        //doc.LoadXml(xml);
        //value = doc.SelectSingleNode(iroot).Attributes[name].Value;
        //int a = 1;



        //string domain = AppDomain.CurrentDomain.DomainManager.EntryAssembly.CodeBase;
        //WebTest wt = new WebTest();
        //wt.testWeb();


        //public static System.Collections.IEnumerable Power(int number,int exponent)
        //{
        //    int result = 1;
        //    for (int i = 0; i < exponent; i++)
        //    {
        //        result = result * number;
        //        yield return result;

        //    }
        //}

    }
}