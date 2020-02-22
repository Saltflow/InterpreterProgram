using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntepreterProgram
{
    class UIHelper
    {
        static string[] selectedCode;
        public static TextBox outTextBox;
        public static Mainform UIForm;
        public static TextBox inTextbox;

        public static Thread IntepreteThread;

        private delegate void updateTextBox(string str);

        public static Mutex UIMutex = new Mutex();

        public static void setTextBox(TextBox text)
        {
            outTextBox = text;
        }
        public static void ExtractCode(TabControl tabControl)
        {
            if (tabControl.SelectedTab.Controls.Count == 0)
                return;
            TextBox nowCode = (TextBox) tabControl.SelectedTab.Controls[0];
            selectedCode = nowCode.Lines;
        }
        /*the functon should start a interprete thread ,
         * and it is the thread`s job to update the interprete result to result box
         */
        public static void OutInterpreteResult()
        {
            IntepreteThread = new Thread(new ParameterizedThreadStart(AsyncInterprete));
            IntepreteThread.Start(selectedCode);
            inTextbox.Enabled = true;
        }

        
        public static void AsyncInterprete(object ProgramText)
        {

            Analysis.Analyze((string[]) ProgramText);
            Parser.Parser parser = new Parser.Parser();
            parser.ParseTable();
            parser = new Semantics.SDTranslator();
            parser.ParseTable();
            Semantics.SDTranslator.ParseTree.SemanticAnalysis();

        }

        public static void OutParseResult()
        {
            Parser.Parser parser = new Parser.Parser();
            parser.ParseTable();
            Form ptForm = new Parser.ShowParserTree(Parser.Parser.ParseTree);
            ptForm.Show();
        }

        public static void ShowAllValsAndVars()
        {
            Semantics.Identitables identiTable = new Semantics.Identitables();
            identiTable.Show();
        }

        public static void SematicDebugging()
        {
            Analysis.Analyze(selectedCode);
            Parser.Parser parser = new Parser.Parser();
            parser.ParseTable();
            parser = new Semantics.SDTranslator();
            parser.ParseTable();
            Semantics.AnnoNode.debugging = true;
            Semantics.AnnoNode.debug_update = false;
            Thread thread = new Thread(Semantics.SDTranslator.ParseTree.SemanticAnalysis);
        }

        public static void PressContinue(TextBox resultBox)
        {
            Semantics.AnnoTree.MessageLock.WaitOne();
            resultBox.Text += Semantics.AnnoTree.NodeOnExecute.ToString() + "\r\n";
            Semantics.AnnoTree.MessageLock.ReleaseMutex();
        }
        

        /*stimulation of printf in terminal, which should buffer by line
         */
        public static Queue<string> InBuffer = new Queue<string>();

        /*Sema_up for each valid input,
         * Sema_down for programs try to getting input.
         */
        static Semaphore InSema = new Semaphore(0,0xffff);

        /* read input textbox to buffer
         * this function should only work on UI thread*/
        public static void ReadtoBuffer()
        {
            string inputLines = inTextbox.Text;
            inputLines = inputLines.Replace('\r', ' ');

            string[] nowInput = inputLines.Split(' ');
            foreach(string inputText in nowInput)
            {
                /* remove empty string & backspaces*/
                if (inputText.Length == 0)
                    continue;
                if (inputText == "\n")
                    continue;
                UIMutex.WaitOne();
                InBuffer.Enqueue(inputText);
                UIMutex.ReleaseMutex();
                InSema.Release();
            }
            Log("Read" + inTextbox.Text);
            inTextbox.Text = "";
            
        }

        /* get next input string in Worker Thread
         * note that program will not retrive all the input,
         * so it just get the string through the next space
         */
        public static string getInput()
        {
            if(Program.AutoDebugTesting == true)
            {
                string[] nowInput = TestProgram.BatchTest.InputString.Split(' ');
                if (nowInput.Length != 1)
                    TestProgram.BatchTest.InputString = TestProgram.BatchTest.InputString.Substring(nowInput[0].Length + 1);
                else
                    TestProgram.BatchTest.InputString = "";
                return nowInput[0];
            }
            else
            {
                if(InBuffer.Count == 0)
                {
                    Log("Waiting for input");
                }
                InSema.WaitOne();
                UIMutex.WaitOne();
                string res = InBuffer.Dequeue();
                UIMutex.ReleaseMutex();
                return res;
            }

        }


        private static void AsynMsg(string msg)
        {
            outTextBox.Text += msg;
        }

        public static void Log(string msg)
        {
            if (Program.AutoDebugTesting == true)
            {
                Console.WriteLine("Log:" + msg);
            }
            else
            {
                updateTextBox uptext = new updateTextBox(AsynMsg);
                UIForm.Invoke(uptext, "Log:" + msg + "\r\n");
            }
        }



        public static void Output(string val)
        {
            if (Program.AutoDebugTesting == true)
            {
                TestProgram.BatchTest.OutputString += val;
                Console.WriteLine("Output :" + val + "\r\n");
            }
            else
            {
                updateTextBox uptext = new updateTextBox(AsynMsg);
                UIForm.Invoke(uptext, "Output:" + val + "\r\n");
                
            }
        }
    }
}
