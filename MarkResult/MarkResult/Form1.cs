using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.TestManagement.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace MarkResult
{
    public partial class Form1 : Form
    {

        private string xmlPath = @".\markResult.xml";
        
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            RemoveNameSpace();
            DialogResult result = MessageBox.Show(string.Format("There're {0} Passed cases for the test run. Do you want to continue?", GetFailedCaseNamesListFromTrxFile.Count), "Confirmation", MessageBoxButtons.YesNo);
            //File.WriteAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FailText.xlsx"), GetFailedCaseNamesListFromTrxFile);
            if (result == DialogResult.Yes)
            {                    
                MarkResultMethod();
                MessageBox.Show("Done", "Confirmation", MessageBoxButtons.OK);
            }
            else
            {
                Application.Exit();
            }           
            Application.Exit();
        }

        // Mark result in MTM
        private void MarkResultMethod()
        {
            TfsTeamProjectCollection teamCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri("https://mseng.visualstudio.com:443/defaultcollection"));
            ITestManagementTeamProject project = teamCollection.GetService<ITestManagementService>().GetTeamProject("VSOnline");
            var service = teamCollection.GetService<ITestManagementService>();

            ITestPlan plan = project.TestPlans.Find(Convert.ToInt32(txtProjectID.Text));

            // Create a new test run 
            ITestRun testRun = plan.CreateTestRun(true);


            // Add the certain a test to  the run 
            string query = string.Format("SELECT * from TestPoint where SuiteID='{0}'", txtSuitID.Text);

            ITestPointCollection testPoints = plan.QueryTestPoints(query);
            List<string> failedCaceMTM = new List<string>();

            foreach (ITestPoint testPoint in testPoints)
            {
                // for caseId please type: testPoint.TestCaseId
                testRun.AddTestPoint(testPoint, null);
                //string caseName = testPoint.TestCaseWorkItem.Implementation.DisplayText;                 
                //string cname = caseName.Substring(caseName.LastIndexOf(".") + 1);

                //if (GetFailedCaseNamesListFromTrxFile.Contains(cname))
                //{
                //    failedCaceMTM.Add(cname);
                //}
            }
            var blockCase = GetFailedCaseNamesListFromTrxFile.Except(failedCaceMTM).ToList();

            testRun.Save();

            //Update the outcome of the test       
            ITestCaseResultCollection results = testRun.QueryResults();
            ;

            foreach (ITestCaseResult result in results)
            {
                // Get case name in MTM.
                string caseName = result.Implementation.DisplayText;
                string name = caseName.Substring(caseName.LastIndexOf(".") + 1);
                result.Outcome = GetFailedCaseNamesListFromTrxFile.Contains(name) ? TestOutcome.Passed : TestOutcome.Failed;
                result.State = TestResultState.Completed;
                result.Save();
            }
            testRun.Save();
            testRun.Refresh();
            File.Delete(xmlPath);        
        }

        private void btn_trx_Click(object sender, EventArgs e)
        {
            if (ofd_Trx.ShowDialog() == DialogResult.OK)
            {
                txt_trx.Text = ofd_Trx.FileName;
            }
        }

        private void RemoveNameSpace()
        {
            
            if (File.Exists(xmlPath))
            {
                File.Delete(xmlPath);
            }

            File.Copy(txt_trx.Text, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "markResult.xml"));
            string fileNameSpace = "xmlns=\"http://microsoft.com/schemas/VisualStudio/TeamTest/2010\"";

            string test = File.ReadAllText(xmlPath);
            bool isTrue = test.Contains(fileNameSpace);
            test = test.Replace(fileNameSpace, " ");
            File.WriteAllText(xmlPath, test);
        }

        // Get two value from *.trx file
        private List<string> GetFailedCaseNamesListFromTrxFile
        {
            get
            {               
                Dictionary<string, string> testting = new Dictionary<string, string>();
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "markResult.xml");
                XPathDocument xmlDoc = new XPathDocument(path);

                XPathNodeIterator NodeIterator;
                NodeIterator = xmlDoc.CreateNavigator().Select("/TestRun/Results/UnitTestResult");

                List<string> GetFailedOutCome = new List<string>();
                while (NodeIterator.MoveNext())
                {
                    if (NodeIterator.Current.GetAttribute("outcome", string.Empty).Equals("Passed"))
                    {
                        string testName = NodeIterator.Current.GetAttribute("testName", string.Empty);
                        string outCome = NodeIterator.Current.GetAttribute("outcome", string.Empty);
                        try
                        {
                            testting.Add(testName, outCome);
                            GetFailedOutCome.Add(testName);
                        }
                        catch (Exception e)
                        {
                            throw new Exception(string.Format("Found duplicated test case name: '{0}',please change the name!", testName));
                        }
                    }
                }
                return GetFailedOutCome;
            }
        }

        // Get three value from .Tra file 
        private Dictionary<Tuple<string, string>, string> GetThreeTraFileValue
        {
            get
            {
                if (File.Exists(@".\markResult.xml"))
                {
                    File.Delete(@".\markResult.xml");
                }

                //File.Copy(txt_trx.Text, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "markResult.xml"));
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "170.xml");

                XPathDocument xmlDoc = new XPathDocument(path);

                XPathNodeIterator NodeIterator;
                NodeIterator = xmlDoc.CreateNavigator().Select("/TestRun/Results/UnitTestResult");

                Dictionary<Tuple<string, string>, string> GetFailedOutCome = new Dictionary<Tuple<string, string>, string>();
                while (NodeIterator.MoveNext())
                {
                    if (NodeIterator.Current.GetAttribute("outcome", string.Empty).Equals("Passed"))
                    {
                        string testName = NodeIterator.Current.GetAttribute("testName", string.Empty);
                        string outCome = NodeIterator.Current.GetAttribute("outcome", string.Empty);
                        string testId = NodeIterator.Current.GetAttribute("testId", string.Empty);
                        GetFailedOutCome.Add(Tuple.Create(testName, testId), outCome);                        
                    }
                }                
                return GetFailedOutCome;

                /*
                // use foreach to get the value from Dictionary
                foreach (var getValue in GetFailedOutCome)
                {
                    string testName = getValue.Key.Item1;
                    string outCome = getValue.Key.Item2;
                    string testId = getValue.Value;
                }        
                */
            }
        }

        // To Read the DLL file
        private void ReadDLL()
        {
            string baseDirectory = string.Format(AppDomain.CurrentDomain.BaseDirectory + "{0}", @"\ProfileAPI\ProfileAPI.dll");

            Assembly assembly = Assembly.LoadFrom(baseDirectory);
            IEnumerable<TypeInfo> method = assembly.DefinedTypes;

            Dictionary<string, string> dicInfo = new Dictionary<string, string>();

            foreach (TypeInfo info in method)
            {
                foreach (MethodInfo methodInfo in info.DeclaredMethods.Where(n => !n.IsPrivate))
                {
                    var methodName = methodInfo.Name;

                    // WorkItemAttribute , TestPropertyAttribute
                    foreach (CustomAttributeData workItem in methodInfo.CustomAttributes.Where(n => n.AttributeType.Name.Equals("TestPropertyAttribute")))
                    {
                        // when attribute is WorkItemAttribute, switch ConstructorArguments[0] 
                        var workItemID = workItem.ConstructorArguments[1].Value.ToString();
                        dicInfo.Add(methodName, workItemID);
                    }
                }
            }
        }

        private void ModifyTxt()
        {
            string txt;
            List<string> caseList = new List<string>();
            using (StreamReader reader = new StreamReader(@".\PassCaseMTM.txt"))
            {
                while ((txt = reader.ReadLine()) != null)
                {
                    txt = txt.Substring(txt.LastIndexOf(".") + 1);
                    caseList.Add(txt);
                }
            }

            var testst = GetFailedCaseNamesListFromTrxFile.Intersect(caseList).ToList();
            File.WriteAllLines(@".\PassCaseMTM.txt", caseList);
        }

        private void txtPlayList_Click(object sender, EventArgs e)
        {
            if (ofd_playlist.ShowDialog()==DialogResult.OK)
            {
                txtPlaylist.Text = ofd_playlist.FileName;
            }

            string text = File.ReadAllText(txtPlaylist.Text);
            MatchCollection collectioin = Regex.Matches(text, @"Mtps.WebApplication.*\w");

            List<string> caseNames = new List<string>();
            foreach (var item in collectioin)
            {
                caseNames.Add(item.ToString());
            }

            XmlDocument xml = new XmlDocument();
            XmlElement playlist = xml.CreateElement("Playlist");
            xml.AppendChild(playlist);
            XmlAttribute versionAttr = xml.CreateAttribute("Version");
            versionAttr.InnerText = "1.0";           
            playlist.Attributes.Append(versionAttr);

            foreach (string item in caseNames)
            {
                XmlElement add = xml.CreateElement("Add");
                playlist.AppendChild(add);
                XmlAttribute testAttr = xml.CreateAttribute("Test");
                testAttr.InnerText = item;
                add.Attributes.Append(testAttr);
            }
            xml.Save(@"D:\new.playlist");

            MessageBox.Show("Done");
            Application.Exit();
        }
    }
}
