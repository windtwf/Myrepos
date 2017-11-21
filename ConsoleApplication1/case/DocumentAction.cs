using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication1
{
    [TestClass]
    public class DocumentAction
    {

        [TestMethod]
        public void getSystemDirectory()
        {
            string path = @"D:\";
            List<string> infoList = new List<string>();
            foreach (string dire in System.IO.Directory.GetDirectories(path))
            {
                infoList.Add(dire);
            }
            int count = infoList.Count;
        }

        [TestMethod]
        public void BackDeskTop()
        {
            var MyProcess = new System.Diagnostics.Process();
            MyProcess.StartInfo.FileName = "MyDesktop.scf";
            MyProcess.StartInfo.Verb = "open";
            MyProcess.Start();
        }
    }
}
