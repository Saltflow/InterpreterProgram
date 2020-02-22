using IntepreterProgram.Semantics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntepreterProgram.TestProgram
{
    public delegate void TestSect();
    class BatchTest
    {
        TestSect Test;
        static Parser.Parser parse;

        public static string InputString = "10 2 3 5 6 7 1 4 8 5";
        public static string OutputString = "";

        public static void OpenTest()
        {
            Program.AutoDebugTesting = true;
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Multiselect = true;
            openFile.Filter = "CMM程序|*.cmm";
            string[] allFiles;
            if(DialogResult.OK == openFile.ShowDialog())
            {
                allFiles = openFile.FileNames;
            }
            else
            {
                allFiles = null;
                return;
            }
            parse = new Parser.Parser();
           // parse = new SDTranslator();
            TestAnalysing(allFiles);
            
        }
        private static void TestAnalysing(string[] allFiles)
        {
            for (int i = 0; i < allFiles.Length; i++)
            {
                StreamReader reader = new StreamReader(allFiles[i]);
                List<string> Source = new List<string>();
                while (!reader.EndOfStream)
                {
                    Source.Add(reader.ReadLine());
                }
                int res = Analysis.Analyze(Source.ToArray());
                TestParsing();
            }
        }
        private static void TestParsing()
        {
            parse.ParseTable();
            parse = new SDTranslator();
            parse.ParseTable();
        }
    }
}
