using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.VSHelp;
using Microsoft.VisualStudio.VSHelp80;
using System.Windows.Forms;
using EnvDTE;
using EnvDTE80;
using Extensibility;
using System.Runtime.InteropServices;
using System.Web.UI.WebControls;
using Microsoft.VisualStudio.TextManager.Interop;

namespace ConsoleApplication1
{

    public class VisualStudio_Coding
    {

        IVsFindTarget findTarget;
        IVsFindHelper findHelper;

        [DllImport("User32.dll")]
        public static extern bool WinHelp(IntPtr hwnd,string help, uint uCommand,string dwData);
        const uint HELP_CONTEXT      = 0x0001;  /* Display topic in ulTopic */
        const uint HELP_QUIT         = 0x0002;  /* Terminate help */
        const uint HELP_INDEX    = 0x0003;  /* Display index */
        const uint HELP_CONTENTS     = 0x0003;
        const uint HELP_HELPONHELP   = 0x0004;  /* Display help on using help */
        const uint HELP_SETINDEX     = 0x0005;  /* Set current Index for multi index help */
        const uint HELP_SETCONTENTS  = 0x0005;
        const uint HELP_CONTEXTPOPUP = 0x0008;
        const uint HELP_FORCEFILE    = 0x0009;
        const uint HELP_KEY      = 0x0101; /* Display topic for keyword in offabData */
        const uint HELP_COMMAND      = 0x0102;
        const uint HELP_PARTIALKEY   = 0x0105;
        const uint HELP_MULTIKEY     = 0x0201;
        const uint HELP_SETWINPOS    = 0x0203;
        const uint HELP_CONTEXTMENU  = 0x000a;
        const uint HELP_FINDER       = 0x000b;
        const uint HELP_WM_HELP      = 0x000c;
        const uint HELP_SETPOPUP_POS = 0x000d;

        const uint HELP_TCARD          = 0x8000;
        const uint HELP_TCARD_DATA     = 0x0010;
        const uint HELP_TCARD_OTHER_CALLER = 0x0011;

        const uint IDH_NO_HELP         = 28440;
        const uint IDH_MISSING_CONTEXT     = 28441; // Control doesn't have matching help context
        const uint IDH_GENERIC_HELP_BUTTON = 28442; // Property sheet help button
        const uint IDH_OK          = 28443;
        const uint IDH_CANCEL          = 28444;
        const uint IDH_HELP        = 28445;
        const uint WM_COMMAND = 0x0111;

        

        public void VsTest()
        {
            IntPtr parentHwnd = new IntPtr(0);
            string key="string";
            string yajun = "luyajun";


            bool ret = WinHelp(parentHwnd, null, HELP_HELPONHELP, "string");
                int i = 0;                           
        }

        public void TestManagerTest()
        {
            TextSpan span1=new TextSpan();
            span1.iStartLine = 64;
            TextSpan span2 = new TextSpan();
            span2.iEndLine = 76;
            TextSpan[] textSpan = new TextSpan[] { span1, span2 };

            VsTextImageClass textClass = new VsTextImageClass();
            //VsCodeWindowClass codeClass=new VsCodeWindowClass();
            //codeClass.Close();

            string guid = System.Guid.NewGuid().ToString();
    
            uint VSFR_EndOfDoc = (uint)__VSFINDRESULT.VSFR_EndOfDoc;
            uint RESULT = (uint)__VSFINDRESULT.VSFR_EndOfDoc;
            
            //findTarget.Find("luyajun", (uint)__VSFINDOPTIONS.FR_Document, 0, findHelper, out RESULT);
            findTarget.MarkSpan(textSpan); 

            int b = 56;
        }




        public void EnDTETest(DTE2 dte)
        {
            Find2 findWin;
            Document doc;
            TextDocument textDoc;
            TextSelection textSel;
            int iCtr;

            //Constants.vsViewKindTextView
            //Constants.vsProjectItemKindSolutionItems;
            string vsViewKindvalue = "{7651A703-06E5-11D1-8EBD-00A0C90F26EA}";
            string openFile = "{ADFC4E61-0397-11D1-9F4E-00A0C911004F}";
            
            // Create a new text file.
            //dte.ItemOperations.NewFile("General\\Text File", "New file", vsViewKindvalue);
                    
            // Set up references for the text document, Find object, and
            // TextSelection object.
            doc = dte.ActiveDocument;
            textDoc = (TextDocument)doc.Object("TextDocument");
            textSel = textDoc.Selection;
            findWin = (Find2)dte.Find;
            // Make sure all docs are searched before displaying results.
            findWin.WaitForFindToComplete = true;

            // Insert ten lines of text.
            //for (iCtr = 1; iCtr <= 3; iCtr++)
            //{
            //    textDoc.Selection.Text = "luyajun" + Environment.NewLine;
            //}

            //textDoc.Selection.Text = "This is a different luyajun";

            // Uses FindReplace to find all occurrences of the word, test, in 
            // the document.
            string ccc = "luyajun";

            findWin.FindWhat = "luyajun";
            findWin.MatchCase = false;
            findWin.MatchWholeWord = false;
            findWin.ReplaceWith = "luding";
            findWin.Execute();


            //findWin.FindReplace(vsFindAction.vsFindActionReplaceAll, "luyajun",
            //  (int)vsFindOptions.vsFindOptionsFromStart, "luding",
            //  vsFindTarget.vsFindTargetCurrentDocument, "",
            //  "", vsFindResultsLocation.vsFindResultsNone);

            // Uses Find2.Execute to find the word, different, in the document.
            // findWin.FindWhat = "different"
            // findWin.MatchCase = True
            // findWin.Execute()

            // Uses Find2.Execute to replace all occurrences of the word, Test, 
            // with the word, replacement.
            // findWin.FindWhat = "test"
            // findWin.ReplaceWith = "replacement"
            // findWin.Action = vsFindAction.vsFindActionReplaceAll
            // findWin.Execute()

            int a = 9;
        }
        
    }
    
}
