using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    [TestClass]
    public class Recursive
    {
        #region Test1
        [TestMethod]
        public void ResurciveTest()
        {
            int[] sub2 = { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144 };
            int foo = Foo(6);
            int i = 0;
        }

        public int Foo(int num)
        {
            //if (num <= 0) return 0;
             if (num <= 2) return 1;
            else return Foo(num - 1) + Foo(num - 2);
        }        
        #endregion


        #region Test2
        [TestMethod]
        public void ResureciveTest2()
        {
            int count = 0;
            int total = ResTest2(5, ref count);
            int counnum = count;
            int totalnum = total;
        }

        public int ResTest2(int value,ref int count)
        {
            count++;
            if (value > 10)
            {
                return value;
            } 
            return ResTest2(value+1, ref count);
        }
        #endregion


        #region
        [TestMethod]
        public void RT3()
        {
            int[] sub2 = { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144 };
            int[] sub = { 0, 2, 4, 6, 10, 16, 26, 42, 68 };
            int[] sub3 = { 1, 3, 5, 7, 9, 11, 13, 15, 17 };
            int num = Rt3Test(7);
            int a = 0;           
        }

        public int Rt3Test(int value)
        {
            if (value <=1)
            {
                return 1 ;
            }          
            else
            {
                return (Rt3Test(value - 1) + Rt3Test(value + 1)) / 2;
            }
        }
        #endregion


        #region 
        [TestMethod]
        public void RT4()
        {
           int value= RT4Test(5);
           int num = 0;
        }

        public int RT4Test(int value)
        {
            int num = 0;
            if (value == 1)
            {
                num = 1;
            }
            else
            {
                num = value * RT4Test(value - 1);
            }
            return num;
        }
        #endregion



        #region
        //从n个不同元素中任取m（m≤n）个元素，按照一定的顺序排列起来，
        //叫做从n个不同元素中取出m个元素的一个排列。当m=n时所有的排列情况叫全排列
        [TestMethod]      
        public void RT5()
        {
            string str = "abc";
            char[] charArray = str.ToCharArray();
            //permute(charArray, 0, 2);

            Console.ReadLine();
        }
        public void permute(char[] arry, int i, int n)
        {          
            int j;
            if (i == n)
            {
                Console.WriteLine(arry);
            }
            else
            {
                for (j = i; j <= n; j++)
                {
                    Swap(ref arry[i],ref arry[j]);
                    permute(arry,i+1,n);
                    Swap(ref arry[i], ref arry[j]);
                }
            }
        }
        public void Swap(ref char a, ref char b)
        {
            char temp = a;
            a = b;
            b = temp;
        }
        #endregion


        #region
        [TestMethod]
        public void testProsee()
        {
            var Myprogram = "dwm";
            var Mydir=@"D:\MyP\";
            List<string> str = new List<string>();
            foreach (string myfile in System.IO.Directory.GetDirectories(Mydir)) 
            {
                str.Add(myfile);
            }
            string a = str[1];
            var eventLong = new System.Diagnostics.EventLog();
            System.Diagnostics.Process[] myProgress = System.Diagnostics.Process.GetProcessesByName(Myprogram);
            string Mydate = null;
            foreach (System.Diagnostics.Process myprose in myProgress)
            {
                Mydate = myprose.Id.ToString();
            }
        }
        #endregion
    }
}
