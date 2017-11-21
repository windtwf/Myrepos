using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHDocVw;

namespace ConsoleApplication1
{
    public class GetIEUrl
    {
        public void GetIeAddress()
        {
            foreach (InternetExplorer ie in  new ShellWindows())
            {
                string url = ie.LocationURL;
                string name = ie.Name;
                ie.Navigate("http://msdn.microsoft.com/zh-cn/library", Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                string a = "a";
            }

           // InternetExplorerClass ie=
        }
    }
}
