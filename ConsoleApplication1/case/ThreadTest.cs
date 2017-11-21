using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    public class ThreadTest
    {
        public void RunMe()
        {
            int i = 0;
            Console.WriteLine("Please run me");
        }

        public static void PrintBase(IEnumerable<ThreadTest> bases)
        {
            foreach (ThreadTest t in bases)
            {
                Console.WriteLine(t);
            }
        }

        
    }

    public class Derived : ThreadTest
    {
        public static void HAHA()
        {
            List<Derived> dlist = new List<Derived>();

            Derived.PrintBase(dlist);
            IEnumerable<ThreadTest> bIenm = dlist;

            Action<IEnumerable<ThreadTest>> messTarget;

            if (Environment.GetCommandLineArgs().Length > 1)
            {
                messTarget = PrintBase;
            }
            List<String> names = new List<String>();
            names.Add("Bruce");
            names.Add("Alfred");
            names.Add("Tim");
            names.Add("Richard");            

        }
    }
}
