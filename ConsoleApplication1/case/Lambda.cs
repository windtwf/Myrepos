using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleApplication1
{
    public delegate bool D();
    public delegate bool D2(int i);

    [TestClass]
    public class Lambda
    {
       public D del;
       public D2 del2;

       protected enum GradeLevel { FirstYear = 1, SecondYear, ThirdYear, FourthYear };
       protected class Student
       {
           public string FirstName { get; set; }
           public string LastName { get; set; }
           public int ID { get; set; }
           public GradeLevel Year;
           public List<int> ExamScores;
       }

       protected static List<Student> students = new List<Student>
    {
        new Student {FirstName = "Terry", LastName = "Adams", ID = 120, Year = GradeLevel.SecondYear, ExamScores = new List<int>{ 99, 82, 81, 79}},
        new Student {FirstName = "Fadi", LastName = "Fakhouri", ID = 116, Year = GradeLevel.ThirdYear,ExamScores = new List<int>{ 99, 86, 90, 94}},
        new Student {FirstName = "Hanying", LastName = "Feng", ID = 117, Year = GradeLevel.FirstYear, ExamScores = new List<int>{ 93, 92, 80, 87}},
        new Student {FirstName = "Cesar", LastName = "Garcia", ID = 114, Year = GradeLevel.FourthYear,ExamScores = new List<int>{ 97, 89, 85, 82}},
        new Student {FirstName = "Debra", LastName = "Garcia", ID = 115, Year = GradeLevel.ThirdYear, ExamScores = new List<int>{ 35, 72, 91, 70}},
        new Student {FirstName = "Hugo", LastName = "Garcia", ID = 118, Year = GradeLevel.SecondYear, ExamScores = new List<int>{ 92, 90, 83, 78}},
        new Student {FirstName = "Sven", LastName = "Mortensen", ID = 113, Year = GradeLevel.FirstYear, ExamScores = new List<int>{ 88, 94, 65, 91}},
        new Student {FirstName = "Claire", LastName = "O'Donnell", ID = 112, Year = GradeLevel.FourthYear, ExamScores = new List<int>{ 75, 84, 91, 39}},
        new Student {FirstName = "Svetlana", LastName = "Omelchenko", ID = 111, Year = GradeLevel.SecondYear, ExamScores = new List<int>{ 97, 92, 81, 60}},
        new Student {FirstName = "Lance", LastName = "Tucker", ID = 119, Year = GradeLevel.ThirdYear, ExamScores = new List<int>{ 68, 79, 88, 92}},
        new Student {FirstName = "Michael", LastName = "Tucker", ID = 122, Year = GradeLevel.FirstYear, ExamScores = new List<int>{ 94, 92, 91, 91}},
        new Student {FirstName = "Eugene", LastName = "Zabokritski", ID = 121, Year = GradeLevel.FourthYear, ExamScores = new List<int>{ 96, 85, 91, 60}}
    };

       public void TestStudentClass()
       {
           //var categories = from student in students
           //                 group student by student.Year into studentGroup
           //                 select new { GradeLevel = studentGroup.Key, TotleScore = studentGroup.Sum(s => s.ExamScores.Sum()) };
           
           //foreach (var cat in categories)
           //{
           //    Console.WriteLine("Key={0} Sum={1}", cat.GradeLevel, cat.TotleScore);
           //}
           //Console.ReadLine();
           int[] scoreList = new int[] { 1, 2, 3, 4, 5, 6 };
           string[] strstring = new string[] { "ab", "abc", "abcd", "asdf", "asdg"};
           bool haha = strstring.All(n => n.StartsWith("ab"));
          
           

           var nameList = from student in students
                          group student by student.Year into studentGroup
                          select new { GradeLevel = studentGroup.Key, TotleScore = studentGroup.Sum(s => s.ExamScores.Sum()) };
           foreach (var list in nameList)
           {
               Console.WriteLine("My name is:");
           }
           Console.ReadLine();
       }

        [TestMethod]
        public void TestLambda()
        {
            int[] numbers = { 5, 0, 4, 24, 23, 12, 34, 6, 8, 45, 76, 89, 12 };
            int num = numbers.Count(n => n % 2 == 1);
            var firstNumberLessThan6 = numbers.TakeWhile(n => n < 6);
            var newnumber = numbers.TakeWhile((n, index) => n >= index); //index: the number's index in the array.
            var aaa = numbers.Where((n,index) => index == 3); // get the value that nubmer index is 3
            foreach (var number in aaa)
            {
                Console.WriteLine(number);
                Console.ReadLine();
            }           
        }

        
        public void TestLambdaDel(int input)
        {
            int j = 0;
            // Initialize the delegates with lambda expressions.
            // Note access to 2 outer variables.
            // del will be invoked within this method.
            del = () => { j = 10; return j > input; };

            // del2 will be invoked after TestMethod goes out of scope.
            del2 = (x) => { return x == j; };

            // Demonstrate value of j:
            // Output: j = 0 
            // The delegate has not been invoked yet.
            Console.WriteLine("j = {0}", j);

            // Invoke the delegate.
            bool boolResult = del();

            // Output: j = 10 b = True
            Console.WriteLine("j = {0}. b = {1}", j, boolResult);
        }
            
        [TestMethod]
        public void TestLambda2()
        {
            Random rd = new Random();
            int[] array={2,4,5,6};
            int count=array.Count();
            int a = rd.Next(0, array.Count());
            int c=a<count ? a + 1 : a - 1;
            int d = 12;
        }

        public void TestTakeWhile()
        {
            var list = new int[] { 1, 4, 7, 2, 8, 6, 45 };
            foreach (var lll in list.Where((n,v)=>n>=v))
            {
                Console.WriteLine("number is "+ lll);
            }

            Console.ReadLine();
        }

        
    }
}
