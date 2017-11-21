using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Word = Microsoft.Office.Interop.Word;

namespace ConsoleApplication1
{
    public class Test
    {
        public void Query()
        {
            List<int> Scores = new List<int> { 23, 45, 2, 3, 67, 87, 45, 89, 97, 99 };
            IEnumerable<int> queryInt = from score in Scores
                                        where score > 60
                                        select score;
            foreach (var i in queryInt)
            {
                int count=queryInt.Count();
                Console.WriteLine(i + " ");
                Console.ReadLine();
            }
            Console.WriteLine("There are {0} number in the Query",queryInt.Count());
            Console.ReadLine();

            string test = "";
        }        

        /// <summary>
        /// Offic API. To input words in Office Word.
        /// </summary>
        public void DisplayWord()
        {
            var wordApp = new Word.Application();
            wordApp.Visible = true;
            // docs is a collection of all the Document objects currently  
            // open in Word.
            Word.Documents docs = wordApp.Documents;

            // Add a document to the collection and name it doc. 
            Word.Document doc = docs.Add();

            // Define a range, a contiguous area in the document, by specifying 
            // a starting and ending character position. Currently, the document 
            // is empty.
            Word.Range range = doc.Range(0, 0);

            // Use the InsertAfter method to insert a string at the end of the 
            // current range.
            range.InsertAfter("Testing, testing, testing. . .");

            // You can comment out any or all of the following statements to 
            // see the effect of each one in the Word document. 

            // Next, use the ConvertToTable method to put the text into a table.  
            // The method has 16 optional parameters. You only have to specify 
            // values for those you want to change. 

            // Convert to a simple table. The table will have a single row with 
            // three columns.
            range.ConvertToTable(Separator: ",");

            // Change to a single column with three rows..
            range.ConvertToTable(Separator: ",", AutoFit: true, NumColumns: 1);

            // Format the table.
            range.ConvertToTable(Separator: ",", AutoFit: true, NumColumns: 1,
                Format: Word.WdTableFormat.wdTableFormatList7);
        }
    }
}
