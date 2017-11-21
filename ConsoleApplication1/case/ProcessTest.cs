using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    [TestClass]
    public class ProcessTest
    {
        Process process = null;

        [TestMethod]
        public void Process1Test()
        {
            try
            {
                process = Process.GetCurrentProcess();
                if (!process.HasExited)
                {
                    process.Refresh();
                    int a=process.GetHashCode();
                    var value = process.BasePriority;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
